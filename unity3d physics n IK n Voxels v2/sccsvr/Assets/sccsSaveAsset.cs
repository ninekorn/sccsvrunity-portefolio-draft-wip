using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class sccsSaveAsset : MonoBehaviour {




	// Use this for initialization
	void Start () {
        string planeAssetName = this.transform.gameObject.name + ".asset";
        AssetDatabase.CreateAsset(this.transform.gameObject.GetComponent<MeshFilter>().mesh, "Assets/Editor/" + planeAssetName);
        AssetDatabase.SaveAssets();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
