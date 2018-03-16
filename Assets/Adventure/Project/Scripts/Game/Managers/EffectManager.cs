using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour {

	private static EffectManager instance;
	public static EffectManager Instance { 
		get {
			return instance;
		}
	}

	public GameObject hitEffectPrefab;
	public GameObject killEffectPrefab;

	void Awake () {
		instance = this;
	}

	public void ApplyEffect (Vector3 position, GameObject effectPrefab) {
		GameObject effectObject = Instantiate (effectPrefab);
		effectObject.transform.SetParent (transform);
		effectObject.transform.position = position;
	}
}
