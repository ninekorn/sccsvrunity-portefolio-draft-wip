using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using SPINACH.iSCentralDispatch.Internal;

namespace SPINACH.iSCentralDispatch.Editor{
	public class iSCDRuntimeDebuggerWindow : EditorWindow {

		static private iSCDRuntimeDebuggerWindow window;

		static public void Open () {
			window = (iSCDRuntimeDebuggerWindow)EditorWindow.GetWindow (typeof (iSCDRuntimeDebuggerWindow));

			window.titleContent = new GUIContent("iSCD Debugger");
			window.autoRepaintOnSceneChange = true;
			window.Show ();
		}

		void LateUpdate(){
			if(!window) window = (iSCDRuntimeDebuggerWindow)EditorWindow.GetWindow (typeof (iSCDRuntimeDebuggerWindow));
			window.Repaint ();
		}

		Vector2 scrollPos;
		void OnGUI () {

			if(!window) window = (iSCDRuntimeDebuggerWindow)EditorWindow.GetWindow (typeof (iSCDRuntimeDebuggerWindow));

			if (!iSCentralDispatchRuntime.IsRuntimeInitialized()) {
				EditorGUI.DropShadowLabel (new Rect (0, 0, window.position.width, window.position.height/2), "Runtime Not Started\n\nCreate iSCentralDispatchRuntime to start debugging");
				return;
			}

			iSCentralDispatchRuntime runtime = iSCentralDispatchRuntime.GetDefaultRuntime ();
			if (!runtime.IsDebugging ()) runtime.EnableDebugger ();
			iSCentralDispatchRuntimeDebugger debugger = runtime.GetDebugger ();

			Rect runtimeInfoRect = new Rect (5, 5, window.position.width - 10, 170);

			GUI.Box (runtimeInfoRect,"");
			EditorGUI.DropShadowLabel (new Rect (0, 16, window.position.width, 11), "Runtime Information");

			GUILayout.BeginArea (runtimeInfoRect);

			Space (5);

			FullLengthValueLabel ("Runtime Started :", "Yes");
			FullLengthValueLabel ("Time Since Runtime Started :", (debugger.MillisecondsSinceRuntimeStarted() * 0.001d).ToString("F3") + "s");

			DrawLine (1);
			//Space (1);

			FullLengthValueLabel ("Number of Running Threads :", debugger.NumberOfRunningThreads().ToString());
			FullLengthValueLabel ("Total Threads Started :", debugger.TotalStartedThreads().ToString());

			DrawLine (1);
			//Space (1);

			FullLengthValueLabel ("Number of Main-thread Issued :", debugger.TotalMainThreadDispatches().ToString());
			FullLengthValueLabel ("Current Main-thread Pending Tasks :", debugger.MainThreadPendingQueueLength().ToString());
			DrawLine (1);

			GUILayout.EndArea ();

			Rect runtimeDetailsRect = new Rect (5, 180, window.position.width - 10, window.position.height - 190);

			GUI.Box (runtimeDetailsRect,"");
			EditorGUI.DropShadowLabel (new Rect (0, 190, window.position.width, 11), "Threads Details");
			GUILayout.BeginArea (runtimeDetailsRect);

			Space (5);

			List<iSCDThreadContainer> cl = runtime.GetActiveContainerList ();

			using (var h = new EditorGUILayout.HorizontalScope ())
			{
				using (var scrollView = new EditorGUILayout.ScrollViewScope (scrollPos))
				{
					int cellHeight = 90;

					scrollPos = scrollView.scrollPosition;
					GUILayout.Label ("", GUILayout.Height (cl.Count * cellHeight));

					for (int i = 0; i < cl.Count; i++) {

						iSCDThreadContainer tc = cl [i];

						float widthOffset = (cl.Count * cellHeight) > runtimeDetailsRect.height ? 40 : 30;
						Rect detailCellRect = new Rect (10, i*cellHeight, window.position.width - widthOffset, cellHeight);

						if (i % 2 == 0) {
							GUI.Box (detailCellRect,"");
						}

						GUILayout.BeginArea (detailCellRect);

						Space (1);

						string status = "Running";
						if(!tc.IsResponding()) status = "Not Responding..";
						if(tc.IsWaiting()) status = "Waiting for Other..";
						if(!tc.IsRunning()) status = "Stopped";

						FullLengthValueLabel (tc.iSCD_Name + "[" + tc.iSCD_RuntimeID.ToString () + "]", "Status : " + status);
						GUILayout.Label ("Life Time: " + (debugger.MillisecondsSinceThreadStarted (tc.iSCD_RuntimeID)*0.001d).ToString() + "s");

						List<iSCDRDLogPackage> logs = debugger.LogsForThreadID (tc.iSCD_RuntimeID);
						if (logs.Count > 0) {
							GUILayout.Label (string.Format("({0}): {1}", logs[logs.Count-1].logTime.ToString("F2"),logs [logs.Count-1].content.ToString ()));
						} else {
							GUILayout.Label ("No Logs Yet...");
						}

						GUILayout.BeginHorizontal ();

						if (GUILayout.Button ("Abort Now")) {
							runtime.AbortThread (tc.iSCD_RuntimeID);
						}

						GUILayout.EndHorizontal ();

						GUILayout.EndArea ();
					}

				}
			}

			GUILayout.EndArea ();

		}

		void FullLengthValueLabel(string title, string v){
			GUILayout.BeginHorizontal ();
			GUILayout.Label (title,EditorStyles.boldLabel,GUILayout.ExpandWidth(true),GUILayout.MaxWidth(1000000));
			GUILayout.Label (v);
			GUILayout.EndHorizontal ();
		}

		void Space(int num){
			for (int i = 0; i < num; i++)
				EditorGUILayout.Space ();
		}

		void DrawLine(float lineWidth){
			GUILayout.Box ("", GUILayout.Height (lineWidth),GUILayout.ExpandWidth(true));
		}
	}
}
