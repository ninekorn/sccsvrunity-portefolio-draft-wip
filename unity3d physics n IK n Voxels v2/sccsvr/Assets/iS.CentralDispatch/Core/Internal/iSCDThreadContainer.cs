using System;
using System.Threading;
using System.Collections;

namespace SPINACH.iSCentralDispatch.Internal{

	/// <summary>
	/// iSCD thread container.
	/// 
	/// This is the basic thread container for iSCD and used for iSCD internally.
	/// </summary>

	public class iSCDThreadContainer : object {

		public string iSCD_Name;

		public int iSCD_RuntimeID{
			set{
				runtimeIDCache = value;
				thread.Name = value.ToString ();
			}
			get{
				return runtimeIDCache;
			}
		}

		public object parameter;

		bool started = false;
		bool waiting = false;
		int runtimeIDCache;

		private DateTime lastLifeTimeReport;
		private Thread thread;
		private ManualResetEvent resetEvent;

//		public iSCDThreadContainer(ThreadStart task, int runtimeID){
//			resetEvent = new ManualResetEvent (true);
//			thread = new Thread (task);
//
//			iSCD_RuntimeID = runtimeID;
//			iSCD_Name = "Unnamed Thread";
//
//			parameter = null;
//		}

//		public iSCDThreadContainer(ParameterizedThreadStart task, int runtimeID, object para){
//			resetEvent = new ManualResetEvent (true);
//			thread = new Thread (task);
//
//			parameter = para;
//			iSCD_RuntimeID = runtimeID;
//			iSCD_Name = "Unnamed Thread";
//		}

		public iSCDThreadContainer(string threadName, ThreadStart task, int runtimeID){
			resetEvent = new ManualResetEvent (true);
			thread = new Thread (task);
			iSCD_RuntimeID = runtimeID;
			iSCD_Name = threadName;

			parameter = null;
			lastLifeTimeReport = DateTime.UtcNow;
		}

		public iSCDThreadContainer(string threadName, ParameterizedThreadStart task, int runtimeID, object para){
			resetEvent = new ManualResetEvent (true);
			thread = new Thread (task);

			parameter = para;
			iSCD_RuntimeID = runtimeID;
			iSCD_Name = threadName;
			lastLifeTimeReport = DateTime.UtcNow;
		}


		public bool IsRunning(){
			return started;
		}

		public bool IsWaiting(){
			return waiting;
		}

		public bool IsResponding(){
			return (DateTime.UtcNow - lastLifeTimeReport).TotalSeconds < 0.1d;
		}

		public void Start(){
			started = true;

			if (parameter != null) thread.Start (parameter);
			else thread.Start ();
		}

		public void Pause(){
			if (waiting) return;
			waiting = true;
			resetEvent.Reset ();
		}

		public void Resume(){
			if (!waiting) return;
			waiting = false;
			resetEvent.Set ();
		}

		public void UpdateLifeReport(){
			lastLifeTimeReport = DateTime.UtcNow;
			WaitForResetEvent ();
		}

		public void AbortAndDestroy(){
			if (!started) return;
			thread.Abort ();

			Destroy ();
		}

		public void Destroy(){
			if (!started) return;
			waiting = false;
			started = false;
			thread = null;
			resetEvent = null;

			System.GC.Collect ();
		}

		public void WaitForResetEvent(){
			resetEvent.WaitOne ();
		}

		public void SetPriority(ThreadPriority p){
			thread.Priority = p;
		}
	}

}