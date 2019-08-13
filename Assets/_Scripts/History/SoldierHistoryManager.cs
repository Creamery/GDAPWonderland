using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierHistoryManager : MonoBehaviour {
	
    [SerializeField] protected SoldierHistoryModels soldierHistoryModels;

	[Header("Debugging")]
	[SerializeField] protected List<History> historyQ;

	private void Awake() {
		GetSoldierHistoryModels().InitializeComponents();
		ShowHistoryPanel(false);
		ClearDisplayedHistories();

		historyQ = new List<History>();
	}

	public void AddHistory(History historyEntry) {
		historyQ.Add(historyEntry);
		SetHistory(historyEntry);
	}

	private void SetHistory(History historyEntry) {
		GetSoldierHistoryModels().SetHistory(historyEntry);
	}

	public void ClearDisplayedHistories() {
		GetSoldierHistoryModels().RemoveHistory();
	}

	public void ClearHistories() {
		ClearDisplayedHistories();
		historyQ.Clear();
	}

	public void ShowHistoryPanel(bool val) {
		GetSoldierHistoryModels().gameObject.SetActive(val);
	}

	public SoldierHistoryModels GetSoldierHistoryModels() {
		if (this.soldierHistoryModels == null)
			this.soldierHistoryModels = GetComponentInChildren<SoldierHistoryModels>();
		return this.soldierHistoryModels;
	}
}


public class History {
	public readonly PlayerManager attacker, defender;

	private bool doesAttackerWin;
	public bool DoesAttackerWin {
		get {
			return doesAttackerWin;
		}

		set {
			doesAttackerWin = value;
		}
	}

	public readonly bool isRuleHigher;

	public readonly Card[] attackerCards;

	public readonly Card[] defenderCards;

	public History(bool isRuleHigher, PlayerManager defender, Card[] attackerCards, Card[] defenderCards) {
		this.isRuleHigher = isRuleHigher;

		this.defender = defender;
		this.attacker = GameMaster.Instance.GetOpposingPlayer(defender);

		this.attackerCards = new Card[GameConstants.MAX_ATTACK_COMBINATION];
		for (int i = 0; i < attackerCards.Length; i++) {
			this.attackerCards[i] = attackerCards[i];
		}

		this.defenderCards = new Card[2];
		for (int i = 0; i < defenderCards.Length; i++) {
			this.defenderCards[i] = defenderCards[i];
		}
	}

	public History(bool isRuleHigher, bool doesAtkWin, PlayerManager defender, Card[] attackerCards, Card[] defenderCards) {
		this.isRuleHigher = isRuleHigher;
		this.DoesAttackerWin = doesAtkWin;

		this.defender = defender;
		this.attacker = GameMaster.Instance.GetOpposingPlayer(defender);

		this.attackerCards = new Card[GameConstants.MAX_ATTACK_COMBINATION];
		for (int i = 0; i < attackerCards.Length; i++) {
			this.attackerCards[i] = attackerCards[i];
		}

		this.defenderCards = new Card[2];
		for(int i = 0; i < defenderCards.Length; i++) {
			this.defenderCards[i] = defenderCards[i];
		}
	}
}