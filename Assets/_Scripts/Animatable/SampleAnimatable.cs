using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleAnimatable : AnimatableClass {
	public const string scriptName = "SampleAnimatable";

	// List of Params
	public const string SHOW = "show";
	public const string FLOAT = "float";

	[SerializeField] private Animator sampleAnimator;

	// Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers
	[SerializeField] private bool triggerShow = false;
	[SerializeField] private bool triggerFloat = false;

	void Start () {
		MAX_DURATION = 0.5f;
		this.sampleAnimator = gameObject.GetComponent<Animator> ();
		this.ResetTriggerVariables ();
		this.isPlaying = false;

	}

	// Functions to be called by external scripts
	public void Show() {
		this.triggerShow = true;
	}

	public void Float() {
		this.triggerFloat = true;
	}

	// Always add
	public override void ResetTriggers() {
		sampleAnimator.ResetTrigger (SHOW);
		sampleAnimator.ResetTrigger (FLOAT);
	}

	void Update () {
		SwitchTriggers ();
	}

	// Always add
	public override void ResetTriggerVariables() { // Inspector
		triggerShow = false;
		triggerFloat = false;
	}

	public override void SwitchTriggers() {
		ResetTriggers ();

		if (triggerShow) {
			this.AnimationPlay ();
			sampleAnimator.SetTrigger (SHOW);
		} 
		else if (triggerFloat) {
			this.AnimationPlay ();
			sampleAnimator.SetTrigger (FLOAT);
		}
		ResetTriggerVariables ();
	}

}
