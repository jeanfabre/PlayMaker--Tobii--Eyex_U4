// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tobii Eyex")]
	[Tooltip("Check if the User is detected by the device.")]
	public class EyexIsUserPresent : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		public FsmBool isPresent;
		
		public FsmEvent onUserDetectedEvent;
		
		public FsmEvent onUserLostEvent;
		
		public bool everyFrame;
		
		GameObject _go;

		bool _isPresent;

		private EyeXHost _eyexHost;

		public override void Reset()
		{
			isPresent = null;
			onUserDetectedEvent = null;
			onUserLostEvent = null;
		}
		
		public override void OnEnter()
		{
			_eyexHost = EyeXHost.GetInstance();

			_isPresent = GetIsPresent();
			isPresent.Value = _isPresent;
			
			CheckForPresence();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			CheckForPresence();
		}
		
		bool GetIsPresent()
		{
			if (_eyexHost!=null)
			{
				return _eyexHost.UserPresence == EyeXUserPresence.Present;
			}
			return false;
		}
		
		void CheckForPresence()
		{
			bool _newIsPresent = GetIsPresent();
			
			if (_isPresent!=_newIsPresent)
			{
				_isPresent = _newIsPresent;
				
				isPresent.Value = _isPresent;
				
				if (_isPresent)
				{
					Fsm.Event(onUserDetectedEvent);
				}else{
					Fsm.Event(onUserLostEvent);
				}
			}
		}
		
	}
}