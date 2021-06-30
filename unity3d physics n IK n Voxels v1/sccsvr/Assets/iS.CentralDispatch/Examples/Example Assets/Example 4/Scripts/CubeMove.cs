using UnityEngine;
using System.Threading;
using System.Collections;

public class CubeMove : MonoBehaviour {

	public Vector3 speed;

	void Update(){
		transform.Translate (speed * Time.deltaTime);
	}


}