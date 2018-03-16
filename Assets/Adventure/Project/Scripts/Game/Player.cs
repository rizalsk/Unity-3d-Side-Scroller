using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[Header("Visuals")]
	public GameObject model;
	public Animator playerAnimator;
	public float rotatingSpeed = 2f;

	[Header("Movement")]
	public float movingVelocity;
	public float jumpingVelocity;
	public float knockbackForce;
	public float playerRotatingSpeed = 1000f;

	[Header("Equipment")]
	public int health = 5;
	public Sword sword;
	public Bow bow;
	public GameObject quiver;
	public int arrowAmount = 15;
	public GameObject bombPrefab;
	public int bombAmount = 5;
	public float throwingSpeed;
	public int orbAmount = 0;

	private Rigidbody playerRigidbody;
	private bool canJump;
	private Quaternion targetModelRotation;
	private float knockbackTimer;
	private bool justTeleported;
	private Vector3 originalPlayerAnimatorPosition;
	private Dungeon currentDungeon;

	public bool JustTeleported {
		get {
			bool returnValue = justTeleported;
			justTeleported = false;
			return returnValue;
		}
	}

	public Dungeon CurrentDungeon {
		get {
			return currentDungeon;
		}
	}

	// Use this for initialization
	void Start () {
		bow.gameObject.SetActive (false);
		quiver.gameObject.SetActive (false);

		playerRigidbody = GetComponent<Rigidbody> ();
		targetModelRotation = Quaternion.Euler (0, 0, 0);
		originalPlayerAnimatorPosition = playerAnimator.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		// Raycast to identify if the player can jump.
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.down, out hit, 1.31f)) {
			canJump = true;
		}

		playerAnimator.SetBool ("OnGround", canJump);

		model.transform.rotation = Quaternion.Lerp (model.transform.rotation, targetModelRotation, Time.deltaTime * rotatingSpeed);

		if (knockbackTimer > 0) {
			knockbackTimer -= Time.deltaTime;
		} else {
			ProcessInput ();
		}
	}

	void LateUpdate () {
		// Keep the character animator's gameobject in place.
		playerAnimator.transform.localPosition = originalPlayerAnimatorPosition;
	}

	void ProcessInput () {
		// Move in the XZ plane.
		playerRigidbody.velocity = new Vector3 (
			0,
			playerRigidbody.velocity.y,
			0
		);

		bool isPlayerMoving = false;
		if (Input.GetKey("right") || Input.GetKey(KeyCode.D)) {
			targetModelRotation = Quaternion.Euler (
				0, 
				model.transform.localEulerAngles.y + playerRotatingSpeed * Time.deltaTime, 
				0
			);
		}
		if (Input.GetKey ("left") || Input.GetKey(KeyCode.A)) {
			targetModelRotation = Quaternion.Euler (
				0, 
				model.transform.localEulerAngles.y - playerRotatingSpeed * Time.deltaTime, 
				0
			);
		}
		if (Input.GetKey ("up") || Input.GetKey(KeyCode.W)) {
			playerRigidbody.velocity = new Vector3 (
				model.transform.forward.x * movingVelocity,
				playerRigidbody.velocity.y,
				model.transform.forward.z * movingVelocity
			);

			isPlayerMoving = true;
		}
		if (Input.GetKey ("down") || Input.GetKey (KeyCode.S)) {
			playerRigidbody.velocity = new Vector3 (
				-model.transform.forward.x * movingVelocity,
				playerRigidbody.velocity.y,
				-model.transform.forward.z * movingVelocity
			);

			isPlayerMoving = true;
		}

		playerAnimator.SetFloat ("Forward", isPlayerMoving ? 1f : 0f);

		// Check for jumps.
		if (canJump && Input.GetKeyDown ("space")) {
			canJump = false;
			playerRigidbody.velocity = new Vector3 (
				playerRigidbody.velocity.x,
				jumpingVelocity,
				playerRigidbody.velocity.z
			);
		}

		// Check equipment interaction.
		if (Input.GetKeyDown ("z")) {
			sword.gameObject.SetActive (true);
			bow.gameObject.SetActive (false);
			quiver.gameObject.SetActive (false);

			sword.Attack ();
		}

		if (Input.GetKeyDown ("x")) {
			if (arrowAmount > 0) {
				sword.gameObject.SetActive (false);
				bow.gameObject.SetActive (true);
				quiver.gameObject.SetActive (true);

				bow.Attack ();
				arrowAmount--;
			}
		}

		if (Input.GetKeyDown ("c")) {
			ThrowBomb ();
		}
	}

	private void ThrowBomb () {
		if (bombAmount <= 0) {
			return;
		}

		GameObject bombObject = Instantiate (bombPrefab);
		bombObject.transform.position = transform.position + model.transform.forward;

		Vector3 throwingDirection = (model.transform.forward + Vector3.up).normalized;

		bombObject.GetComponent<Rigidbody> ().AddForce (throwingDirection * throwingSpeed);

		bombAmount--;
	}

	void OnTriggerEnter (Collider otherCollider) {
		if (otherCollider.GetComponent<EnemyBullet> () != null) {
			Hit ((transform.position - otherCollider.transform.position).normalized);
			Destroy (otherCollider.gameObject);
		} else if (otherCollider.GetComponent<Treasure> () != null) {
			orbAmount++;
			Destroy (otherCollider.gameObject);
		}
	}

	void OnTriggerStay (Collider otherCollider) {
		if (otherCollider.GetComponent<Dungeon> () != null) {
			currentDungeon = otherCollider.GetComponent<Dungeon> ();
		}
	}

	void OnTriggerExit (Collider otherCollider) {
		if (otherCollider.GetComponent<Dungeon> () != null) {
			Dungeon exitDungeon = otherCollider.GetComponent<Dungeon> ();
			if (exitDungeon == currentDungeon) {
				currentDungeon = null;
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.GetComponent<Enemy> ()) {
			Hit ((transform.position - collision.transform.position).normalized);
		}
	}

	private void Hit (Vector3 direction) {
		Vector3 knockbackDirection = (direction + Vector3.up).normalized;
		playerRigidbody.AddForce (knockbackDirection * knockbackForce);
		knockbackTimer = 1f;

		health--;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void Teleport (Vector3 target) {
		transform.position = target;
		justTeleported = true;
	}
}
