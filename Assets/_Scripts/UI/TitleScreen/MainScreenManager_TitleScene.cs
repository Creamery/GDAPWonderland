using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Screen manager for the Title scene.
/// </summary>
public class MainScreenManager_TitleScene : MonoBehaviour, MainScreenManager {
    public enum Screen {
        LOGO, MENU, QUIT_CONFIRM, TUTORIAL_CONFIRM
    }

    private static MainScreenManager_TitleScene sharedInstance;
    public static MainScreenManager_TitleScene Instance {
        get { return sharedInstance; }
    }

    // Main screen elements. Inherits MainScreen.cs
    [SerializeField] MainScreen_UILogo screen_Logo;
    [SerializeField] MainScreen_UIMenu screen_Menu;
    [SerializeField] Screen screenVariable;
    [SerializeField] MainScreen_UIQuitConfirm screen_QuitConfirm;
    [SerializeField] MainScreen_UITutorialConfirm screen_TutorialConfirm;

    public PlayerManager GetPlayer() {
        return null;
    }

    [Header("Testing")]
    // TODO Remove
    //[SerializeField] private bool showScreenLogo;
    //[SerializeField] private bool showScreenMenu;
    //[SerializeField] private bool showScreenQuitConfirm;
    [SerializeField] private SceneLoader sceneLoader;

	void Awake() {
		sharedInstance = this;
	}

    //// REQ : Add
    //void Update() {
    //    // TODO Remove, for inspector
    //    if (this.showScreenLogo) {
    //        this.UncheckAll();
    //        this.ShowLogo();
    //    }
    //    else if (this.showScreenMenu) {
    //        this.UncheckAll();
    //        this.ShowMenu(this.screenVariable);
    //    }
    //    else if (this.showScreenQuitConfirm) {
    //        this.UncheckAll();
    //        this.ShowQuitConfirm();
    //    }
    //}

