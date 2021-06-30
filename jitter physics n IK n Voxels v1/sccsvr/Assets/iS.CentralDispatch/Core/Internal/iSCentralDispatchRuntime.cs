using System;
using UnityEngine;
using System.Threading;
using System.Collections;
using SPINACH.iSCentralDispatch;
using System.Collections.Generic;
using SPINACH.iSCentralDispatch.Internal;

namespace SPINACH.iSCentralDispatch.Internal{

	public class iSCentralDispatchRuntime : MonoBehaviour {

		public float targetedFramerate = 60f;

		private iSCentralDispatchRuntimeDebugger debugger;
		private int containerRuntimeIDPointer = 0;
		private List<iSCDThreadContainer> activeContainers = new List<iSCDThreadContainer>();
		private List<TaskInformationPackage> registeredTasks = new List<TaskInformationPackage>();

		private DateTime initedTime;

		static public iSCentralDispatchRuntime self;

		static public void Init(){
			GameObject obj = new GameObject("iSCentralDispatchRuntime");
			self = obj.AddComponent<iSCentralDispatchRuntime> ();
		}

		static public bool IsRuntimeInitialized(){
			return self != null;
		}

		static public iSCentralDispatchRuntime GetDefaultRuntime(){
			if (self) return self;

			Init ();
			return self;
		}

		public void SetTargetFramerate(int rate){
			targetedFramerate = (float)rate;
		}

		#region unity called method
		void Awake(){
			self = this;

			RuntimeIntegrityCheck ();
			initedTime = DateTime.UtcNow;
		}

		//int tasksDoneThisFrame = 0;
		void Update(){

			long startedTime = iSCDTimeUtilities.MillisecondsSinceJanFirst1970 ();
			long maxTimeCost = (long)(1f/targetedFramerate * 100f);

			long lastestStopTime = startedTime + maxTimeCost;

			lock (registeredTasks) {
				while (lastestStopTime > iSCDTimeUtilities.MillisecondsSinceJanFirst1970()) {
					//Debug.Log ("s" + iSCDTimeUtilities.MillisecondsSinceJanFirst1970 ().ToString() + "t" + lastestStopTime);
					if (registeredTasks.Count <= 0) break;

					TaskInformationPackage package = registeredTasks [0];

					try{
						if (package.task != null) package.task ();
						if (package.finishCallback != null) package.finishCallback ();
						if (package.taskFinishCallback != null) package.taskFinishCallback (package.callerThreadID);
					}
					catch (Exception e){
						Debug.LogError ("iSCDRuntime: Uncatched exception raised in main thread queue, aborting thread " + package.callerThreadID.ToString());
						Debug.LogError ("Exception: " + e.ToString());
						AbortThread (package.callerThreadID);

					}
					finally{
						registeredTasks.RemoveAt (0);
						//tasksDoneThisFrame++;
					}
				}
			}
			//Debug.Log (tasksDoneThisFrame);
			//tasksDoneThisFrame = 0;
		}

		void OnDestroy(){
			foreach (iSCDThreadContainer container in activeContainers) {
				container.AbortAndDestroy ();
			}
		}

		#endregion

		/// <summary>
		/// Runtimes integrity check.
		/// 
		/// We do a simple check at setup here for runtime integrity.
		/// </summary>
		void RuntimeIntegrityCheck(){

			iSCentralDispatchRuntime[] runtimes = Resources.FindObjectsOfTypeAll<iSCentralDispatchRuntime> ();
			if (runtimes.Length > 1) {
				Destroy (gameObject);
				throw new Exception ("MultiRuntimeFoundException: More than one instance of iSCentralDispatchRuntime found.");
			}
				
		}

		public void EnableDebugger(){
			if (IsDebugging()) return;
			if (!iSCentralDispatch.IsInMainThread ()) {
				throw new Exception ("iSCDRuntime: EnableDebugger() can only be called in main thread");
			}
				
			debugger = gameObject.AddComponent<iSCentralDispatchRuntimeDebugger> ();
		}

		public void DisableDebugger(){
			if (!iSCentralDispatch.IsInMainThread ()) {
				throw new System.Exception ("iSCDRuntime: DisableDebugger() can only be called in main thread");
			}

			if (!debugger) return;

			Destroy (debugger);
		}

		public bool IsDebugging(){
			return debugger != null;
		}

		public iSCentralDispatchRuntimeDebugger GetDebugger(){
			return debugger;
		}

		public DateTime RuntimeInitalizedTime(){
			return initedTime;
		}

