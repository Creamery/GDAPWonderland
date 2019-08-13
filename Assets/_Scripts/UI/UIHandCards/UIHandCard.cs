﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandCard : MonoBehaviour{

    readonly Color DEFAULT = new Color(188, 0, 210);
	readonly Color POWERED = Color.red;


    private Card cardReference;
	[SerializeField] private CardAttackText cardAttack;
	[SerializeField] private CardHealthText cardHealth;
	[SerializeField] private Image cardBase;

	/// <summary>
	/// CardAttackText getter.
	/// </summary>
	/// <returns></returns>
	public CardAttackText GetCardAttackText() {
		if (this.cardAttack == null) {
			this.cardAttack = GetComponentInChildren<CardAttackText>();
		}
		return this.cardAttack;
	}

	/// <summary>
	/// CardHealthText getter.
	/// </summary>
	/// <returns></returns>
	public CardHealthText GetCardHealthText() {
		if (this.cardHealth == null) {
			this.cardHealth = GetComponentInChildren<CardHealthText>();
		}
		return this.cardHealth;
	}


	public void UpdateCardValues() {
		if (this.GetCardReference() != null) {
			Card c = GetCardReference();
			cardBase.sprite = General.GetCardSprite(c.GetCardSuit(), MainScreenManager_GameScene.Instance.GetPlayer().playerNo, c.GetCardRank());

            // this.recolorText(c.GetCardSuit());
            // GetCardAttackText().GetTextMesh().color = DEFAULT;

            this.GetCardAttackText().SetTextUI(c.GetCardAttack().ToString(), c.GetCardSuit());
			this.GetCardHealthText().SetTextUI(c.GetCardHealth().ToString(), c.GetCardSuit());
            EnableBonus(GetComponentInParent<UIHandCardManager>().IsPoweredUp);
		}
		else {
			// this.GetCardAttackText().SetTextUI("0");
			// this.GetCardHealthText().SetTextUI("0");
            this.GetCardAttackText().SetTextUI("");
            this.GetCardHealthText().SetTextUI("");
        }
	}

	/// <summary>
	/// Card getter.
	/// </summary>
	/// <returns></returns>
	public Card GetCardReference() {
		return this.cardReference;
	}

	/// <summary>
	/// Sets the Card reference for the UICard display.
	/// </summary>
	/// <param name="card">The card to be referenced</param>
	public void Set(Card card) {
		this.cardReference = card;
		this.UpdateCardValues();
	}

	public void ShouldShow(bool val) {
		if (!val) {
			SetAlpha(cardBase, 0);
			SetAlpha(cardAttack.GetTextMeshUI(), 0);
			SetAlpha(cardHealth.GetTextMeshUI(), 0);
		}
		else {
			SetAlpha(cardBase, 1);
			SetAlpha(cardAttack.GetTextMeshUI(), 1);
			SetAlpha(cardHealth.GetTextMeshUI(), 1);
		}
	}

	public void EnableBonus(bool val) {
		if (this.cardReference.GetCardSuit() == Card.Suit.HEARTS)
			return;

		if (val) {
			int bonus = GameMaster.Instance.IsRuleHigher ? GameConstants.SkillValues.VORPAL_SWORD_BONUS : GameConstants.SkillValues.VORPAL_SWORD_BONUS * -1;
			GetCardAttackText().SetTextUI((GetCardReference().GetCardAttack() + bonus).ToString(), GetCardReference().GetCardSuit());
			GetCardAttackText().GetTextMeshUI().color = POWERED; 
		}
		else {
			GetCardAttackText().SetTextUI(GetCardReference().GetCardAttack().ToString(), GetCardReference().GetCardSuit());
			// GetCardAttackText().GetTextMesh().color = DEFAULT;
            // this.recolorText(GetCardReference().GetCardSuit());
        }
	}

	private void SetAlpha(Image comp, float newAlpha) {
		Color oldColor = comp.color;
		comp.color = new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
	}

	private void SetAlpha(TextMeshProUGUI comp, float newAlpha) {
		Color oldColor = comp.color;
		comp.color = new Color(oldColor.r, oldColor.g, oldColor.b, newAlpha);
	}
}
