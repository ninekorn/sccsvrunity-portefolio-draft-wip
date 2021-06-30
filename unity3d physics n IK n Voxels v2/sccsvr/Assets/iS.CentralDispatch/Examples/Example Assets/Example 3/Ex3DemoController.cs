using UnityEngine;
using System.Collections;
using SPINACH.iSCentralDispatch;

public class Ex3DemoController : MonoBehaviour {


	void Start () {
		iSCentralDispatch.DispatchNewThread ("Logger 1", Logger1);
		iSCentralDispatch.DispatchNewThread ("Logger 2", Logger2);
		iSCentralDispatch.DispatchNewThread ("Logger 3", Logger3);
	}
	
	void Logger1(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			iSCDDebug.Log ("Hello, I am iS.CentrualDispatch");

			//Execute something fun to delay.
			for(int i = 0; i < 100000000; i++);
		}
	}

	void Logger2(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			iSCDDebug.Log ("iS.CentralDispatch is made by SPINACH");

			//Execute something fun to delay.
			for (int i = 0; i < 100000000; i++);
		}

	}

	void Logger3(){
		while (true) {
			iSCentralDispatch.LifeReport ();

			iSCDDebug.Log ("Actually, Qi is the one who wrote the core codes.");

			//Execute something fun to delay.
			for (int i = 0; i < 100000000; i++);
		}

	}
}
