using UnityEditor;
using UnityEngine;
using System.Collections;

namespace SPINACH.iSCentralDispatch.Editor{

	public class iSCDEditorLinker{

		[MenuItem("Window/iSCD Debugger")]
		static public void OpenDebuggerWindow(){
			iSCDRuntimeDebuggerWindow.Open ();
		}
	}
}