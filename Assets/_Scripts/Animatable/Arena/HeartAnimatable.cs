using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartAnimatable : AnimatableClass {
    public const string scriptName = "HeartAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.

    // For sliding in/out soldiers when the roulette shows/hides
    public const string SHOW = "show";
    public const string HIDE = "hide";

    // Backup defense sliding
    public const string BREAK = "break";


    // Animation offset time to give padding in the coroutine. Used in the f_VARIABLES below.
    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    // Used for making coroutines wait for animations (WaitForSeconds)
    // TODO: Filler values, change to proper values when animations are created.
    public const float f_SHOW = 0.3f + f_offset; 
    public const float f_HIDE = 0.3f + f_offset;
    public const float f_BREAK = 0.3f + f_offset;

    [SerializeField] private Animator lockScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;

    [SerializeField] private bool triggerBreak = false;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.lockScreenAnimator == null) {
            this.lockScreenAnimator = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    /// <summary>
    /// Animator getter.
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimator() {
        if(this.lockScreenAnimator == null) {
            this.lockScreenAnimator = gameObject.GetComponent<Animator>();
        }
        return this.lockScreenAnimator;
    }

    // Functions to be called by external scripts
  
    public void Show() {
        this.triggerShow = true;
    }
    public void Hide() {
        this.triggerHide = true;
    }

    public void Break() {
        this.triggerBreak = true;
    }

    // Always add
    public override void ResetTriggers() {
        lockScreenAnimator.ResetTrigger(SHOW);
        lockScreenAnimator.ResetTrigger(HIDE);

        lockScreenAnimator.ResetTrigger(BREAK);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;

        triggerBreak = false;
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
        else if (triggerBreak) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(BREAK);
        }
        ResetTriggerVariables();
    }
}
