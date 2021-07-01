using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//brb

public class ExampleWindow : MonoBehaviour
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = true;
    float myFloat = 1.23f;


    [MenuItem("Window/ExampleWindow")]
    public static void ShowWindow()
    {
        //https://www.youtube.com/watch?v=491TSNwXTIg => not working anymore at 5:26 of the video
        //ExampleWindow window = (ExampleWindow)EditorWindow.GetWindow(typeof(ExampleWindow)); // not working in Unity3D 2017.4.26f1
        //ExampleWindow window = (ExampleWindow)EditorWindow.GetWindow(typeof(ExampleWindow)); // not working from Unity3D website https://docs.unity3d.com/ScriptReference/EditorWindow.html
        //window.Show();

        //GetWindow<ExampleWindow>("Example");
    }

    void OnGui()
    {

        //https://www.youtube.com/watch?v=491TSNwXTIg => not working anymore at 5:26 of the video
        /*GUILayout.Label("This is a Label.", EditorStyles.boldLabel)
        myString = EditorGUILayout.TextField("Name", myString);
        if (GUILayout.Button("Press Me"))
        {

        }*/



        GUILayout.Label("Base Settings", EditorStyles.boldLabel);
        myString = EditorGUILayout.TextField("Text Field", myString);

        groupEnabled = EditorGUILayout.BeginToggleGroup("Optional Settings", groupEnabled);
        myBool = EditorGUILayout.Toggle("Toggle", myBool);
        myFloat = EditorGUILayout.Slider("Slider", myFloat, -3, 3);
        EditorGUILayout.EndToggleGroup();
    }


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
