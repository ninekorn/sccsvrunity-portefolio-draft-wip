using System;
using UnityEngine;
using System.Threading;
using System.Collections;
using SPINACH.iSCentralDispatch.Internal;

namespace SPINACH.iSCentralDispatch{

	/// <summary>
	/// Welcome to iSCentralDispatch
	/// 
	/// Read commits of following methods and documentations to learn more.
	/// </summary>
	[ExecuteInEditMode]
	public class iSCentralDispatch {

		static private iSCentralDispatchRuntime runtime;

		static private iSCentralDispatchRuntime GetRuntime(){
			if (runtime) return runtime;

			runtime = iSCentralDispatchRuntime.GetDefaultRuntime ();
			return runtime;
		}

		static public bool RuntimeStarted(){
			if (runtime == null) {
				return false;
			}

			return true;
		}

		static private bool IsRuntimeStarted(){
			if (runtime == null) {
				Debug.LogError ("iSCD: No Runtime Could be found. Call InitDispatchRuntime() or DispatchNewThread(*) first.");
				return false;
			}

			return true;
		}

		/// <summary>
		/// Inits the dispatch runtime.
		/// Called this to force create a runtime instance if isn't one in scene.
		/// When you start a new thread, a runtime will be created automatically.
		/// </summary>
		static public void InitDispatchRuntime(){
			iSCentralDispatchRuntime.GetDefaultRuntime ();
		}

		/// <summary>
		/// Dispatch to new thread.
		/// 
		/// This will start a new thread to execute your task.
		/// </summary>
		/// <param name="task">Task to execute in main thread</param>
		static public int DispatchNewThread(Action task){
			return GetRuntime ().StartNewThread (task);
		}

		/// <summary>
		/// Dispatch to new thread.
		/// 
		/// This will start a new thread to execute your task.
		/// </summary>
		/// <param name="task">Task to execute in main thread</param>
		static public int DispatchNewThread(string name, Action task){
			return GetRuntime ().StartNewThread (name, task);
		}

		/// <summary>
		/// Dispatch to new thread with a parameter.
		/// 
		/// This will start a new thread to execute your task.
		/// </summary>
		/// <returns>The new thread.</returns>
		/// <param name="task">Task.</param>
		/// <param name="parameter">Parameter.</param>
		static public int DispatchNewThread(Action<object> task, object parameter){
			return GetRuntime ().StartNewThread (task,parameter);
		}

		/// <summary>
		/// Dispatch to new thread with a parameter.
		/// 
		/// This will start a new thread to execute your task.
		/// </summary>
		/// <returns>The new thread.</returns>
		/// <param name="task">Task.</param>
		/// <param name="parameter">Parameter.</param>
		static public int DispatchNewThread(string name, Action<object> task, object parameter){
			return GetRuntime ().StartNewThread (name, task, parameter);
		}

		/// <summary>
		/// Dispatch to main thread without blocking the current thread
		/// 
		/// This will use main thread to execute the 'task' but not block the current thread.
		/// </summary>
		/// <param name="task">Task.</param>
		/// <param name="finishCallback">Finish callback.</param>
		static public void DispatchTaskToMainThread(Action task, Action<int> finishCallback){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().DispatchTaskToMainThread (task, finishCallback);
		}

		/// <summary>
		/// Dispatch to main thread and block current thread.
		/// 
		/// This will use main thread to execute the 'task' and block the current thread.
		/// After main thread finished executing specified task, current thread will resume.
		/// </summary>
		/// <param name="task">Task to execute in main thread</param>
		static public void DispatchMainThread(Action task){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().DispatchMainThread (task);
		}

		/// <summary>
		/// Resume a specified thread.
		/// </summary>
		/// <param name="id">Identifier.</param>
		static public void ResumeThread(int id){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().ResumeThread (id);
		}

