using UnityEngine;
using System.Collections;

public class PlanePlacer : MonoBehaviour {

	public Transform root;
	public GameObject plane;
	public float distanceInterval;
	public float timeInterval;

	void Start () {
		StartCoroutine (Place ());
	}

	float currentInterval = 0;
	IEnumerator Place(){
		while (enabled) {

			GameObject obj = Instantiate (plane) as GameObject;
			obj.transform.parent = root;
			obj.transform.localRotation = Quaternion.identity;
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = new Vector3 (obj.transform.position.x, obj.transform.position.y, currentInterval);
			currentInterval += distanceInterval;

			yield return new WaitForSeconds (timeInterval);
		}
	}
}
