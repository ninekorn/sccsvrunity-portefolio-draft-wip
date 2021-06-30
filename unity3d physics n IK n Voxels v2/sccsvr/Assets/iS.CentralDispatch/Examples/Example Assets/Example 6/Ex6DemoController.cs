using UnityEngine;
using System.Collections;
using SPINACH.iSCentralDispatch;

public class Ex6DemoController : MonoBehaviour {

	public GameObject[] objects;
	public Transform spawnPoint;

	int threadID = -1;

	void Start () {
		threadID = iSCentralDispatch.DispatchNewThread ("Spawner",MyThread);

		iSCentralDispatch.SetTargetFramerate (70);
	}

	void MyThread () {
		//iSCDDebug.Log ("I am an alternative thread, I am preparing to spawn some big things.");
		//iSCDDebug.Log ("Before I start, I would like to have a rest, so I pause my self for 2 seconds and use Enumerator to wait.");

		iSCentralDispatch.DispatchMainThread (() => {
			StartCoroutine(Timer());
		});
		iSCentralDispatch.PauseThread (threadID);

		//iSCDDebug.Log ("Now I will start spawning...");

		iSCentralDispatch.DispatchMainThread (() => {
			StartCoroutine(Spawner(2000));
		});
			
		iSCentralDispatch.PauseThread (threadID);

		//iSCDDebug.Log ("I have spawned 2000 of objects without blocking the gameplay, isn't that cool ?");
	}

	IEnumerator Timer(){
		yield return new WaitForSeconds (2);
		iSCentralDispatch.ResumeThread (threadID);
	}

	IEnumerator Spawner(int count){
		for (int i = 0; i < count; i++) {
			Instantiate (objects[Random.Range (0, objects.Length)], spawnPoint.position, spawnPoint.rotation);

			if (i % 10 == 0)
				yield return null;
		}

		iSCentralDispatch.ResumeThread (threadID);
	}
}
