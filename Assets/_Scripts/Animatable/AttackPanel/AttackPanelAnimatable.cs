using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPanelAnimatable : AnimatableClass {
    public const string scriptName = "AttackPanelAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string HIT = "hit";
    public const string MISS = "miss";
    public const string AGAIN = "again";

    public const string SHOOT = "shoot";
    public const string SHOW = "Show";


    public const string SCAN_HIT = "scan hit";
    public const string SCAN_MISS = "scan miss";
    public const string SCAN_NONE = "scan none";


    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_HIT = 1.2f + f_offset;
    public const float f_MISS = 1.2f + f_offset;
    public const float f_AGAIN = 1.2f + f_offset;

    public const float f_SHOOT = 1.1f + f_offset;

    public const float f_SCAN_HIT = 1.2f + f_offset;
    public const float f_SCAN_MISS = 1.2f + f_offset;
    public const float f_SCAN_AGAIN = 1.2f + f_offset;


    [SerializeField] private Animator attackPanelAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerHit = false;
    [SerializeField] private bool triggerMiss = false;
    [SerializeField] private bool triggerAgain = false;
    [SerializeField] private bool triggerShoot = false;

    //[SerializeField] private bool triggerScanHit = false;
    //[SerializeField] private bool triggerScanMiss = false;
    //[SerializeField] private bool triggerScanAgain = false;

    void Start() {
        MAX_DURATION = 0.5f;
        if (this.attackPanelAnimatable == null) {
            this.attackPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if (this.attackPanelAnimatable == null) {
            this.attackPanelAnimatable = gameObject.GetComponent<Animator>();
        }
        return this.attackPanelAnimatable;
    }

    // Functions to be called by external scripts
    public void Show() {
        this.GetAnimator().SetBool(SHOW, true);
    }
    public void Hide() {
        this.GetAnimator().SetBool(SHOW, false);
    }
    public void Hit() {
        this.triggerHit = true;
    }
    public void Miss() {
        this.triggerMiss = true;
    }
    public void Again() {
        this.triggerAgain = true;
    }
    public void Shoot() {
        this.triggerShoot = true;
    }
    // Always add
    public override void ResetTriggers() {
        attackPanelAnimatable.ResetTrigger(HIT);
        attackPanelAnimatable.ResetTrigger(MISS);
        attackPanelAnimatable.ResetTrigger(AGAIN);
        attackPanelAnimatable.ResetTrigger(SHOOT);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerHit = false;
        triggerMiss = false;
        triggerAgain = false;

        triggerShoot = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerHit) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(HIT);
        }
        else if (triggerMiss) {
            SoundManager.Instance.Play(AudibleNames.Crosshair.MISS);
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(MISS);
        }
        else if (triggerAgain) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(AGAIN);
        }
        else if (triggerShoot) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SHOOT);
        }
        ResetTriggerVariables();
    }
}
