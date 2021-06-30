using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SPINACH.iSCentralDispatch;

public class Ex5DemoController : MonoBehaviour {
	public Text bar1Text;
	public Text bar2Text;

	public Image bar1Image;
	public Image bar2Image;

	public int highest;

	int bar1 = 0;
	int bar2 = 0;

	int thread2ID = -1;

	void Start () {
		int bar1ID = iSCentralDispatch.DispatchNewThread ("Increase Bar 1",IncreaseBar1);
		iSCentralDispatch.SetPriorityForThread (bar1ID, iSCDThreadPriority.VeryLow);

		int bar2ID = iSCentralDispatch.DispatchNewThread ("Increase Bar 2",IncreaseBar2);
		iSCentralDispatch.SetPriorityForThread (bar2ID, iSCDThreadPriority.VeryHigh);

		thread2ID = bar2ID;
		StartCoroutine (PauseAndResume ());
	}

	void Update(){
		bar1Text.text = bar1.ToString ();
		bar1Image.fillAmount = (float)bar1 / (float)highest;

		bar2Text.text = bar2.ToString ();
		bar2Image.fillAmount = (float)bar2 / (float)highest;
	}

	IEnumerator PauseAndResume(){
		while (enabled) {
			if (bar2 >= highest * 0.9f)
				break;

			yield return new WaitForSeconds (1);
			iSCentralDispatch.PauseThread (thread2ID);

			yield return new WaitForSeconds (0.5f);
			iSCentralDispatch.ResumeThread (thread2ID);
		}
	}

	void IncreaseBar1(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			bar1++;
			if (bar1 >= highest)
				break;
		}
	}

	void IncreaseBar2(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			bar2++;
			if (bar2 >= highest)
				break;
		}
	}
}
