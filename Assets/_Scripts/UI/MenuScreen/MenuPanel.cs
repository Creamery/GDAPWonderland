using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour {

    [SerializeField] private MenuParentObjectMarker menuParent;

    private static MenuPanel sharedInstance;
    public static MenuPanel Instance {
        get { return sharedInstance; }
    }
    void Awake() {
        sharedInstance = this;
    }

    // Called by external scripts
    public void Show() {
        this.GetMenuParent().Show();
	}

    public void Hide() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        this.GetMenuParent().Hide();
    }

    public MenuParentObjectMarker GetMenuParent() {
        if(this.menuParent == null) {
            this.menuParent = GetComponentInChildren<MenuParentObjectMarker>();
        }
        return this.menuParent;
    }
}
