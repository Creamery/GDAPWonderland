﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetScreenAnimatable : AnimatableClass {
    public const string scriptName = "ItemGetScreenAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
    public const string DRAW_CARD = "draw card";
    public const string BURN_CARD = "burn card";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.35f + f_offset;
    public const float f_HIDE = 1.0f + f_offset;
    public const float f_DRAW_CARD = 3.0f + f_offset;


    public const float f_BURN_CARD = 1.2f + f_offset;

    [SerializeField] private Animator itemGetScreenAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
    [SerializeField] private bool triggerDrawCard = false;
    [SerializeField] private bool triggerCardBurn = false;

    void Start() {
        MAX_DURATION = 0.5f;
        if (this.itemGetScreenAnimatable == null) {
            this.itemGetScreenAnimatable = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.itemGetScreenAnimatable == null) {
            this.itemGetScreenAnimatable = gameObject.GetComponent<Animator>();
        }
        return this.itemGetScreenAnimatable;
    }

    // Functions to be called by external scripts
    public void PlayTargetBell() {
        SoundManager.Instance.Play(AudibleNames.Target.BELL);
    }
    public void PlayTargetGet() {
        SoundManager.Instance.Play(AudibleNames.Target.GET);
    }
    public void PlayButtonRelease() {
        SoundManager.Instance.Play(AudibleNames.Button.RELEASE);
    }
    public void PlayFireBurn() {
        SoundManager.Instance.Play(AudibleNames.Fire.BURN);
    }
    public void Show() {
        General.LogEntrance("ItemGetScreen Show");
        this.triggerShow = true;
    }
    public void Hide() {
        General.LogEntrance("ItemGetScreen Hide");
        this.triggerHide = true;
    }
    public void DrawCard() {
        General.LogEntrance("ItemGetScreen DrawCard");
        this.triggerDrawCard = true;
    }
    public void BurnCard() {
        General.LogEntrance("ItemGetScreen DrawCard");
        this.triggerCardBurn = true;
    }
    // Always add
    public override void ResetTriggers() {
        itemGetScreenAnimatable.ResetTrigger(SHOW);
        itemGetScreenAnimatable.ResetTrigger(HIDE);
        itemGetScreenAnimatable.ResetTrigger(DRAW_CARD);
        itemGetScreenAnimatable.ResetTrigger(BURN_CARD);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
        triggerDrawCard = false;
        triggerCardBurn = false;
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
        else if (triggerDrawCard) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(DRAW_CARD);
        }
        else if (triggerCardBurn) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(BURN_CARD);
        }
        ResetTriggerVariables();
    }
}
