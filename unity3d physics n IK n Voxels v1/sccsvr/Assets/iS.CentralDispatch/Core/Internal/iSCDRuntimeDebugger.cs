using System;
using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

namespace SPINACH.iSCentralDispatch.Internal{
	public class iSCentralDispatchRuntimeDebugger : MonoBehaviour {

		public bool verbose = false;

		private int totalThreadsStarted = 0;
		private int mainThreadDispatches = 0;

		private List<iSCDDebuggingThreadContainerInfo> containerInfos = new List<iSCDDebuggingThreadContainerInfo>();
		private List<iSCDRDLogPackage> logPackages = new List<iSCDRDLogPackage>();
		private iSCentralDispatchRuntime runtime;

		static public void StartDebugger(){
			iSCentralDispatchRuntime r = iSCentralDispatchRuntime.GetDefaultRuntime ();
			if (r == null) {
				throw new System.Exception ("iSCDRuntimeDebugger Failed to init: no active runtime found");
			}

			r.EnableDebugger ();
		}

		void Awake(){
			runtime = GetComponent<iSCentralDispatchRuntime> ();

			foreach (iSCDThreadContainer c in runtime.GetActiveContainerList()) {
				CountThreads (c);
			}
		}

		int logsThisFrame = 0;
		void Update(){
			lock (logPackages) {
				while (logPackages.Count > 0) {
					if (logsThisFrame >= 50) {
						Debug.Log ("iSCDLogger: " + logPackages.Count.ToString () + " more log left to next frame");
						break;
					}

					iSCDRDLogPackage p = logPackages [0];
					logPackages.RemoveAt (0);
					string message = FormatThreadName(p) + p.content;

					if (!verbose && p.isVerbose) continue;
					switch (p.type) {
					case iSCDRDLogType.Info:
						Debug.Log (message);
						break;

					case iSCDRDLogType.Warning:
						Debug.LogWarning (message);
						break;

					case iSCDRDLogType.Error:
						Debug.LogError (message);
						break;
					}

					logsThisFrame++;
				}
			}

			logsThisFrame = 0;
		}

		string FormatThreadName(iSCDRDLogPackage p){
			return string.Format ("<color=#00DFFF>{0}[{1}]({2}s): </color>", p.sender.iSCD_Name, p.sender.iSCD_RuntimeID, p.logTime.ToString("F2"));
		}

		iSCDThreadContainer GetCurrentSender(){
			if (Thread.CurrentThread.Name != null) {
				return runtime.ContainerForID (int.Parse (Thread.CurrentThread.Name));
			} else {
				return null;
			}

		}
			
		iSCDDebuggingThreadContainerInfo ContainerInfoForThreadID(int id){
			foreach (iSCDDebuggingThreadContainerInfo info in containerInfos) {
				if (info.container.iSCD_RuntimeID == id)
					return info;
			}

			return null;
		}

		public void RegisterThreadAbort(int threadID){
			iSCDDebuggingThreadContainerInfo info = ContainerInfoForThreadID(threadID);
			info.lifeTime = (DateTime.UtcNow - info.initedTime).TotalMilliseconds;
		}

		public void CountThreads(iSCDThreadContainer container){
			iSCDDebuggingThreadContainerInfo info = new iSCDDebuggingThreadContainerInfo (container);
			containerInfos.Add (info);

			totalThreadsStarted++;
		}

		public void CountMainThreadDispatchs(){
			mainThreadDispatches++;
		}

		public void Log(object content, iSCDRDLogType type, bool verbose){
			if (Thread.CurrentThread.Name == null)
				throw new System.Exception ("iSCentralDispatchRuntimeDebugger.Log cannot be called in main thread.");

			iSCDThreadContainer container = GetCurrentSender ();

			iSCDRDLogPackage package = new iSCDRDLogPackage (container, type, content, MillisecondsSinceThreadStarted (container.iSCD_RuntimeID) * 0.001f, verbose);

			iSCDDebuggingThreadContainerInfo info = ContainerInfoForThreadID (container.iSCD_RuntimeID);
			info.logs.Add (package);

			lock(logPackages) logPackages.Add (package);
		}

		public double MillisecondsSinceRuntimeStarted(){
			return (System.DateTime.UtcNow - runtime.RuntimeInitalizedTime ()).TotalMilliseconds;
		}

		public double MillisecondsSinceThreadStarted(int threadID){
			iSCDDebuggingThreadContainerInfo info = ContainerInfoForThreadID (threadID);
			if (info.container.IsRunning ()) {
				return (DateTime.UtcNow - info.initedTime).TotalMilliseconds;
			} else {
				return info.lifeTime;
			}
		}

		public List<iSCDRDLogPackage> LogsForThreadID(int threadID){
			iSCDDebuggingThreadContainerInfo info = ContainerInfoForThreadID (threadID);
			return info.logs;
		}

		public int NumberOfRunningThreads(){

			int count = 0;

			foreach (iSCDThreadContainer c in runtime.GetActiveContainerList()) {
				if (c.IsRunning ())
					count++;
			}

			return count;
		}

		public int TotalStartedThreads(){
			return totalThreadsStarted;
		}

		public int TotalMainThreadDispatches(){
			return mainThreadDispatches;
		}

		public int MainThreadPendingQueueLength(){
			return runtime.MainThreadTasksCount ();
		}
	}

	public enum iSCDRDLogType{
		Info,
		Warning,
		Error
	}

	public class iSCDDebuggingThreadContainerInfo{
		public iSCDThreadContainer container;
		public DateTime initedTime;
		public double lifeTime;
		public List<iSCDRDLogPackage> logs = new List<iSCDRDLogPackage>();

		public iSCDDebuggingThreadContainerInfo(iSCDThreadContainer container){
			this.container = container;
			initedTime = DateTime.UtcNow;
		}
	}

	public struct iSCDRDLogPackage{
		public iSCDThreadContainer sender;
		public iSCDRDLogType type;
		public object content;
		public bool isVerbose;
		public double logTime;

		public iSCDRDLogPackage(iSCDThreadContainer sender, iSCDRDLogType type, object content, double logTime, bool isVerbose){
			this.sender = sender;
			this.type = type;
			this.content = content;
			this.isVerbose = isVerbose;
			this.logTime = logTime;
		}
	}
}