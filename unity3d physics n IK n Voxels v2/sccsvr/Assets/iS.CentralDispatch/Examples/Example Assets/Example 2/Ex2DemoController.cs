using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SPINACH.iSCentralDispatch;

public class Ex2DemoController : MonoBehaviour {

	public Text bar1Text;
	public Text bar2Text;
	public Text bar3Text;

	public Image bar1Image;
	public Image bar2Image;
	public Image bar3Image;

	public int highest;

	int bar1 = 0;
	int bar2 = 0;
	int bar3 = 0;


	void Start () {
		int bar1ID = iSCentralDispatch.DispatchNewThread ("Increase Bar 1",IncreaseBar1);
		iSCentralDispatch.SetPriorityForThread (bar1ID, iSCDThreadPriority.VeryHigh);

		int bar2ID = iSCentralDispatch.DispatchNewThread ("Increase Bar 2",IncreaseBar2);
		iSCentralDispatch.SetPriorityForThread (bar2ID, iSCDThreadPriority.Normal);

		int bar3ID = iSCentralDispatch.DispatchNewThread ("Increase Bar 3",IncreaseBar3);
		iSCentralDispatch.SetPriorityForThread (bar3ID, iSCDThreadPriority.VeryLow);
	}

	void Update(){
		bar1Text.text = bar1.ToString ();
		bar1Image.fillAmount = (float)bar1 / (float)highest;

		bar2Text.text = bar2.ToString ();
		bar2Image.fillAmount = (float)bar2 / (float)highest;

		bar3Text.text = bar3.ToString ();
		bar3Image.fillAmount = (float)bar3 / (float)highest;
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

	void IncreaseBar3(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			bar3++;
			if (bar3 >= highest)
				break;
		}
	}
}
