using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteAnimatable : AnimatableClass {
	public const string scriptName = "RouletteAnimatable";

	public const string SHOW = "show";
	public const string HIDE = "hide";
//	public const string RED = "red";
//	public const string BLACK = "black";

	[SerializeField] private Animator rouletteAnimator;

	// Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers
	[SerializeField] private bool triggerShow = false;
	[SerializeField] private bool triggerHide = false;
//	[SerializeField] private bool triggerRed = false;
//	[SerializeField] private bool triggerBlack = false;

	void Start () {
		MAX_DURATION = 0.5f;
		this.rouletteAnimator = gameObject.GetComponent<Animator> ();
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

//	public void Red() {
//		this.triggerRed = true;
//	}
//
//	public void Black(){
//		this.triggerBlack = true;
//	}

	public override void ResetTriggers() {
		rouletteAnimator.ResetTrigger (SHOW);
		rouletteAnimator.ResetTrigger (HIDE);
//		rouletteAnimator.ResetTrigger (RED);
//		rouletteAnimator.ResetTrigger (BLACK);
	}

	void Update () {
		SwitchTriggers ();
	}
		
	public override void ResetTriggerVariables() { // Inspector
		triggerShow = false;
		triggerHide = false;
//		triggerRed = false;
//		triggerBlack = false;
	}

	public void OnRouletteShown() {
		GetComponentInChildren<Roulette>().RouletteShown();
	}

	public void OnRouletteHidden() {
		GetComponentInChildren<Roulette>().RouletteHidden();
	}

	public override void SwitchTriggers() {
		ResetTriggers ();

		if (triggerShow) {
			this.AnimationPlay ();
			rouletteAnimator.SetTrigger (SHOW);

		} else if (triggerHide) {
			this.AnimationPlay ();
			rouletteAnimator.SetTrigger (HIDE);
		} 

//		else if (triggerRed) {
//			this.AnimationPlay ();
//			rouletteAnimator.SetTrigger (RED);
//		}

//		else if (triggerBlack) {
//			this.AnimationPlay ();
//			rouletteAnimator.SetTrigger (BLACK);
//		}
		ResetTriggerVariables ();
	}
}
