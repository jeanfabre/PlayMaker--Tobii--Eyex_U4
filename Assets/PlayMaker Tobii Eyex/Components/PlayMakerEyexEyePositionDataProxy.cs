// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using UnityEngine;
using System.Collections;

using Tobii.EyeX.Framework;

/// <summary>
/// PlayMaker Proxy Component for EyePositionDataComponent.
/// </summary>
[AddComponentMenu("PlayMaker/Addons/Tobii EyeX/Eye Position Data Proxy")]
public class PlayMakerEyexEyePositionDataProxy : MonoBehaviour {

	public FixationDataMode fixationDataMode;
	
	private EyeXHost _eyexHost;
	private IEyeXDataProvider<EyeXEyePosition> _dataProvider;
	
	/// <summary>
	/// The last eye position.
	/// </summary>
	public EyeXEyePosition LastEyePosition { get; private set; }

	public static PlayMakerEyexEyePositionDataProxy Instance;

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
	}
}
