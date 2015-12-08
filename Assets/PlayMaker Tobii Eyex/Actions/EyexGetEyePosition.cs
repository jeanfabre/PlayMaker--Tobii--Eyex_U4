// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tobii Eyex")]
	[Tooltip("Get the current Eyes position.")]
	public class EyexGetEyesPosition : FsmStateAction
	{

		public FsmBool isValid;

		public FsmBool leftEyeIsValid;
		public FsmVector3 leftEyePosition;
		public FsmVector3 leftEyePositionNormalized;

		public FsmBool rightEyeIsValid;
		public FsmVector3 rightEyePosition;
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

		EyeXEyePosition _pos;

		void GetEyesPosition()
		{
			if (PlayMakerEyexEyePositionDataProxy.Instance == null)
			{
				LogError("Missing PlayMakerEyexEyePositionDataProxy!");
				Fsm.Event(errorEvent);
				return;
			}

			_pos = PlayMakerEyexEyePositionDataProxy.Instance.LastEyePosition;

			if (!isValid.IsNone)
			{
				isValid.Value = _pos.IsValid;
			}

			if (!leftEyeIsValid.IsNone)
			{
				leftEyeIsValid.Value = _pos.LeftEye.IsValid;
			}

			if (!leftEyePosition.IsNone)
			{
				leftEyePosition.Value = new Vector3(_pos.LeftEye.X,_pos.LeftEye.Y,_pos.LeftEye.Z);
			}

			if (!leftEyePositionNormalized.IsNone)
			{
				leftEyePositionNormalized.Value = new Vector3(_pos.LeftEyeNormalized.X,_pos.LeftEyeNormalized.Y,_pos.LeftEyeNormalized.Z);
			}

			if (!rightEyeIsValid.IsNone)
			{
				rightEyeIsValid.Value = _pos.RightEye.IsValid;
			}

			if (!rightEyePosition.IsNone)
			{
				rightEyePosition.Value = new Vector3(_pos.RightEye.X,_pos.RightEye.Y,_pos.RightEye.Z);
			}

			if (!righEyePositionNormalized.IsNone)
			{
				righEyePositionNormalized.Value = new Vector3(_pos.RightEyeNormalized.X,_pos.RightEyeNormalized.Y,_pos.RightEyeNormalized.Z);
			}
		}

	}
}