using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour {

	public GameObject playerModel;
	public GameObject arrowPrefab;

	public void Attack () {
		GameObject arrowObject = Instantiate (arrowPrefab);
		arrowObject.transform.position = playerModel.transform.position + playerModel.transform.forward;
		arrowObject.transform.forward = playerModel.transform.forward;
	}
}
