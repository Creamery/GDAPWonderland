using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesLeftAnimatable : AnimatableClass {
    public const string scriptName = "MovesLeftAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHAKE = "shake";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHAKE = 1.75f + f_offset;

    [SerializeField] private Animator endScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers
    [SerializeField] private bool triggerShake = false;


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
    public void Shake() {
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(SHAKE);
        ResetTriggerVariables();
    }

    public override void ResetTriggers() {
        if(this.GetAnimator().gameObject.activeSelf) {
            this.GetAnimator().ResetTrigger(SHAKE);
        }
    }
    public void PlayButtonEmpty() {
        SoundManager.Instance.Play(AudibleNames.Button.CANCEL);
    }
    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShake = false;
    }

    public override void SwitchTriggers() {
        if (triggerShake) {
            this.Shake();
        }
    }
}
