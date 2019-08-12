using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleInfo {
	public Rules rule;
	public string forceText;

	public RuleInfo(Rules rule=Rules.TEXT_ONLY, string text = "") {
		this.rule = rule;
		if (text != "")
			this.rule = Rules.TEXT_ONLY;
		this.forceText = text;
	}

	public static Rules ToRuleEnum(string ruleText) {
		ruleText = ruleText.ToLower().Trim();
		switch (ruleText) {
			case "lower": return Rules.HIGHER;
			case "higher": return Rules.LOWER;

			case "movex2": return Rules.MOVEX2;
			case "moved2": return Rules.MOVED2;
			case "movep2": return Rules.MOVEP2;
			case "movep3": return Rules.MOVEP3;
			case "movep4": return Rules.MOVEP4;
			case "movep5": return Rules.MOVEP5;
			case "bomb": return Rules.BOMB;
			case "summon": return Rules.SUMMON;
			case "strdef": return Rules.STRDEF;
			case "strhand": return Rules.STRHAND;

			case "red": return Rules.RED_CARDS_ONLY;
			case "white": return Rules.WHITE_CARDS_ONLY;
			case "replenishdeck": return Rules.REPLENISH_DECK;
			case "replenishdefense": return Rules.REPLENISH_DEFENSE;
			case "drawcard": return Rules.DRAW_CARD;
			case "removecards": return Rules.REMOVE_CARDS;
			case "drinkme": return Rules.DRINK_ME;
			case "eatme": return Rules.EAT_ME;
			default: return Rules.TEXT_ONLY;
		}
	}
}

public enum Rules {
    UNKNOWN,
	HIGHER, LOWER,
	MOVEX2, MOVED2, MOVEP2, MOVEP3, MOVEP4, MOVEP5,
	STRDEF, STRHAND,
	BOMB, SUMMON,
	RED_CARDS_ONLY, WHITE_CARDS_ONLY,
    REPLENISH_DEFENSE, REPLENISH_DECK,
    DRAW_CARD, REMOVE_CARDS,
    EAT_ME, DRINK_ME, TEXT_ONLY
}

public static class RulesExtensions {
	public static string ToRuleText(this Rules rule) {
		switch (rule) {
			case Rules.HIGHER: return "Higher attack damage is effective against defendants";
			case Rules.LOWER: return "Lower attack damage is effective against defendants";
			case Rules.RED_CARDS_ONLY: return "Hearts or Diamonds Only";
			case Rules.WHITE_CARDS_ONLY: return "Spades or Clubs Only";
			case Rules.EAT_ME: return "Your Defenses increased by 1";
			case Rules.DRINK_ME: return "Your Defenses are decreased by 1";
			default: return "No Card Restriction";
		}
	}
}

public class RuleManager : MonoBehaviour{
	public const string RULE_PARAM = "RULE";

    private static RuleManager sharedInstance;
    public static RuleManager Instance {
        get {
			return sharedInstance;
        }
    }

	[SerializeField] private RuleTextObserver ruleText;
	[SerializeField] private RuleIconObserver ruleIcon;
    [SerializeField] private RulePanelAnimatable rulePanelAnimatable;
	private void Awake() {
		sharedInstance = this;
	}

	private void Start() {
		EventBroadcaster.Instance.AddObserver(EventNames.UI.RULE_UPDATE, UpdateRule);
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.RULE_UPDATE);
	}

	public void UpdateRule(Parameters param) {
		RuleInfo ruleInfo = param.GetObjectExtra(RULE_PARAM) as RuleInfo;
		if(ruleInfo.rule == Rules.TEXT_ONLY) {
			ruleText.SetRuleText(ruleInfo.forceText);
		}
		else {
			ruleText.SetRuleText(ruleInfo.rule.ToRuleText());
		}
	}
    public void PlayButtonCancel() {
        SoundManager.Instance.Play(AudibleNames.Button.CANCEL);
    }
    public void RuleDebut(Rules rule) {
        this.GetRulePanelAnimatable().NewRule(rule);
    }
    public void Show() {
        this.GetRulePanelAnimatable().Show();
    }
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