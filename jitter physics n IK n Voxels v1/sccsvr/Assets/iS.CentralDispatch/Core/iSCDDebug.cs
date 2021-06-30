using UnityEngine;
using System.Collections;
using SPINACH.iSCentralDispatch.Internal;

namespace SPINACH.iSCentralDispatch{

	public class iSCDDebug {
		static private iSCentralDispatchRuntime runtime;
		static private iSCentralDispatchRuntimeDebugger debugger;

		//static private bool enableDebugNotified = false;

		static private iSCentralDispatchRuntime GetRuntime(){
			if (runtime) return runtime;

			runtime = iSCentralDispatchRuntime.GetDefaultRuntime ();
			return runtime;
		}

		static private iSCentralDispatchRuntimeDebugger GetDebugger(){
			if (debugger) return debugger;

			runtime = iSCentralDispatchRuntime.GetDefaultRuntime ();
			if (!runtime) {
				System.Exception e = new System.Exception ("NoDebuggerFound: Runtime not inited");
				Debug.LogError(e);
				throw e;
			}
				
			if (!runtime.IsDebugging ()) {
				iSCentralDispatch.DispatchMainThread (() => {
					runtime.EnableDebugger();
				});
			}
			debugger = runtime.GetDebugger ();

			return debugger;
		}

		static public void StartDebugging(){
			GetRuntime ().EnableDebugger ();
		}

		static public void StopDebugging(){
			GetRuntime ().DisableDebugger ();
		}
			
		static public void Log(object content){
			if(GetDebugger())GetDebugger ().Log (content, iSCDRDLogType.Info, false);
		}

		static public void LogWarning(object content){
			if(GetDebugger())GetDebugger ().Log (content, iSCDRDLogType.Warning, false);
		}

		static public void LogError(object content){
			if(GetDebugger())GetDebugger ().Log (content, iSCDRDLogType.Error, false);
		}
	}
}