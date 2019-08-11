using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPhaseAnimatable : AnimatableClass {
    public const string scriptName = "DrawPhaseAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";
    public const string DRAW_CARD = "draw card";
    public const string BURN_CARD = "burn card";


    public const string ROULETTE = "roulette";
    public const string ATTACK = "attack";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 1.5f + f_offset;
    public const float f_HIDE = 1.0f + f_offset;
    public const float f_DRAW_CARD = 3.0f + f_offset;


    public const float f_BURN_CARD = 1.2f + f_offset;


    public const float f_ROULETTE = 1.5f + f_offset;
    public const float f_ATTACK = 1.5f + f_offset;


    public const float f_ATTACK_SHOW = 1.1f;

    [SerializeField] private Animator itemGetScreenAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;
    [SerializeField] private bool triggerDrawCard = false;
    [SerializeField] private bool triggerCardBurn = false;


    [SerializeField] private bool triggerRoulette = false;
    [SerializeField] private bool triggerAttack = false;

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
    public void PlayFireBurn() {
        SoundManager.Instance.Play(AudibleNames.Fire.BURN);
    }
    // Functions to be called by external scripts
    public void PlayButtonDrag() {
        SoundManager.Instance.Play(AudibleNames.Button.DRAG);
    }
    public void PlayButtonRelease() {
        SoundManager.Instance.Play(AudibleNames.Button.RELEASE);
    }
    public void Show() {
        Debug.Log("<color=cyan>post get ()</color>");
        General.LogEntrance("DrawPhase Show");
        this.triggerShow = true;

        
        this.SwitchTriggers();
    }
    public void Hide() {
        General.LogEntrance("DrawPhase Hide");
        this.triggerHide = true;
        this.SwitchTriggers();
    }
    public void DrawCard() {
        General.LogEntrance("DrawPhase DrawCard");
        this.triggerDrawCard = true;
        this.SwitchTriggers();
    }
    public void BurnCard() {
        General.LogEntrance("DrawPhase BurnCard");
        this.triggerCardBurn = true;
        this.SwitchTriggers();
    }

    public void Roulette() {
        General.LogEntrance("DrawPhase Roulette");
        this.triggerRoulette = true;
        this.SwitchTriggers();
    }


    public void Attack() {
        General.LogEntrance("DrawPhase Attack");
        this.triggerAttack = true;
        this.SwitchTriggers();
    }
    // Always add
    public override void ResetTriggers() {
        itemGetScreenAnimatable.ResetTrigger(SHOW);
        itemGetScreenAnimatable.ResetTrigger(HIDE);
        itemGetScreenAnimatable.ResetTrigger(DRAW_CARD);
        itemGetScreenAnimatable.ResetTrigger(BURN_CARD);


        itemGetScreenAnimatable.ResetTrigger(ROULETTE);
        itemGetScreenAnimatable.ResetTrigger(ATTACK);
    }

    //void Update() {
    //    SwitchTriggers();
    //}

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;
        triggerDrawCard = false;
        triggerCardBurn = false;



        triggerRoulette = false;
        triggerAttack = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerShow) {
            Debug.Log("<color=cyan>TRIGGER SHOW get ()</color>");
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

        else if (triggerRoulette) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(ROULETTE);
        }

        else if (triggerAttack) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(ATTACK);
        }
        ResetTriggerVariables();
    }
}
