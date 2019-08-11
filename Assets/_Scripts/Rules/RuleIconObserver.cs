using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Rule icon change observer. Requires an Image component.
/// </summary>
public class RuleIconObserver : MonoBehaviour {
    private Image ruleImage;
    private void Awake() {
        //EventBroadcaster.Instance.AddObserver(EventNames.UI.RULE_UPDATE, UpdateRuleIcon);
    }

    public void Destroy() {
        //EventBroadcaster.Instance.RemoveObserver(EventNames.UI.RULE_UPDATE);
    }

    public void SetRuleIcon(Rules rule) {
		// TODO: RUle switch case
	}

    /// <summary>
    /// Rule image getter.
    /// </summary>
    /// <returns></returns>
    public Image GetRuleIcon() {
        if(this.ruleImage == null) {
            this.ruleImage = GetComponent<Image>();
        }
        return this.ruleImage;
    }
}
