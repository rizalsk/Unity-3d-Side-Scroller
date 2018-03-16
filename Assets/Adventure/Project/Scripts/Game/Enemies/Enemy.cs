using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	public int health = 1;

	public virtual void Hit () {
		health--;
		if (health <= 0) {
			EffectManager.Instance.ApplyEffect (transform.position, EffectManager.Instance.killEffectPrefab);
			Destroy (gameObject);
		} else {
			EffectManager.Instance.ApplyEffect (transform.position, EffectManager.Instance.hitEffectPrefab);
		}
	}

	public void OnTriggerEnter (Collider otherCollider) {
		if (otherCollider.GetComponent<Arrow> () != null) {
			Hit ();
			Destroy (otherCollider.gameObject);
		}
	}

	public void OnTriggerStay (Collider otherCollider) {
		if (otherCollider.GetComponent<Sword> () != null) {
			if (otherCollider.GetComponent<Sword> ().JustAttacked) {
				Hit ();
			}
		}
	}
}
