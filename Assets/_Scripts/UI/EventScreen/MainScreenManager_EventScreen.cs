using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen manager for the Title scene.
/// </summary>
public class MainScreenManager_EventScreen : MonoBehaviour, MainScreenManager {
    public enum Screen {
        ANNOUNCEMENT, ITEM_GET, HERO_GET
    }

    private static MainScreenManager_EventScreen sharedInstance;
    public static MainScreenManager_EventScreen Instance {
        get { return sharedInstance; }
    }

    void Awake() {
        sharedInstance = this;
    }
    void Start() {
        this.HideScreens();
    }
    // Main screen elements. Inherits MainScreen.cs
    [SerializeField] MainScreen_UIAnnouncement screen_Announcement;
    [SerializeField] MainScreen_UIItemGet screen_ItemGet;

    [SerializeField] Screen screenVariable;


    [SerializeField] Rules currentRule;

    public PlayerManager GetPlayer() {
        return null;
    }

    [Header("Testing")]
    // TODO Remove
    [SerializeField] private bool showScreenAnnouncement;
    [SerializeField] private bool showScreenItemGet;


    //// REQ : Add
    void Update() {
        // TODO Remove, for inspector
        if (this.showScreenAnnouncement) {
            this.UncheckAll();
            this.ShowAnnouncement(this.currentRule);
        }
        else if (this.showScreenItemGet) {
            this.UncheckAll();
            this.ShowItemGet(this.currentRule);
        }
    }


    // REQ : Add. Functions called by external scripts or buttons.
    // Hide all screens then show the announcement screen.
    // TODO: Some screens should call Item Get, Bomb, ETC. Not just Show Announcement
    public void ShowAnnouncement(Rules rule) {
        this.HideScreens();
        this.currentRule = rule;
       
        StartCoroutine(this.GetScreenAnnouncement().ShowRoutine(this.currentRule));
    }

    public void ShowItemGet(Rules rule) {
        this.HideScreens();
        this.currentRule = rule;
        StartCoroutine(this.GetScreenItemGet().ShowRoutine(this.currentRule));
    }




    // REQ : Add
    public void HideScreens() {
        this.GetScreenAnnouncement().gameObject.SetActive(false);
        this.GetScreenItemGet().gameObject.SetActive(false);
    }

    // REQ : Add
    public void UncheckAll() {
        this.showScreenAnnouncement = false;
        this.showScreenItemGet = false;
    }


    public MainScreen_UIAnnouncement GetScreenAnnouncement() {
        if (this.screen_Announcement == null) {
            this.screen_Announcement = GetComponentInChildren<MainScreen_UIAnnouncement>();
        }
        return this.screen_Announcement;
    }

    public MainScreen_UIItemGet GetScreenItemGet() {
        if (this.screen_ItemGet == null) {
            this.screen_ItemGet = GetComponentInChildren<MainScreen_UIItemGet>();
        }
        return this.screen_ItemGet;
    }
}
