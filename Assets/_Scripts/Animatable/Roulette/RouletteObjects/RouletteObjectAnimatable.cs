using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteObjectAnimatable : AnimatableClass {
	public const string scriptName = "RouletteObjectAnimatable";

	public const string SHOW = "show";
	public const string HIDE = "hide";
	public const string FLOAT = "float";

	[SerializeField] private Animator rouletteObjectAnimator;

	// Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers
	[SerializeField] private bool triggerShow = false;
	[SerializeField] private bool triggerHide = false;
	[SerializeField] private bool triggerFloat = false;

	void Start () {
		MAX_DURATION = 0.5f;
		this.rouletteObjectAnimator = gameObject.GetComponent<Animator> ();
		this.ResetTriggerVariables ();
		this.isPlaying = false;
	}

	// Functions to be called by external scripts
	public void Show() {
		this.triggerShow = true;
	}

	public void Hide() {
		this.triggerHide = true;
	}

	public void Float(){
		this.triggerFloat = true;
	}

	public override void ResetTriggers() {
		rouletteObjectAnimator.ResetTrigger (SHOW);
		rouletteObjectAnimator.ResetTrigger (HIDE);
		rouletteObjectAnimator.ResetTrigger (FLOAT);
	}

	void Update () {
		SwitchTriggers ();
	}

	public override void ResetTriggerVariables() { // Inspector
		triggerShow = false;
		triggerHide = false;
		triggerFloat = false;
	}

	public override void SwitchTriggers() {
		ResetTriggers ();

		if (triggerShow) {
			this.AnimationPlay ();
			rouletteObjectAnimator.SetTrigger (SHOW);

		} else if (triggerHide) {
			this.AnimationPlay ();
			rouletteObjectAnimator.SetTrigger (HIDE);
		} else if (triggerFloat) {
			this.AnimationPlay ();
			rouletteObjectAnimator.SetTrigger (FLOAT);
		}

		ResetTriggerVariables ();
	}
}
