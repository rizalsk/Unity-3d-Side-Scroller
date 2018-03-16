using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ButtonScript : MonoBehaviour {
	public Button restart;
	public Button exit;
	private Transform  gameOverPanel;

	// Use this for initialization
	void Start () {
		gameOverPanel = UIManager.instance.canvas.Find("Panel_Gameover");
		restart = gameOverPanel.Find("Restart").GetComponentInChildren<Button> ();
		exit = gameOverPanel.Find("Exit").GetComponentInChildren<Button> ();
	}

	public void RestartGame(){
		Application.LoadLevel ("Main 3d");
	}

	public void ExitGame(){
		Application.Quit ();
	}
}
