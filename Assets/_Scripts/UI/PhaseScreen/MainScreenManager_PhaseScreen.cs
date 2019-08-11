using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen manager for the Title scene.
/// </summary>
public class MainScreenManager_PhaseScreen : MonoBehaviour, MainScreenManager {
    public enum Phase {
        DRAW, ROULETTE, ATTACK
    }

    private static MainScreenManager_PhaseScreen sharedInstance;
    public static MainScreenManager_PhaseScreen Instance {
        get { return sharedInstance; }
    }

    void Awake() {
        sharedInstance = this;
    }
    void Start() {
        this.HideScreens();
    }
    // Main screen elements. Inherits MainScreen.cs
    [SerializeField] MainScreen_UIDrawPhase screen_DrawPhase;

    [SerializeField] Screen screenVariable;

    [SerializeField] Phase currentPhase;

    public PlayerManager GetPlayer() {
        return null;
    }

    //[Header("Testing")]
    // TODO Remove
    //[SerializeField] private bool showScreenAnnouncement;
    //[SerializeField] private bool showScreenItemGet;



    // REQ : Add. Functions called by external scripts or buttons.
    // Hide all screens then show the announcement screen.
    // TODO: Some screens should call Item Get, Bomb, ETC. Not just Show Announcement
    //public void ShowAnnouncement(Rules rule) {
    //    this.HideScreens();
    //    this.currentRule = rule;

    //    StartCoroutine(this.GetScreenAnnouncement().ShowRoutine(this.currentRule));
    //}
    public void Hide() {
        this.HideScreens();
        StartCoroutine(this.GetDrawPhase().HideRoutine());
    }

    public void ShowDrawPhase() {
        Debug.Log("<color=cyan> Show draw hase ()</color>");
        this.HideScreens();
        this.currentPhase = Phase.DRAW;
        StartCoroutine(this.GetDrawPhase().ShowRoutine());
    }

    public void ShowRoulettePhase() {
        this.HideScreens();
        this.currentPhase = Phase.ROULETTE;
        StartCoroutine(this.GetDrawPhase().RouletteRoutine());
    }

    public void ShowAttackPhase() {
        this.HideScreens();
        this.currentPhase = Phase.ATTACK;
        StartCoroutine(this.GetDrawPhase().AttackRoutine());
    }

    

    public Phase GetCurrentPhase() {
        this.currentPhase = Phase.ATTACK;
		//StartCoroutine(this.GetDrawPhase().AttackRoutine());
		return this.currentPhase;
    }
    // REQ : Add
    public void HideScreens() {
        this.GetDrawPhase().gameObject.SetActive(false);
    }

    // REQ : Add
    public void UncheckAll() {
        //this.showScreenAnnouncement = false;
        //this.showScreenItemGet = false;
    }

    public MainScreen_UIDrawPhase GetDrawPhase() {
        if (this.screen_DrawPhase == null) {
            this.screen_DrawPhase = GetComponentInChildren<MainScreen_UIDrawPhase>();
        }
        return this.screen_DrawPhase;
    }
}
