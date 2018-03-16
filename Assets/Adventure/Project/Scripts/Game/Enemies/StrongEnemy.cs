using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongEnemy : Enemy {

	public Animator enemyAnimator;

	private Vector3 originalEnemyAnimatorPosition;

	void Awake () {
		originalEnemyAnimatorPosition = enemyAnimator.transform.localPosition;

		enemyAnimator.SetFloat ("Forward", 0.3f);
	}
	
	void LateUpdate () {
		enemyAnimator.transform.localPosition = originalEnemyAnimatorPosition;
	}
}
