using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePanelAnimatable : AnimatableClass {
    public const string scriptName = "RulePanelAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
	public const string SHAKE = "shake";


    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.3f + f_offset;
    public const float f_HIDE = 0.3f + f_offset;


    [SerializeField] private Animator attackPanelAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
	[SerializeField] private bool triggerShake = false;

    void Start() {
        MAX_DURATION = 0.5f;
        if (this.attackPanelAnimatable == null) {
            this.attackPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public void PlayError() {
        SoundManager.Instance.Play(AudibleNames.Button.WRONG);
    }

    public Animator GetAnimator() {
        if (this.attackPanelAnimatable == null) {
            this.attackPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        return this.attackPanelAnimatable;
    }

    // Functions to be called by external scripts

    public void Show() {
        General.LogEntrance("RulePanelAnimatable Show");
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(SHOW);
        ResetTriggerVariables();
    }
    public void Hide() {
        General.LogEntrance("RulePanelAnimatable Hide");
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(HIDE);
        ResetTriggerVariables();
    }

	public void Shake() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(SHAKE);
        ResetTriggerVariables();
    }

    public void NewRule(Rules rule) {
        // TODO : Check whether to show or hide the rule bar based on
        // whether the rule has restrictions

        switch (rule) {
            case Rules.UNKNOWN:
                this.Hide();
                break;
            default: this.Show();
                break;
        }
    }

    // Always add
    public override void ResetTriggers() {
        attackPanelAnimatable.ResetTrigger(SHOW);
        attackPanelAnimatable.ResetTrigger(HIDE);
		attackPanelAnimatable.ResetTrigger(SHAKE);
	}

    void Update() {
        //SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
		triggerShake = false;
    }

    public override void SwitchTriggers() {
        if (triggerShow) {
            this.Show();
        }
        else if (triggerHide) {
            this.Hide();
        }
		else if (triggerShake) {
            this.Shake();
		}
    }
}
