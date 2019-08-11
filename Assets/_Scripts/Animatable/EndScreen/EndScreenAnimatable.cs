using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenAnimatable : AnimatableClass {
    public const string scriptName = "LockScreenAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
    public const string RESET = "reset";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.3f + f_offset;
    public const float f_HIDE = 0.3f + f_offset;
    public const float f_RESET = 1.1f + f_offset;

    [SerializeField] private Animator endScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
    [SerializeField] private bool triggerReset = false;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.endScreenAnimator == null) {
            this.endScreenAnimator = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.endScreenAnimator == null) {
            this.endScreenAnimator = gameObject.GetComponent<Animator>();
        }
        return this.endScreenAnimator;
    }

    // Functions to be called by external scripts
    // Do a direct call
    public void Show() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(SHOW);
    }
    public void Hide() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(HIDE);
    }
    public void Reset() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(RESET);
    }

    // Always add
    public override void ResetTriggers() {
        endScreenAnimator.ResetTrigger(HIDE);
        endScreenAnimator.ResetTrigger(SHOW);
        endScreenAnimator.ResetTrigger(RESET);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
        triggerReset = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerShow) {
            this.Show();
        }
        else if (triggerHide) {
            this.Hide();
        }
        else if (triggerReset) {
            this.Reset();
        }
        ResetTriggerVariables();
    }
}
