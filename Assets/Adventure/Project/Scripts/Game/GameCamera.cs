using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour {

	public GameObject target;
	public Vector3 targetOffset;
	public float playerFocusHeight = 12f;
	public float playerFocusDepth = -4f;
	public float focusSpeed = 1f;

	public GameObject temporaryTarget;
	public float temporaryFocusTime = 3f;

	// Update is called once per frame
	void LateUpdate () {
		if (temporaryTarget != null) {
			transform.position = Vector3.Lerp (transform.position, temporaryTarget.transform.position + targetOffset, Time.deltaTime * focusSpeed);

			transform.localEulerAngles = new Vector3 (
				transform.localEulerAngles.x,
				0,
				transform.localEulerAngles.z
			);
		}
		else if (target != null) {
			if (target.GetComponent<Player> () != null) {
				Player player = target.GetComponent<Player> ();

				Vector3 playerTargetPosition = player.transform.position + Vector3.up * playerFocusHeight + player.model.transform.forward * playerFocusDepth;

				transform.position = Vector3.Lerp (transform.position, playerTargetPosition, Time.deltaTime * focusSpeed);

				if (player.JustTeleported) {
					transform.position = playerTargetPosition;
				}

				transform.localEulerAngles = new Vector3 (
					transform.localEulerAngles.x,
					player.model.transform.localEulerAngles.y,
					transform.localEulerAngles.z
				);
			} else {
				transform.position = Vector3.Lerp (transform.position, target.transform.position + targetOffset, Time.deltaTime * focusSpeed);
			}
		}
	}

	public void FocusOn (GameObject target) {
		temporaryTarget = target;

		StartCoroutine (FocusOnRoutine ());
	}

	private IEnumerator FocusOnRoutine () {
		yield return new WaitForSeconds (temporaryFocusTime);

		temporaryTarget = null;
	}
}
