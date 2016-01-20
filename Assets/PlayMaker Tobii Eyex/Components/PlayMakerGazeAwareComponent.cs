// (c) Copyright HutongGames, LLC 2010-2015. All rights reserved.

using System.Collections.Generic;
using UnityEngine;

using HutongGames.PlayMaker;

using HutongGames.PlayMaker.Ecosystem.Utils;

/// <summary>
/// Component that encapsulates the <see cref="EyeXGazeAware"/> behavior and fire PlayMaker Events.
/// </summary>
[AddComponentMenu("PlayMaker/Addons/Tobii EyeX/PlayMaker Gaze Aware")]
public class PlayMakerGazeAwareComponent : EyeXGameObjectInteractorBase
{
	// Delay between first glance and gaze aware event response
	public int delayInMilliseconds;


	public PlayMakerEventTarget eventTarget;
	
	public PlayMakerEvent GlazeStartedEvent;
	
	public PlayMakerEvent GlazeEndedEvent;

	public bool debug;


	bool _hasGaze;

	/// <summary>
	/// Gets a value indicating whether the user's eye-gaze is within the bounds of the interactor.
	/// </summary>
	public bool HasGaze { get; private set; }


	protected override void Update()
	{
		base.Update();
		
		HasGaze = GameObjectInteractor.HasGaze();

		if (_hasGaze!=HasGaze)
		{
			_hasGaze = HasGaze;
			if (HasGaze)
			{
				if (debug) Debug.Log(this.gameObject.name+": PlayMakerGazeAwareComponent : "+GlazeStartedEvent+" "+eventTarget);

				GlazeStartedEvent.SendEvent(null,eventTarget);
			}else{
				if (debug) Debug.Log(this.gameObject.name+": PlayMakerGazeAwareComponent : "+GlazeEndedEvent+" "+eventTarget);
				GlazeEndedEvent.SendEvent(null,eventTarget);
			}
		}
	}
	
	protected override IList<IEyeXBehavior> GetEyeXBehaviorsForGameObjectInteractor()
	{
		return new List<IEyeXBehavior>(new[] { new EyeXGazeAware() { DelayTime = delayInMilliseconds }});
	}
}
