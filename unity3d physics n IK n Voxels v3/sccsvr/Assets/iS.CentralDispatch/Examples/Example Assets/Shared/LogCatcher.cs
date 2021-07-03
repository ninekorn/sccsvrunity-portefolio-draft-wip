using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LogCatcher : MonoBehaviour {

	public Text textBox;
	public int maxLines;

	List<string> logs = new List<string>();

	void OnEnable() {
		Application.logMessageReceived += HandleLog;
	}
	void OnDisable() {
		Application.logMessageReceived -= HandleLog;
	}
	void HandleLog(string logString, string stackTrace, LogType type) {
		logs.Add (logString);
		if (logs.Count > maxLines)
			logs.RemoveAt (0);

		SendToLabel ();
	}

	void SendToLabel () {
		string text = "";

		foreach (string t in logs) {
			text += t + "\n";
		}

		textBox.text = text;
	}
}
