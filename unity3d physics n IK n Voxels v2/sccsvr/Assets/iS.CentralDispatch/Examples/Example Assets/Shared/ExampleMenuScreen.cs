using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ExampleMenuScreen : MonoBehaviour {
 
	public void LoadEx1(){
		SceneManager.LoadScene ("Example 1 - Realtime Fracturing");
	}

	public void LoadEx2(){
		SceneManager.LoadScene ("Example 2 - Counting Numbers");
	}

	public void LoadEx3(){
		SceneManager.LoadScene ("Example 3 - Multi-Threaded Logs");
	}

	public void LoadEx4(){
		SceneManager.LoadScene ("Example 4 - Multi-Threaded Move");
	}

	public void LoadEx5(){
		SceneManager.LoadScene ("Example 5 - Pause and Resume");
	}

	public void LoadEx6(){
		SceneManager.LoadScene ("Example 6 - Thread and Enumerator");
	}

	public void LoadEx7(){
		SceneManager.LoadScene ("Example 7 - Async Instantiating");
	}
}
