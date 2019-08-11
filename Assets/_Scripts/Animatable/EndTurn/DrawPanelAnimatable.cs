using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPanelAnimatable : AnimatableClass {
    public const string scriptName = "DrawPanelAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string BURN = "burn";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 1.75f + f_offset;
    public const float f_BURN = 1.0f + f_offset;

    [SerializeField] private Animator endScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers
    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerBurn = false;


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
        ResetTriggerVariables();
    }
    public void Burn() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(BURN);
        ResetTriggerVariables();
    }
    
    // Always add
    public override void ResetTriggers() {
        endScreenAnimator.ResetTrigger(BURN);
        endScreenAnimator.ResetTrigger(SHOW);
    }

    //void Update() {
    //    SwitchTriggers();
    //}

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerBurn = false;
    }

    public override void SwitchTriggers() {
        if (triggerShow) {
            this.Show();
        }
        else if (triggerBurn) {
            this.Burn();
        }
    }
}
