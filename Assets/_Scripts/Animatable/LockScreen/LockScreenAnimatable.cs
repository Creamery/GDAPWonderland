using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockScreenAnimatable : AnimatableClass {
    public const string scriptName = "LockScreenAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string MINIMIZE = "minimize";
    public const string MAXIMIZE = "maximize";
    public const string UNLOCK = "unlock";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_MINIMIZE = 0.3f + f_offset;
    public const float f_MAXIMIZE = 0.3f + f_offset;
    public const float f_UNLOCK = 1.1f + f_offset;

    [SerializeField] private Animator lockScreenAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerMaximize = false;
    [SerializeField] private bool triggerMinimize = false;
    [SerializeField] private bool triggerUnlock = false;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.lockScreenAnimator == null) {
            this.lockScreenAnimator = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.lockScreenAnimator == null) {
            this.lockScreenAnimator = gameObject.GetComponent<Animator>();
        }
        return this.lockScreenAnimator;
    }
    public void PlayButtonOpen() {
        SoundManager.Instance.Play(AudibleNames.Button.OPEN);
    }
    public void PlayCrosshairLoad() {
        SoundManager.Instance.Play(AudibleNames.Crosshair.LOAD);
    }
    // Functions to be called by external scripts

    public void Maximize() {
        General.LogEntrance("Maximize");
        this.triggerMaximize = true;
    }
    public void Minimize() {
        this.triggerMinimize = true;
    }
    public void Unlock() {
        this.triggerUnlock = true;
    }

    // Always add
    public override void ResetTriggers() {
        lockScreenAnimator.ResetTrigger(MAXIMIZE);
        lockScreenAnimator.ResetTrigger(MINIMIZE);
        lockScreenAnimator.ResetTrigger(UNLOCK);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerMaximize = false;
        triggerMinimize = false;
        triggerUnlock = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerMaximize) {
            this.AnimationPlay();
            this.PlayCrosshairLoad();
            this.GetAnimator().SetTrigger(MAXIMIZE);
        }
        else if (triggerMinimize) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(MINIMIZE);
        }
        else if (triggerUnlock) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(UNLOCK);
        }
        ResetTriggerVariables();
    }
}
