using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleConfirmationAnimatable : AnimatableClass {
    public const string scriptName = "CircleConfirmationAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";

    public const float f_offset = 0.05f;

    public const float f_SHOW = 0.25f + f_offset;
    public const float f_HIDE = 0.25f + f_offset;

    [SerializeField] private Animator contentScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.contentScreenAnimator == null) {
            this.contentScreenAnimator = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.contentScreenAnimator == null) {

            this.contentScreenAnimator = gameObject.GetComponent<Animator>();
        }
        return this.contentScreenAnimator;
    }

    // Functions to be called by external scripts
  
    public void Show() {
        this.triggerShow = true;
    }
    public void Hide() {
        this.triggerHide = true;
    }


    // Always add
    public override void ResetTriggers() {
        contentScreenAnimator.ResetTrigger(SHOW);
        contentScreenAnimator.ResetTrigger(HIDE);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerShow) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SHOW);
        }
        else if (triggerHide) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(HIDE);
        }
        ResetTriggerVariables();
    }


}
