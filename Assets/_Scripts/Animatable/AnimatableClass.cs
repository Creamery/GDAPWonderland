using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatableClass : MonoBehaviour, Animatable {

	public abstract void ResetTriggers ();
	public abstract void ResetTriggerVariables ();
	public abstract void SwitchTriggers ();


	// You have to set IsPlaying on and off through AnimationEvents
	[SerializeField] protected float MAX_DURATION = 0.5f;
	[SerializeField] protected bool isPlaying;
	protected float timeStart;

	void Update () {
		if (IsPlaying ()) {
			if (this.timeStart >= (Time.deltaTime + MAX_DURATION)) {
				this.AnimationStop ();
				Debug.Log ("MAX DURATION");
			}
		}
	}

	public bool IsPlaying() {
		return this.isPlaying;
	}

	public void AnimationPlay() {
		this.isPlaying = true;
		this.timeStart = Time.deltaTime;
	}

	public void AnimationStop() {
		this.isPlaying = false;
	}
}