		public List<iSCDThreadContainer> GetActiveContainerList(){
			return activeContainers;
		}

		public int ActiveContainerCount(){
			return activeContainers.Count;
		}

		public int MainThreadTasksCount(){
			return registeredTasks.Count;
		}

		#region starting new thread

		public int StartNewThread(Action task){
			
			return StartNewThread("Unnamed", task);
		}

		public int StartNewThread(string name, Action task){

			int assignedID = AssignNewIDForContainer ();

			Action taskWithCollection = () => {
				task();
				CollectThread(assignedID);
			};

			iSCDThreadContainer container = new iSCDThreadContainer (name, taskWithCollection.Invoke, assignedID);
			container.iSCD_Name = name;

			activeContainers.Add (container);

			container.Start ();

			if (IsDebugging ()) {
				debugger.CountThreads (container);
			}

			return assignedID;
		}

		public int StartNewThread(Action<object> task, object para){
			
			return StartNewThread("Unnamed",task, para);
		}

		public int StartNewThread(string name, Action<object> task, object para){

			int assignedID = AssignNewIDForContainer ();

			Action<object> taskWithCollection = (object p) => {
				task(p);
				CollectThread(assignedID);
			};

			iSCDThreadContainer container = new iSCDThreadContainer (name, taskWithCollection.Invoke, assignedID, para);
			container.iSCD_Name = name;

			activeContainers.Add (container);

			container.Start ();

			if (IsDebugging ()) debugger.CountThreads (container);

			return assignedID;
		}

		#endregion

		#region thread control

		public void PauseThread(int threadRuntimeID){
			iSCDThreadContainer c = ContainerForID (threadRuntimeID);

			c.Pause ();
			if (Thread.CurrentThread.Name != null && int.Parse (Thread.CurrentThread.Name) == threadRuntimeID) c.WaitForResetEvent ();
		}

		public void PauseThread(int threadRuntimeID, float time){
			PauseThread (threadRuntimeID);

			StartCoroutine (ResumeThreadTimed (threadRuntimeID, time));
		}

		IEnumerator ResumeThreadTimed(int threadRuntimeID, float time){
			yield return new WaitForSeconds (time);
			ResumeThread (threadRuntimeID);
		}

		public void ResumeThread(int threadRuntimeID){
			ContainerForID (threadRuntimeID).Resume ();
		}

		public void AbortThread(int threadRuntimeID){
			int index = ContainerIDForContainerPoolID (threadRuntimeID);
			if (index == -1) {
				return;
			}
			activeContainers [index].AbortAndDestroy ();

			if (!IsDebugging ())
				activeContainers.RemoveAt (index);
			else
				debugger.RegisterThreadAbort (threadRuntimeID);
		}

		public void SetPriorityForThread(int threadID, System.Threading.ThreadPriority p){
			iSCDThreadContainer container = ContainerForID (threadID);
			container.SetPriority (p);
		}

		public void CollectThread(int threadRuntimeID){
			int index = ContainerIDForContainerPoolID (threadRuntimeID);
			activeContainers [index].Destroy ();

			if (!IsDebugging ())
				activeContainers.RemoveAt (index);
			else
				debugger.RegisterThreadAbort (threadRuntimeID);
		}

		#endregion

		int AssignNewIDForContainer(){
			containerRuntimeIDPointer++;
			return containerRuntimeIDPointer;
		}

		iSCDThreadContainer ContainerForName(string name){
			foreach (iSCDThreadContainer container in activeContainers) {
				if (container.iSCD_Name == name)
					return container;
			}

			throw new System.Exception ("ErrorThreadName: Requested thread with name is not exist.");
		}

		public iSCDThreadContainer ContainerForID(int id){
				foreach (iSCDThreadContainer container in activeContainers) {
					if (container.iSCD_RuntimeID == id)
						return container;
				}

			throw new System.Exception ("ErrorThreadName: Requested thread with ID is not exist.");
		}

		int ContainerIDForContainerPoolID(int id){
			for (int i = 0; i < activeContainers.Count; i++) {
				if (activeContainers [i].iSCD_RuntimeID == id) {
					return i;
				}
			}

			return -1;
		}

		#region thread specified methods

		public void LifeReport(){
			iSCDThreadContainer container = ContainerForID (int.Parse (Thread.CurrentThread.Name));
			container.UpdateLifeReport ();
		}

