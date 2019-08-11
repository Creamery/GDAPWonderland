using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentScreenAnimatable : AnimatableClass {
	public const string scriptName = "ContentScreenAnimatable";

	// List of Params. String must have a corresponding Trigger parameter in animator.
	public const string LOGO_SPIN = "logo_spin";
    public const string LOGO_SHOW = "logo_show";
    public const string LOGO_HIDE = "logo_hide";
    public const string LOGO_INIT = "logo_init";

    public const string MENU_SHOW = "menu_show";
    public const string MENU_HIDE = "menu_hide";

    public const string QUIT_CONFIRM_SHOW = "quit_confirm_show";
    public const string QUIT_CONFIRM_HIDE = "quit_confirm_hide";

    public const string TUTORIAL_CONFIRM_SHOW = "tutorial_confirm_show";
    public const string TUTORIAL_CONFIRM_HIDE = "tutorial_confirm_hide";
    

    [SerializeField] private Animator contentScreenAnimator;

	// Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

	[SerializeField] private bool triggerLogoSpin = false;
    [SerializeField] private bool triggerLogoShow = false;
    [SerializeField] private bool triggerLogoHide = false;
    [SerializeField] private bool triggerLogoInit = false;

    [SerializeField] private bool triggerMenuShow = false;
    [SerializeField] private bool triggerMenuHide = false;

    [SerializeField] private bool triggerQuitConfirmShow = false;
    [SerializeField] private bool triggerQuitConfirmHide = false;

    [SerializeField] private bool triggerTutorialConfirmShow = false;
    [SerializeField] private bool triggerTutorialConfirmHide = false;

    void Start () {
		MAX_DURATION = 0.5f;
        if(this.contentScreenAnimator == null) {
            this.contentScreenAnimator = gameObject.GetComponent<Animator>();
        }
		this.ResetTriggerVariables ();
		this.isPlaying = false;

	}

    // Functions to be called by external scripts
    public void LogoShow() {
        this.triggerLogoShow = true;
    }

    public void LogoSpin() {
		this.triggerLogoSpin = true;
	}

    public void LogoHide() {
        this.triggerLogoHide = true;
    }

    public void LogoInit() {
        this.triggerLogoInit = true;
    }

    public void MenuShow() {
        this.triggerMenuShow = true;
    }
    public void MenuHide() {
        this.triggerMenuHide = true;
    }

    public void QuitConfirmShow() {
        this.triggerQuitConfirmShow = true;
    }
    public void QuitConfirmHide() {
        this.triggerQuitConfirmHide = true;
    }

    public void TutorialConfirmShow() {
        this.triggerTutorialConfirmShow = true;
    }
    public void TutorialConfirmHide() {
        this.triggerTutorialConfirmHide = true;
    }
    

    // Always add
    public override void ResetTriggers() {
        contentScreenAnimator.ResetTrigger (LOGO_SPIN);
        contentScreenAnimator.ResetTrigger(LOGO_SHOW);
        contentScreenAnimator.ResetTrigger(LOGO_HIDE);
        contentScreenAnimator.ResetTrigger(LOGO_INIT);
        
        contentScreenAnimator.ResetTrigger(MENU_SHOW);
        contentScreenAnimator.ResetTrigger(MENU_HIDE);

        contentScreenAnimator.ResetTrigger(QUIT_CONFIRM_SHOW);
        contentScreenAnimator.ResetTrigger(QUIT_CONFIRM_HIDE);

        contentScreenAnimator.ResetTrigger(TUTORIAL_CONFIRM_SHOW);
        contentScreenAnimator.ResetTrigger(TUTORIAL_CONFIRM_HIDE);
    }

	void Update () {
		SwitchTriggers ();
	}

	// ADD TRIGGER BOOL
	public override void ResetTriggerVariables() {
        triggerLogoSpin = false;
        triggerLogoShow = false;
        triggerLogoHide = false;
        triggerLogoInit = false;

        triggerMenuShow = false;
        triggerMenuHide = false;

        triggerQuitConfirmShow = false;
        triggerQuitConfirmHide = false;

        triggerTutorialConfirmShow = false;
        triggerTutorialConfirmHide = false;
    }

	public override void SwitchTriggers() {
		ResetTriggers ();

        // LOGO
		if (triggerLogoSpin) {
			this.AnimationPlay ();
            contentScreenAnimator.SetTrigger (LOGO_SPIN);
		}
        else if (triggerLogoShow) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(LOGO_SHOW);
        }
        else if (triggerLogoHide) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(LOGO_HIDE);
        }
        else if (triggerLogoInit) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(LOGO_INIT);
        }

        // MENU
        else if (triggerMenuShow) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(MENU_SHOW);
        }
        else if (triggerMenuHide) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(MENU_HIDE);
        }

        // QUIT CONFIRM
        else if (triggerQuitConfirmShow) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(QUIT_CONFIRM_SHOW);
        }
        else if (triggerQuitConfirmHide) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(QUIT_CONFIRM_HIDE);
        }

        // TUTORIAL CONFIRM
        else if (triggerTutorialConfirmShow) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(TUTORIAL_CONFIRM_SHOW);
        }
        else if (triggerTutorialConfirmHide) {
            this.AnimationPlay();
            contentScreenAnimator.SetTrigger(TUTORIAL_CONFIRM_HIDE);
        }
        ResetTriggerVariables ();
	}

}
