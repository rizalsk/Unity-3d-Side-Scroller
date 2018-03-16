using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public static UIManager instance;


	public Transform canvas;
	//Quest Info
	public Transform questInfo;
	public Transform questInfoContent;
	public Button questInfoAcceptButton;
	public Button questInfoCancelButton;
	public Button questInfoCompleteButton;
	//Quest Book - Quest Grid
	public Transform questBook;
	public Transform questBookContent;
	public Button questBookCancelButton;


	// Use this for initialization
	void Awake () {
		if(!instance) instance = this;
		canvas = GameObject.Find("Canvas").transform;

		//Quest Info
		questInfo = canvas.Find("Quest Info");
		questInfoContent = questInfo.Find("Background/Info/Viewport/Content");
		questInfoAcceptButton = questInfo.Find("Background/Buttons/Accept").GetComponent<Button>();
		questInfoCompleteButton = questInfo.Find("Background/Buttons/Complete").GetComponent<Button>();

		//Quest Info Cancel Button
		questInfoCancelButton = questInfo.Find("Background/Buttons/Cancel").GetComponent<Button>();
		questInfoCancelButton.onClick.AddListener(() => {
			questInfo.gameObject.SetActive(false);
		});

		//Quest Book
		questBook = canvas.Find("Quest Book");
		questBookContent = questBook.Find("Background/Info/Viewport/Content");

		//Quest Book Cancel Button
		questBookCancelButton = questBook.Find("Background/Buttons/Cancel").GetComponent<Button>();
		questBookCancelButton.onClick.AddListener(() => {
			questBook.gameObject.SetActive(false);
		});

	}
}
