// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using Tobii.EyeX.Framework;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory("Tobii Eyex")]
	[Tooltip("Sets the GazePointDataMode. Options are Unfiltered or lightlyFiltered")]
	public class EyexGetGazePointDataMode : FsmStateAction
	{

		public FsmBool lightlyFiltered;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets the gaze point in (Unity) screen space pixels. The bottom-left of the screen/camera is (0, 0); the right-top is (pixelWidth, pixelHeight).")]
		public FsmVector2 ScreenPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip("Gets the gaze point in GUI space pixels. The top-left of the screen is (0, 0); the bottom-right is (pixelWidth, pixelHeight).")]
		public FsmVector2 GuiPosition;

		[UIHint(UIHint.Variable)]
		[Tooltip(" Gets the gaze point in the viewport coordinate system. The bottom-left of the screen/camera is (0, 0); the top-right is (1, 1).")]
		public FsmVector2 ViewPortPosition;


		[UIHint(UIHint.Variable)]
		public FsmBool isWithinScreenBound;

		[UIHint(UIHint.Variable)]
		public FsmBool isValid;

		private IEyeXDataProvider<EyeXGazePoint> _dataProvider;

		private EyeXGazePoint LastGazePoint;
		public override void Reset()
		{
			lightlyFiltered = true;
			GuiPosition = null;
			ScreenPosition = null;
			ViewPortPosition = null;
			isWithinScreenBound = null;
			isValid = null;

		}
		
		public override void OnEnter()
		{

			_dataProvider = EyeXHost.GetInstance().GetGazePointDataProvider(lightlyFiltered.Value?GazePointDataMode.LightlyFiltered:GazePointDataMode.Unfiltered);

			_dataProvider.Start();
		}

		public override void OnUpdate()
		{
			LastGazePoint = _dataProvider.Last;

			if (LastGazePoint.IsValid)
			{
				if (! GuiPosition.IsNone)
				{
					GuiPosition.Value = LastGazePoint.GUI;
				}
				
				if (! ScreenPosition.IsNone)
				{
					ScreenPosition.Value = LastGazePoint.Screen;
				}
				
				if (! ViewPortPosition.IsNone)
				{
					ViewPortPosition.Value = LastGazePoint.Viewport;
				}
				
				if (! isWithinScreenBound.IsNone)
				{
					isWithinScreenBound.Value = LastGazePoint.IsWithinScreenBounds;
				}

			}

			if (! isValid.IsNone)
			{
				isValid.Value = LastGazePoint.IsValid;
			}


		}

		public override void OnExit()
		{
			_dataProvider.Stop();
		}
	}
}