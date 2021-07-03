using UnityEngine;
using System.Collections;

public class Ex4DemoController : MonoBehaviour {

	public Transform mainCube;
	public GameObject[] cubes;

	private int displayedCount = 7;

	public void DisplayNewCube(){
		if (displayedCount == cubes.Length -1)
			return;
		
		displayedCount++;
		cubes [displayedCount].SetActive (true);
		cubes [displayedCount].transform.position = new Vector3 (cubes [displayedCount].transform.position.x, cubes [displayedCount].transform.position.y, mainCube.position.z);

	}

	public void RemoveCube(){
		if (displayedCount == 0)
			return;

		cubes [displayedCount].SetActive (false);
		displayedCount--;
	}
}
