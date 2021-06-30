using UnityEngine;
using System.Collections;

public class Ex1DemoController : MonoBehaviour {

	public float forceToAdd;
	public GameObject[] prefab;
	public Transform spawnPoint;

	bool fireReady = true;

	IEnumerator GetReady(){
		yield return new WaitForSeconds (1f);
		fireReady = true;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0) && fireReady) {
			GameObject obj = Instantiate (prefab[Random.Range(0,prefab.Length)], spawnPoint.position, Random.rotation) as GameObject;
			obj.GetComponent<Rigidbody> ().AddForce (spawnPoint.forward * forceToAdd);
			fireReady = false;
			StartCoroutine (GetReady ());
		}
	}
}
