using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// Rule text change observer. Requires a TextMeshPro component.
/// </summary>
public class RuleTextObserver : MonoBehaviour {
    private TextMeshProUGUI ruleText;
    private void Awake() {
        //EventBroadcaster.Instance.AddObserver(EventNames.UI.RULE_UPDATE, UpdateRuleText);
    }

    public void Destroy() {
        //EventBroadcaster.Instance.RemoveObserver(EventNames.UI.RULE_UPDATE);
    }

    public void SetRuleText(string newText) { 
        this.GetRuleText().text = newText.ToUpper();
    }

    /// <summary>
    /// Rule text getter.
    /// </summary>
    /// <returns></returns>
    public TextMeshProUGUI GetRuleText() {
        if (this.ruleText == null) {
            this.ruleText = GetComponent<TextMeshProUGUI>();
        }
        return this.ruleText;
    }
	
}
