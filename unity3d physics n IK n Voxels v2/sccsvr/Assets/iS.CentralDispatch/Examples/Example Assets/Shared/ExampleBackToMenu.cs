using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ExampleBackToMenu : MonoBehaviour {
	
	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			LoadMainMenu ();
		}
	}

	public void LoadMainMenu(){
		SceneManager.LoadScene ("Example Menu");
	}
}
