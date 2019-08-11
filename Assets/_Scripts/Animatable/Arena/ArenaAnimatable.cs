using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaAnimatable : AnimatableClass {
    public const string scriptName = "ArenaAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.

    // For sliding in/out soldiers when the roulette shows/hides
    public const string OPEN_ROULETTE = "open roulette";
    public const string CLOSE_ROULETTE = "close roulette";

    // Backup defense sliding
    public const string SLIDE_OUT_WHITE = "slide out white";
    public const string SLIDE_IN_WHITE = "slide in white";
    public const string SLIDE_OUT_RED = "slide out red";
    public const string SLIDE_IN_RED = "slide in red";


    // Animation offset time to give padding in the coroutine. Used in the f_VARIABLES below.
    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    // Used for making coroutines wait for animations (WaitForSeconds)
    // TODO: Filler values, change to proper values when animations are created.
    public const float f_OPEN_ROULETTE = 0.3f + f_offset; 
    public const float f_CLOSE_ROULETTE = 0.3f + f_offset;

    public const float f_SLIDE_OUT_WHITE = 0.3f + f_offset;
    public const float f_SLIDE_IN_WHITE = 0.3f + f_offset;
    public const float f_SLIDE_OUT_RED = 0.3f + f_offset;
    public const float f_SLIDE_IN_RED = 0.3f + f_offset;


    [SerializeField] private Animator lockScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerOpenRoulette = false;
    [SerializeField] private bool triggerCloseRoulette = false;

    [SerializeField] private bool triggerSlideOutWhite = false;
    [SerializeField] private bool triggerSlideInWhite = false;
    [SerializeField] private bool triggerSlideOutRed = false;
    [SerializeField] private bool triggerSlideInRed = false;


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
  
    public void OpenRoulette() {
        this.triggerOpenRoulette = true;
    }
    public void CloseRoulette() {
        this.triggerCloseRoulette = true;
    }

    public void SlideInWhite() {
        this.triggerSlideInWhite = true;
    }
    public void SlideOutWhite() {
        this.triggerSlideOutWhite = true;
    }
    public void SlideInRed() {
        this.triggerSlideInRed = true;
    }
    public void SlideOutRed() {
        this.triggerSlideOutRed = true;
    }

    // Always add
    public override void ResetTriggers() {
        lockScreenAnimator.ResetTrigger(OPEN_ROULETTE);
        lockScreenAnimator.ResetTrigger(CLOSE_ROULETTE);

        lockScreenAnimator.ResetTrigger(SLIDE_IN_WHITE);
        lockScreenAnimator.ResetTrigger(SLIDE_OUT_WHITE);
        lockScreenAnimator.ResetTrigger(SLIDE_IN_RED);
        lockScreenAnimator.ResetTrigger(SLIDE_OUT_WHITE);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerOpenRoulette = false;
        triggerCloseRoulette = false;

        triggerSlideInWhite = false;
		triggerSlideOutWhite = false;
        triggerSlideInRed = false;
        triggerSlideOutRed = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerOpenRoulette) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(OPEN_ROULETTE);
        }
        else if (triggerCloseRoulette) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(CLOSE_ROULETTE);
        }
        else if (triggerSlideInWhite) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SLIDE_IN_WHITE);
        }
        else if (triggerSlideOutWhite) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SLIDE_OUT_WHITE);
        }
        else if (triggerSlideInRed) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SLIDE_IN_RED);
        }
        else if (triggerSlideOutRed) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SLIDE_OUT_RED);
        }
        ResetTriggerVariables();
    }
}
