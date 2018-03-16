using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {

	private int enemyCount;
	private int currentEnemyCount;
	private Enemy[] enemies;
	private Treasure treasure;
	private bool isClear;
	private bool justCleared;

	public int EnemyCount {
		get {
			return enemyCount;
		}
	}

	public int CurrentEnemyCount {
		get {
			return currentEnemyCount;
		}
	}

	public bool JustCleared {
		get {
			bool returnValue = justCleared;
			justCleared = false;
			return returnValue;
		}
	}

	public Treasure Treasure {
		get {
			return treasure;
		}
	}

	// Use this for initialization
	void Start () {
		enemies = GetComponentsInChildren<Enemy> ();
		treasure = GetComponentInChildren<Treasure> ();

		treasure.gameObject.SetActive (false);

		enemyCount = enemies.Length;
	}
	
	// Update is called once per frame
	void Update () {
		currentEnemyCount = 0;
		foreach (Enemy enemy in enemies) {
			if (enemy != null) {
				currentEnemyCount++;
			}
		}

		if (isClear == false) {
			if (currentEnemyCount == 0) {
				isClear = true;
				treasure.gameObject.SetActive (true);

				justCleared = true;
			}
		}
	}
}
