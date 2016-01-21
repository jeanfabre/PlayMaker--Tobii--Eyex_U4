// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tobii Eyex")]
	[Tooltip("Get the current Eyes position.")]
	public class EyexGetEyesPosition : FsmStateAction
	{

		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		[UIHint(UIHint.Variable)]
		public FsmBool leftEyeIsValid;

		[UIHint(UIHint.Variable)]
		public FsmVector3 leftEyePosition;

		[UIHint(UIHint.Variable)]
		public FsmVector3 leftEyePositionNormalized;

		[UIHint(UIHint.Variable)]
		public FsmBool rightEyeIsValid;

		[UIHint(UIHint.Variable)]
		public FsmVector3 rightEyePosition;

		[UIHint(UIHint.Variable)]
		public FsmVector3 righEyePositionNormalized;

		public FsmEvent errorEvent;

		public bool everyFrame;
	
		public override void Reset()
		{
			isValid = null;

			leftEyeIsValid = null;
			rightEyeIsValid = null;

			leftEyePosition = null;
			rightEyePosition = null;

			leftEyePositionNormalized = null;
			righEyePositionNormalized = null;

			errorEvent = null;
		}
		
		public override void OnEnter()
		{

			GetEyesPosition();

			if (!everyFrame)
			{
				Finish();
			}
		}

		public override void OnUpdate()
		{
			GetEyesPosition();
		}


		EyeXEyePosition _pos;

		void GetEyesPosition()
		{
			if (PlayMakerEyexEyePositionDataProxy.Instance == null)
			{
				LogError("Missing 'PlayMaker Tobii Eyex' prefab instance in Scene!");
				Fsm.Event(errorEvent);
				return;
			}

			_pos = PlayMakerEyexEyePositionDataProxy.Instance.LastEyePosition;

			if (!isValid.IsNone)
			{
				isValid.Value = _pos!=null?_pos.IsValid:false;
			}

			if ( _pos!=null && _pos.IsValid)
			{
				if (!leftEyeIsValid.IsNone)
				{
					leftEyeIsValid.Value = _pos.LeftEye.IsValid;
				}

				if (!leftEyePosition.IsNone && _pos.LeftEye.IsValid)
				{
					leftEyePosition.Value = new Vector3(_pos.LeftEye.X,_pos.LeftEye.Y,_pos.LeftEye.Z);
				}

				if (!leftEyePositionNormalized.IsNone && _pos.LeftEye.IsValid)
				{
					leftEyePositionNormalized.Value = new Vector3(_pos.LeftEyeNormalized.X,_pos.LeftEyeNormalized.Y,_pos.LeftEyeNormalized.Z);
				}

				if (!rightEyeIsValid.IsNone)
				{
					rightEyeIsValid.Value = _pos.RightEye.IsValid;
				}

				if (!rightEyePosition.IsNone && _pos.RightEye.IsValid)
				{
					rightEyePosition.Value = new Vector3(_pos.RightEye.X,_pos.RightEye.Y,_pos.RightEye.Z);
				}

				if (!righEyePositionNormalized.IsNone && _pos.RightEye.IsValid)
				{
					righEyePositionNormalized.Value = new Vector3(_pos.RightEyeNormalized.X,_pos.RightEyeNormalized.Y,_pos.RightEyeNormalized.Z);
				}
			}else{
				if (!leftEyeIsValid.IsNone)
				{
					leftEyeIsValid.Value = false;
				}

				if (!rightEyeIsValid.IsNone)
				{
					rightEyeIsValid.Value = false;
				}
			}
		}

	}
}