		/// <summary>
		/// Pause a specified thread.
		/// </summary>
		/// <param name="id">Identifier.</param>
		static public void PauseThread(int id){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().PauseThread (id);
		}

		static public void PauseThread(int id, float time){
			if (!IsRuntimeStarted()) return;


		}

		/// <summary>
		/// Abort a specified thread.
		/// </summary>
		/// <param name="id">Identifier.</param>
		static public void AbortThread(int id){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().AbortThread (id);
		}

		/// <summary>
		/// Instantiate the specified prefab at certain position and rotation.
		/// 
		/// This function will block caller thread until the prefab is instantiated and return.
		/// </summary>
		/// <param name="prefab">Prefab to instantiate.</param>
		/// <param name="position">Position.</param>
		/// <param name="rotation">Rotation.</param>
		static public UnityEngine.Object Instantiate(GameObject prefab, Vector3 position, Quaternion rotation){
			if (!IsRuntimeStarted()) return null;

			return GetRuntime ().ThreadedInstantiate (prefab, position, rotation);
		}

		/// <summary>
		/// Instantiate the specified prefab at the Transform's position.
		/// 
		/// This function will block caller thread until the prefab is instantiated and return.
		/// </summary>
		/// <param name="prefab">Prefab.</param>
		/// <param name="place">Place.</param>
		/// <param name="setChild">Should instance become a child of "place"?</param>
		static public UnityEngine.Object Instantiate(GameObject prefab, Transform place, bool setChild){
			if (!IsRuntimeStarted()) return null;

			return GetRuntime ().ThreadedInstantiate (prefab, place, setChild);
		}

		/// <summary>
		/// Instantiates the specified prefab at the Transform's position.
		/// 
		/// This function will not block the caller thread and returns nothing.
		/// When your prefab is spawned, callback will be issued with instance of the prefab.
		/// </summary>
		/// <param name="prefab">Prefab.</param>
		/// <param name="place">Place.</param>
		/// <param name="setChild">Should instance become a child of "place"?</param>
		/// <param name="callback">Instantiated callback</param>
		static public void InstantiateAsync(GameObject prefab, Transform place, bool setChild, Action<UnityEngine.Object> callback){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().ThreadedInstantiateTasked (prefab, place, setChild, callback);
		}

		/// <summary>
		/// Call LifeReport to should iS.CentralDispatch that your thread is alive.
		/// </summary>
		static public void LifeReport(){
			if (!IsRuntimeStarted()) return;

			if (IsInMainThread ()) {
				throw new Exception ("iSCentralDispatch: LifeReport() can not be called in main thread");
			}
			GetRuntime ().LifeReport ();
		}

//		/// <summary>
//		/// Set this to a higher value will make thread sync available for more concurrent thread while costing more time in main thread.
//		/// </summary>
//		/// <param name="task">Task.</param>
//		static public void SetMaxConcurrentMainThreadTask(int task){
//			if (!IsRuntimeStarted()) return;
//
//			GetRuntime ().SetMaxConcurrentMainThreadTask (task);
//		}

//		/// <summary>
//		/// Return the setting of current concurrent main thread task count
//		/// </summary>
//		/// <returns>The concurrent main thread task.</returns>
//		static public int MaxConcurrentMainThreadTask(){
//			if (!IsRuntimeStarted()) return -1;
//
//			return GetRuntime ().ConcurrentMainThreadTask ();
//		}

		static public void SetTargetFramerate(int rate){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().SetTargetFramerate (rate);
		}

		static public void SetPriorityForThread(int threadID, iSCDThreadPriority p){
			if (!IsRuntimeStarted()) return;

			GetRuntime ().SetPriorityForThread (threadID, (System.Threading.ThreadPriority)p);
		}

		static public bool IsInMainThread(){
			return Thread.CurrentThread.Name == null;
		}
	}

	public enum iSCDThreadPriority{
		VeryHigh,
		High,
		Normal,
		Low,
		VeryLow
	}
}