    /// <summary>
    /// Load tutorial scene.
    /// </summary>
    public void LoadTutorial() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        this.GetSceneLoader().LoadScene(Scenes.GAME_SCENE);
    }

    /// <summary>
    /// Load character select screen.
    /// </summary>
    public void LoadCharacterSelect() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        this.GetSceneLoader().LoadScene(Scenes.CHARACTER_SELECT);
    }

    // REQ : Add. Functions called by external scripts or buttons.
    public void ShowLogo() {
        StartCoroutine(ShowLogoRoutine());
    }
   
    public void ShowMenu(Screen previousScreen) {
        StartCoroutine(ShowMenuRoutine(previousScreen));
    }

    public void ShowMenu_fromTutorialConfirm() {
        this.ShowMenu(Screen.TUTORIAL_CONFIRM);
    }


    public void ShowMenu_fromQuitConfirm() {
        this.ShowMenu(Screen.QUIT_CONFIRM);
    }

    public void ShowMenu_fromLogo() {
        this.ShowMenu(Screen.LOGO);
    }

    /// <summary>
    /// Hide Menu.
    /// </summary>
    public void HideMenu() {
        StartCoroutine(HideMenuRoutine());
    }

    public void ShowQuitConfirm() {
        StartCoroutine(ShowQuitConfirmRoutine());
    }

    public void ShowTutorialConfirm() {
        StartCoroutine(ShowTutorialConfirmRoutine());
    }

    /// <summary>
    /// Show the Logo screen from the Menu screen.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowLogoRoutine() {
        // Play the hide routine of the menu screen.
        yield return StartCoroutine(this.GetScreenMenu().HideRoutine());
        
        // Stall while animatable is playing.
        while (this.GetScreenMenu().IsPlaying()) {
            yield return null;
        }

        yield return StartCoroutine(this.GetScreenLogo().ShowRoutine());

    }
    /// <summary>
    /// Show the Menu screen where the previous screen is either the
    /// Logo screen or the QuitConfirm screen (as specified by the
    /// previousScreen parameter).
    /// </summary>
    /// <param name="previousScreen"></param>
    /// <returns></returns>
    public IEnumerator ShowMenuRoutine(Screen previousScreen) {
        // Play the hide routine of the previous screen.
        switch (previousScreen) {
            case Screen.LOGO:
                yield return StartCoroutine(this.GetScreenLogo().HideRoutine());
                // Stall while animatable is playing.
                while (this.GetScreenLogo().IsPlaying()) {
                    yield return null;
                }
                break;
            case Screen.QUIT_CONFIRM:
                yield return StartCoroutine(this.GetScreenQuitConfirm().HideRoutine());
                break;
            case Screen.TUTORIAL_CONFIRM:
                yield return StartCoroutine(this.GetScreenTutorialConfirm().HideRoutine());
                break;
        }
        yield return StartCoroutine(this.GetScreenMenu().ShowRoutine());
    }


    /// <summary>
    /// Hide the Menu screen and show the next screen which is either the Logo screen
    /// or the QuitConfirm screen (as specified by the nextScreen parameter).
    /// previousScreen parameter).
    /// </summary>
    /// <param name="nextScreen"></param>
    /// <returns></returns>
    public IEnumerator HideMenuRoutine() {

        yield return StartCoroutine(this.GetScreenMenu().HideRoutine());

        // Stall while animatable is playing.
        while (this.GetScreenMenu().IsPlaying()) {
            yield return null;
        }
    }

    /// <summary>
    /// Show the QuitConfirm screen from the Menu screen.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowQuitConfirmRoutine() {

        yield return StartCoroutine(this.GetScreenMenu().HideRoutine());
        // Stall while animatable is playing.
        while (this.GetScreenMenu().IsPlaying()) {
            yield return null;
        }
        yield return StartCoroutine(this.GetScreenQuitConfirm().ShowRoutine());
    }

    /// <summary>
    /// Show the TutorialConfirm screen from the Menu screen.
    /// </summary>
    /// <returns></returns>
    public IEnumerator ShowTutorialConfirmRoutine() {

        yield return StartCoroutine(this.GetScreenMenu().HideRoutine());
        // Stall while animatable is playing.
        while (this.GetScreenMenu().IsPlaying()) {
            yield return null;
        }
        yield return StartCoroutine(this.GetScreenTutorialConfirm().ShowRoutine());
    }
    
    /// <summary>
    /// Hide the QuitConfirm screen and show the Menu screen.
    /// </summary>
    /// <returns></returns>
    public IEnumerator HideQuitConfirmRoutine() {

        // Stall while animatable is playing.
        while (this.GetScreenQuitConfirm().IsPlaying()) {
            yield return null;
        }
        yield return StartCoroutine(this.GetScreenMenu().ShowRoutine());
    }

    // REQ : Add
    public void HideScreens() {
    }

    // REQ : Add
    public void UncheckAll() {
        //this.showScreenLogo = false;
        //this.showScreenMenu = false;
        //this.showScreenQuitConfirm = false;
    }

    public MainScreen_UILogo GetScreenLogo() {
        if (this.screen_Logo == null) {
            this.screen_Logo = GetComponentInChildren<MainScreen_UILogo>();
        }
        return this.screen_Logo;
    }

    public MainScreen_UIMenu GetScreenMenu() {
        if (this.screen_Menu == null) {
            this.screen_Menu = GetComponentInChildren<MainScreen_UIMenu>();
        }
        return this.screen_Menu;
    }
    public MainScreen_UIQuitConfirm GetScreenQuitConfirm() {
        if (this.screen_QuitConfirm == null) {
            this.screen_QuitConfirm = GetComponentInChildren<MainScreen_UIQuitConfirm>();
        }
        return this.screen_QuitConfirm;
    }
    public MainScreen_UITutorialConfirm GetScreenTutorialConfirm() {
        if (this.screen_TutorialConfirm == null) {
            this.screen_TutorialConfirm = GetComponentInChildren<MainScreen_UITutorialConfirm>();
        }
        return this.screen_TutorialConfirm;
    }

    public SceneLoader GetSceneLoader() {
        if(this.sceneLoader == null) {
            this.sceneLoader = GetComponent<SceneLoader>();
        }
        return this.sceneLoader;
    }
}
