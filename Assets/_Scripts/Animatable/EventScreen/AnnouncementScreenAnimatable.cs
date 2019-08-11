using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementScreenAnimatable : AnimatableClass {
    public const string scriptName = "AnnouncementScreenAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SHOW = "show";
    public const string HIDE = "hide";

    public const string RULE = "rule";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SHOW = 0.4f + f_offset;
    public const float f_HIDE = 1.0f + f_offset;

    [SerializeField] private Animator announcementScreenAnimatable;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerShow = false;
    [SerializeField] private bool triggerHide = false;


    [SerializeField] private bool triggerRule = false;
    //[SerializeField] private Rules triggerRuleType;


    void Start() {
        MAX_DURATION = 0.5f;
        if (this.announcementScreenAnimatable == null) {
            this.announcementScreenAnimatable = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.announcementScreenAnimatable == null) {
            this.announcementScreenAnimatable = gameObject.GetComponent<Animator>();
        }
        return this.announcementScreenAnimatable;
    }

    // Functions to be called by external scripts
  
    public void Show() {
        General.LogEntrance("AnnouncementScreen Show");
        this.triggerShow = true;
    }
    public void Hide() {
        General.LogEntrance("AnnouncementScreen Hide");
        this.triggerHide = true;
    }
    public void PlayTargetBell() {
        SoundManager.Instance.Play(AudibleNames.Target.BELL);
    }
    public void PlayButtonEmpty() {
        SoundManager.Instance.Play(AudibleNames.Button.EMPTY);
    }
    public void TriggerRule(Rules rule) {
        General.LogEntrance("AnnouncementScreen Rule");
        //this.triggerRuleType = rule;
        this.triggerRule = true;
    }
    // Always add
    public override void ResetTriggers() {
        announcementScreenAnimatable.ResetTrigger(SHOW);
        announcementScreenAnimatable.ResetTrigger(HIDE);

        announcementScreenAnimatable.ResetTrigger(RULE);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerShow = false;
        triggerHide = false;

        triggerRule = false;
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
        else if (triggerRule) {
            PlayRuleAnimation();
        }
        ResetTriggerVariables();
    }
    // TODO: Add here for ANNOUNCEMENT type events
    public void PlayRuleAnimation() {
        this.AnimationPlay();
        this.GetAnimator().SetTrigger(RULE);
        //switch (this.triggerRuleType) {
        //    case Rules.RED_CARDS_ONLY:
        //        this.GetAnimator().SetTrigger(RULE_DIAMONDS_HEARTS);
        //        break;
        //    case Rules.WHITE_CARDS_ONLY:
        //        this.GetAnimator().SetTrigger(RULE_DIAMONDS_HEARTS); // TODO: Change
        //        break;

        //    case Rules.BOMB:
        //        break;
        //    default: this.GetAnimator().SetTrigger(RULE_DIAMONDS_HEARTS); // TODO: Change
        //        break;
        //}
    }
}
