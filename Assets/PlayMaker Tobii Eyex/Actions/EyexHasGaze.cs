// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tobii Eyex")]
	[Tooltip("Check if the GameObject has Gaze. You'll need to have either GazeAwareComponent attached to the GameObject or PlayMakerGazeAwareComponent component.")]
	public class EyexHasGaze : FsmStateAction
	{
		[Tooltip("The GameObject that is gaze aware. It should have either a GazeAwareComponent or a PlayMakerGazeAwareComponent component attached")]
		public FsmOwnerDefault gameObject;
		
		[UIHint(UIHint.Variable)]
		public FsmBool hasGaze;
		
		public FsmEvent onGazeStartedEvent;

		public FsmEvent onGazeEndedEvent;

		public bool everyFrame;

		GameObject _go;

		GazeAwareComponent _GazeAwareComponent;
		PlayMakerGazeAwareComponent _PlayMakerGazeAwareComponent;

		bool _hasGaze;

		public override void Reset()
		{
			hasGaze = null;
			onGazeStartedEvent = null;
			onGazeEndedEvent = null;
		}
		
		public override void OnEnter()
		{
			_getComp();

			_hasGaze = HasGaze();
			hasGaze.Value = _hasGaze;

			CheckForGaze();
			
			if (!everyFrame)
			{
				Finish();
			}
		}

		void _getComp()
		{
			_go = Fsm.GetOwnerDefaultTarget(gameObject);
			if (_go == null)
			{
				return;
			}
			_GazeAwareComponent = _go.GetComponent<GazeAwareComponent>();
			_PlayMakerGazeAwareComponent = _go.GetComponent<PlayMakerGazeAwareComponent>();
		}
		
		public override void OnUpdate()
		{
			CheckForGaze();
		}
		
		
		EyeXEyePosition _pos;

		bool HasGaze()
		{
			if (_PlayMakerGazeAwareComponent!=null)
			{
				return _PlayMakerGazeAwareComponent.HasGaze;
			}

			if (_GazeAwareComponent!=null)
			{
				return _GazeAwareComponent.HasGaze;
			}

			return false;
		}

		void CheckForGaze()
		{
			bool _newHasGaze = HasGaze();

			if (_hasGaze!=_newHasGaze)
			{
				_hasGaze = _newHasGaze;

				hasGaze.Value = _hasGaze;

				if (_hasGaze)
				{
					Fsm.Event(onGazeStartedEvent);
				}else{
					Fsm.Event(onGazeEndedEvent);
				}
			}
		}
		
	}
}