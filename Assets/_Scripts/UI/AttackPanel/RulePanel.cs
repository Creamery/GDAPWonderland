using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RulePanel : MonoBehaviour {

    [SerializeField] RulePanelAnimatable rulePanelAnimatable;

    private static RulePanel sharedInstance;
    public static RulePanel Instance {
        get { return sharedInstance; }
    }
    private void Awake() {
        sharedInstance = this;
    }

    // Called by external scripts
    //public void Show() {
    //    this.GetRulePanelAnimatable().Show();
    //}

    public void Hide() {
        this.GetRulePanelAnimatable().Hide();
    }

    public RulePanelAnimatable GetRulePanelAnimatable() {
        if(this.rulePanelAnimatable == null) {
            this.rulePanelAnimatable = GetComponent<RulePanelAnimatable>();
        }
        return this.rulePanelAnimatable;
    }

}
