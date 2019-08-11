using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour {

    [SerializeField] private StartPanelParentObjectMarker startPanelParent;
    [SerializeField] private StartPanelAnimatable startPanelAnimatable;


    [SerializeField] private HeroFrame heroPlayer1;
    [SerializeField] private HeroFrame heroPlayer2;

    private bool isRunning;

    private static StartPanel sharedInstance;
    public static StartPanel Instance {
        get { return sharedInstance; }
    }
    void Awake() {
        sharedInstance = this;
    }

    // Called by external scripts
    public void Show() {
        StartCoroutine(ShowRoutine());
	}

    public void RefreshHeroes() {
        if(this.heroPlayer1 != null) {
            this.heroPlayer1.SetHero(GameMaster.Instance.GetPlayer1().GetPlayerHero().GetHero());
        }
        if (this.heroPlayer2 != null) {
            this.heroPlayer2.SetHero(GameMaster.Instance.GetPlayer2().GetPlayerHero().GetHero());
        }
    }
    public void PlayThud() {
        SoundManager.Instance.Play(AudibleNames.Button.THUD);
    }
    public void PlayFireStart() {
        SoundManager.Instance.Play(AudibleNames.Fire.START);
    }
    public void PlayButtonDefault() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
    }
    public void PlayTargetBell() {
        SoundManager.Instance.Play(AudibleNames.Target.BELL);
    }
    public void PlayButtonOpen() {
        SoundManager.Instance.Play(AudibleNames.Button.OPEN);
    }
    public IEnumerator ShowRoutine() {
        if (!isRunning) {
            this.RefreshHeroes();
            this.isRunning = true;
            this.GetStartPanelParent().Show();
            this.GetStartPanelAnimatable().Show();
            yield return new WaitForSeconds(StartPanelAnimatable.f_SHOW);

            this.GetStartPanelAnimatable().Hide();
            yield return new WaitForSeconds(StartPanelAnimatable.f_HIDE);
            this.isRunning = false;
            this.Hide();
        }
        yield return null;
    }

    public void Hide() {
        this.GetStartPanelParent().Hide();
    }

    public StartPanelParentObjectMarker GetStartPanelParent() {
        if(this.startPanelParent == null) {
            this.startPanelParent = GetComponentInChildren<StartPanelParentObjectMarker>();
        }
        return this.startPanelParent;
    }

    public StartPanelAnimatable GetStartPanelAnimatable() {
        if (this.startPanelAnimatable == null) {
            this.startPanelAnimatable = GetComponentInChildren<StartPanelAnimatable>();
        }
        return this.startPanelAnimatable;
    }
}
