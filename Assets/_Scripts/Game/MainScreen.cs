using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen : MonoBehaviour {
    public const string HAND_CARDS = "HAND_CARDS";
    public const string RULE_ICON = "RULE_ICON";
    public const string RULE_TEXT = "RULE_TEXT";

    protected MainScreenManager parent;
    

	public virtual void Hide() {
		gameObject.SetActive (false);
	}

	public virtual void Show() {
		gameObject.SetActive (true);
	}


    public virtual MainScreenManager GetParent() {
        if(this.parent == null) {
            this.parent = GetComponentInParent<MainScreenManager>();
        }
        General.LogNull(parent);
        return this.parent;
    }
}
