using UnityEngine;
using System.Collections;

public class UnlockMouse : MonoBehaviour {

	void Start () {
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
}
