using UnityEngine;
using System.Collections;

public class Math3D : MonoBehaviour {

	public static Vector3 SideMultiplication(Vector3 a, Vector3 b){
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}

}
