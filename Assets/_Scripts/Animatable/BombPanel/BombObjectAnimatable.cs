using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombObjectAnimatable : AnimatableClass {
    public const string scriptName = "BombObjectAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
    public const string THROW = "throw";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.3f + f_offset;
    public const float f_HIDE = 0.3f + f_offset;
    public const float f_THROW = 0.3f + f_offset;

    [SerializeField] private Animator bombPanelAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
    [SerializeField] private bool triggerThrow = false;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.bombPanelAnimatable == null) {
            this.bombPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.bombPanelAnimatable == null) {
            this.bombPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        return this.bombPanelAnimatable;
    }

    // Functions to be called by external scripts
  
    public void Show() {
        this.triggerShow = true;
    }
    public void Hide() {
        this.triggerHide = true;
    }
    public void Throw() {
        this.triggerThrow = true;
    }
    // Always add
    public override void ResetTriggers() {
        bombPanelAnimatable.ResetTrigger(HIDE);
        bombPanelAnimatable.ResetTrigger(SHOW);
        bombPanelAnimatable.ResetTrigger(THROW);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
        triggerThrow = false;
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
        else if (triggerThrow) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(THROW);
        }
        ResetTriggerVariables();
    }
}
