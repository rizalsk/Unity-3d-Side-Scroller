using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public static InputManager instance;

	public delegate void InputEvent();
	public static event InputEvent OnPressDown;
	public static event InputEvent OnPressUp;
	public static event InputEvent OnTap;
	public static event InputEvent KeyPressDown;

	// Use this for initialization
	void Start () {
		if(instance == null) instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonUp(0)){
			if(OnPressUp != null) OnPressUp();
		}
		if(Input.anyKeyDown){
			if(KeyPressDown != null) KeyPressDown();
			print("Input Key: " + Input.inputString);
		}
	}
}
