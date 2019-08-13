using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHistoryModels : MonoBehaviour {

	[SerializeField] protected SoldierHistoryManager parent;

    [SerializeField] protected TextMeshTeamLeadObjectMarker textTeamLead;
    [SerializeField] protected TextMeshTeamAssistObjectMarker textTeamAssist;
    [SerializeField] protected TeamWinObjectMarker teamWinLogo;
	// TODO: only enable either team win or target win logo

    [SerializeField] protected TextMeshTargetLeadObjectMarker textTargetLead;
    [SerializeField] protected TextMeshTargetAssistObjectMarker textTargetAssist;
    [SerializeField] protected TargetWinObjectMarker targetWinLogo;


	// Enable/disable these depending on the rule shown in the history entry
    [SerializeField] protected HighRuleObjectMarker ruleHighFront;
    [SerializeField] protected HighRuleObjectMarker ruleHighBack;
    [SerializeField] protected LowRuleObjectMarker ruleLowFront;
    [SerializeField] protected LowRuleObjectMarker ruleLowBack;

	public void SetHistory(History toShow) {
		bool isRuleHigher = toShow.isRuleHigher;
		bool isAtkWinner = toShow.DoesAttackerWin;
		Card[] atkCards = toShow.attackerCards;
		Card[] defCards = toShow.defenderCards;

		SetWinner(isAtkWinner);
		ShowRule(isRuleHigher);
		SetAttackerInfo(atkCards);
		SetDefenderInfo(defCards);
	}

	public void RemoveHistory() {
		//Clear attacker and defender texts
		SetAttackerInfo(null);
		SetDefenderInfo(null);

		//Hide Win logos
		teamWinLogo.gameObject.SetActive(false);
		targetWinLogo.gameObject.SetActive(false);

		//Hide rule logos
		ruleHighFront.gameObject.SetActive(false);
		ruleHighBack.gameObject.SetActive(false);

		ruleLowFront.gameObject.SetActive(false);
		ruleLowBack.gameObject.SetActive(false);
	}

	private void SetDefenderInfo(Card[] defenderCards) {
		if(defenderCards == null) {
			textTargetLead.SetText("");
			textTargetAssist.SetText("");
			return;
		}

		string defTxt1 = defenderCards[0].GetCardHealth().ToString();
		string defTxt2 = "-";
		if (defenderCards[1] != null)
			defTxt2 = defenderCards[1].GetCardHealth().ToString();

		textTargetLead.SetText(defTxt1);
		textTargetAssist.SetText(defTxt2);
	}

	private void SetAttackerInfo(Card[] attackerCards) {
		if (attackerCards == null) {
			textTeamLead.SetText("");
			textTeamAssist.SetText("");
			return;
		}

		string atkTxt1 = attackerCards[0].GetCardAttack().ToString();
		string atkTxt2 = "-";
		if (attackerCards[1] != null)
			atkTxt2 = attackerCards[1].GetCardAttack().ToString();

		textTeamLead.SetText(atkTxt1);
		textTeamAssist.SetText(atkTxt2);
	}

	private void SetWinner(bool doesAttackerWin) {
		teamWinLogo.gameObject.SetActive(doesAttackerWin);
		targetWinLogo.gameObject.SetActive(!doesAttackerWin);
	}

	private void ShowRule(bool isHigher) {
		ruleHighFront.gameObject.SetActive(isHigher);
		ruleHighBack.gameObject.SetActive(isHigher);

		ruleLowFront.gameObject.SetActive(!isHigher);
		ruleLowBack.gameObject.SetActive(!isHigher);
	}

	public void InitializeComponents() {
		if (parent == null)
			parent = GetComponentInParent<SoldierHistoryManager>();

		// Team Components
		if (textTeamLead == null)
			textTeamLead = GetComponentInChildren<TextMeshTeamLeadObjectMarker>();
		if (textTeamAssist == null)
			textTeamAssist = GetComponentInChildren<TextMeshTeamAssistObjectMarker>();
		if (teamWinLogo == null)
			teamWinLogo = GetComponentInChildren<TeamWinObjectMarker>();
		// Target Components
		if (textTargetLead == null)
			textTargetLead = GetComponentInChildren<TextMeshTargetLeadObjectMarker>();
		if (textTargetAssist == null)
			textTargetAssist = GetComponentInChildren<TextMeshTargetAssistObjectMarker>();
		if (targetWinLogo == null)
			targetWinLogo = GetComponentInChildren<TargetWinObjectMarker>();
		// Rule Marker Components
		GameObject frontModel = null, backModel = null;
		foreach(Transform t in transform) {
			if (t.name.Equals("Models Front"))
				frontModel = t.gameObject;
			else if (t.name.Equals("Models Reverse"))
				backModel = t.gameObject;
		}
		// Front Models
		if (frontModel != null) {
			if (ruleHighFront == null)
				ruleHighFront = frontModel.GetComponentInChildren<HighRuleObjectMarker>();
			if (ruleLowFront == null)
				ruleLowFront = frontModel.GetComponentInChildren<LowRuleObjectMarker>();
		}
		else
			Debug.LogError("Failed to Initialize Soldier History Models");

		// Back Models
		if (backModel != null) {
			if (ruleHighBack == null)
				ruleHighBack = backModel.GetComponentInChildren<HighRuleObjectMarker>();
			if (ruleLowBack == null)
				ruleLowBack = backModel.GetComponentInChildren<LowRuleObjectMarker>();
		}
		else
			Debug.LogError("Failed to Initialize Soldier History Models");

	}
}
