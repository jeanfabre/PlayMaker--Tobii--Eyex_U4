// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using System.Collections;

using Tobii.EyeX.Framework;

/// <summary>
/// PlayMaker Proxy Component for EyePositionDataComponent.
/// </summary>
//[AddComponentMenu("PlayMaker/Addons/Tobii EyeX/Eye Position Data Proxy")]
public class PlayMakerEyexEyePositionDataProxy : MonoBehaviour {

	public FixationDataMode fixationDataMode;
	
	private EyeXHost _eyexHost;
	private IEyeXDataProvider<EyeXEyePosition> _dataProvider;
	
	/// <summary>
	/// The last eye position.
	/// </summary>
	public EyeXEyePosition LastEyePosition { get; private set; }

	public static PlayMakerEyexEyePositionDataProxy Instance;

	public static string LeftEyeValidEventName = "TOBII / EYEX / LEFT EYE VALID";
	public static string LeftEyeInValidEventName = "TOBII / EYEX / LEFT EYE INVALID";
	public static string RightEyeValidEventName = "TOBII / EYEX / RIGHT EYE VALID";
	public static string RightEyeInValidEventName = "TOBII / EYEX / RIGHT EYE INVALID";


	bool LastLeftEyeValidState = false;
	bool LastRightEyeValidState = false;

	protected void Awake()
	{
		Instance = this;
		_eyexHost = EyeXHost.GetInstance();
		_dataProvider = _eyexHost.GetEyePositionDataProvider();

	}
	
	protected void OnEnable()
	{
		_dataProvider.Start();
	}
	
	protected void OnDisable()
	{
		_dataProvider.Stop();
	}
	
	protected void Update()
	{
		LastEyePosition = _dataProvider.Last;

		if (LastEyePosition.LeftEye.IsValid != LastLeftEyeValidState)
		{
			LastLeftEyeValidState = LastEyePosition.LeftEye.IsValid;
		
			PlayMakerFSM.BroadcastEvent(LastLeftEyeValidState?LeftEyeValidEventName:LeftEyeInValidEventName);
		}

		if (LastEyePosition.RightEye.IsValid != LastRightEyeValidState)
		{
			LastRightEyeValidState = LastEyePosition.RightEye.IsValid;
			
			PlayMakerFSM.BroadcastEvent(LastRightEyeValidState?RightEyeValidEventName:RightEyeInValidEventName);
		}

	}
}
