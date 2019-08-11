using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPanelAnimatable : AnimatableClass {
    public const string scriptName = "BombPanelAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
    public const string THROWN = "thrown";
    public const string COUNT = "count";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.3f + f_offset;
    public const float f_HIDE = 0.3f + f_offset;
    public const float f_THROWN = 1.0f + f_offset;
    public const float f_COUNT = 1.15f + f_offset;

    [SerializeField] private Animator bombPanelAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
    [SerializeField] private bool triggerThrown = false;
    [SerializeField] private bool triggerCount = false;


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
        ResetTriggers();
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(SHOW);
        ResetTriggerVariables();
    }
    public void Hide() {
        ResetTriggers();
        General.LogEntrance("BombPanelAnimatable HIDE");
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(HIDE);
        ResetTriggerVariables();
    }
    public void Thrown() {
        ResetTriggers();
        General.LogEntrance("BombPanelAnimatable THROWN");
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(THROWN);
        ResetTriggerVariables();
    }

    public void Count() {
        ResetTriggers();
        General.LogEntrance("BombPanelAnimatable COUNT");
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(COUNT);
        ResetTriggerVariables();
    }
    // Always add
    public override void ResetTriggers() {
        bombPanelAnimatable.ResetTrigger(HIDE);
        bombPanelAnimatable.ResetTrigger(SHOW);
        bombPanelAnimatable.ResetTrigger(THROWN);
        bombPanelAnimatable.ResetTrigger(COUNT);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
        triggerThrown = false;
        triggerCount = false;
    }

    public override void SwitchTriggers() {
        if (triggerShow) {
            this.Show();
        }
        else if (triggerHide) {
            this.Hide();
        }
        else if (triggerThrown) {
            this.Thrown();
        }
        else if (triggerCount) {
            this.Count();
        }
    }
}
