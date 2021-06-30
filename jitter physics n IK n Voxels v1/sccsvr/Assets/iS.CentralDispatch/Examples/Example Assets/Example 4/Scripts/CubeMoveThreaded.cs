using UnityEngine;
using System.Collections;
using SPINACH.iSCentralDispatch;

public class CubeMoveThreaded : MonoBehaviour {

	public Vector3 speed;

	int threadID = 0;

	void OnEnable () {
		threadID = iSCentralDispatch.DispatchNewThread ("Synced Move",ThreadedMove);
	}

	void ThreadedMove(){
		while (true) {
			iSCentralDispatch.LifeReport ();
			iSCentralDispatch.DispatchMainThread (() => {
				transform.Translate (speed * Time.deltaTime);
			});
		}
	}

	void OnDisable(){
		if(iSCentralDispatch.RuntimeStarted()) iSCentralDispatch.AbortThread (threadID);
	}
}