		public void DispatchMainThread(Action task){
			if (Thread.CurrentThread.Name == null) {
				throw new System.Exception ("iSCDRuntime: Already in main thread");
			}

			Monitor.Enter (registeredTasks);

			iSCDThreadContainer container = ContainerForID (int.Parse (Thread.CurrentThread.Name));
			Monitor.Enter (container);

			Action callback = () => {
				container.Resume ();
				//iSCDTools.LogVerbose("ISCD Runtime : " + iSCDTools.FormatThreadName(container) + " dispatched back from main thread.");
			};

			TaskInformationPackage package = new TaskInformationPackage (task, callback, container.iSCD_RuntimeID);
			registeredTasks.Add (package);
			Monitor.Exit (registeredTasks);
			//iSCDTools.LogVerbose("ISCD Runtime : " + iSCDTools.FormatThreadName(container) + " ready to dispatch to main thread.");

			container.Pause ();
			container.WaitForResetEvent ();
			Monitor.Exit (container);

			if (IsDebugging ()) debugger.CountMainThreadDispatchs ();
		}

		public void DispatchTaskToMainThread(Action task, Action<int> taskFinishCallback){

			Monitor.Enter (registeredTasks);

			iSCDThreadContainer container = ContainerForID (int.Parse(Thread.CurrentThread.Name));
			Monitor.Enter (container);

			TaskInformationPackage package;


			package = new TaskInformationPackage (task, null, taskFinishCallback, container.iSCD_RuntimeID);
			registeredTasks.Add (package);
			Monitor.Exit (registeredTasks);

			Thread.Sleep (1);

			Monitor.Exit (container);

			if (IsDebugging ()) debugger.CountMainThreadDispatchs ();
		}

		#endregion

		#region helper methods

		public UnityEngine.Object ThreadedInstantiate(GameObject prefab, Vector3 position, Quaternion rotation){
			iSCDThreadContainer container;
			lock (activeContainers) container = ContainerForID (int.Parse (Thread.CurrentThread.Name));

			TaskInformationPackage package = new TaskInformationPackage ();
			package.task = () => {
				package.returns = Instantiate (prefab, position, rotation);
				package.finished = true;
			};

			lock (registeredTasks) registeredTasks.Add (package);

			while (!package.finished);

			return package.returns;
		}

		public UnityEngine.Object ThreadedInstantiate(GameObject prefab, Transform place, bool setChild){
			
			Monitor.Enter (registeredTasks);

			iSCDThreadContainer container;
			container = ContainerForID (int.Parse (Thread.CurrentThread.Name));

			Monitor.Enter (container);

			TaskInformationPackage package = new TaskInformationPackage ();
			package.task = () => {
				GameObject obj = Instantiate (prefab, place.position, place.rotation) as GameObject;
				package.returns = obj;

				if(setChild) obj.transform.SetParent(place);

				package.finished = true;

				container.Resume();
			};

			registeredTasks.Add (package);

			Monitor.Exit (container);
			Monitor.Exit (registeredTasks);

			container.Pause ();
			container.WaitForResetEvent ();

			return package.returns;
		}

		public void ThreadedInstantiateTasked(GameObject prefab, Transform place, bool setChild, Action<UnityEngine.Object> callback){

			Monitor.Enter (registeredTasks);

			iSCDThreadContainer container;
			container = ContainerForID (int.Parse (Thread.CurrentThread.Name));

			Monitor.Enter (container);

			TaskInformationPackage package = new TaskInformationPackage ();
			package.task = () => {
				GameObject obj = Instantiate (prefab, place.position, place.rotation) as GameObject;
				package.returns = obj;

				if(setChild) obj.transform.SetParent(place);

				package.finished = true;

				callback(package.returns);
			};

			registeredTasks.Add (package);

			Monitor.Exit (container);
			Monitor.Exit (registeredTasks);
		}

		#endregion
	}

	public class TaskInformationPackage{
		public int callerThreadID;
		public Action task;
		public Action finishCallback;
		public Action<int> taskFinishCallback;
		public bool finished = false;
		public UnityEngine.Object returns;

		public TaskInformationPackage(){
		}

		public TaskInformationPackage(Action task, Action finishCallback, int callerThreadID){
			this.task = task;
			this.finishCallback = finishCallback;
			this.taskFinishCallback = null;
			this.callerThreadID = callerThreadID;
		}

		public TaskInformationPackage(Action task, Action finishCallback, Action<int> taskFinishCallback, int callerThreadID){
			this.task = task;
			this.finishCallback = finishCallback;
			this.taskFinishCallback = taskFinishCallback;
			this.callerThreadID = callerThreadID;
		}
	}
}
