using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandCard : MonoBehaviour{

	Color DEFAULT = new Color(188, 0, 210);
	readonly Color POWERED = Color.red;


    readonly Color CLUBS = new Color(73, 239, 33);
    readonly Color DIAMONDS = new Color(252, 51, 101);
    readonly Color SPADES = new Color(238, 81, 255);
    readonly Color HEARTS = new Color(66, 236, 232);

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
            
            this.GetCardAttackText().GetTextMesh().color = DIAMONDS;

            switch (c.GetCardSuit()) {
                case Card.Suit.CLUBS:
                    DEFAULT = CLUBS;
                    break;
                case Card.Suit.SPADES:
                    DEFAULT = SPADES;
                    break;
                case Card.Suit.HEARTS:
                    DEFAULT = HEARTS;
                    break;
                case Card.Suit.DIAMONDS:
                    DEFAULT = DIAMONDS;
                    break;
            }

            GetCardAttackText().GetTextMesh().color = DEFAULT;

            this.GetCardAttackText().SetText(c.GetCardAttack().ToString());
			this.GetCardHealthText().SetText(c.GetCardHealth().ToString());
			EnableBonus(GetComponentInParent<UIHandCardManager>().IsPoweredUp);
		}
		else {
			this.GetCardAttackText().SetText("0");
			this.GetCardHealthText().SetText("0");
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
			SetAlpha(cardAttack.GetTextMesh(), 0);
			SetAlpha(cardHealth.GetTextMesh(), 0);
		}
		else {
			SetAlpha(cardBase, 1);
			SetAlpha(cardAttack.GetTextMesh(), 1);
			SetAlpha(cardHealth.GetTextMesh(), 1);
		}
	}

	public void EnableBonus(bool val) {
		if (this.cardReference.GetCardSuit() == Card.Suit.HEARTS)
			return;

		if (val) {
			int bonus = GameMaster.Instance.IsRuleHigher ? GameConstants.SkillValues.VORPAL_SWORD_BONUS : GameConstants.SkillValues.VORPAL_SWORD_BONUS * -1;
			GetCardAttackText().SetText((GetCardReference().GetCardAttack() + bonus).ToString());
			GetCardAttackText().GetTextMesh().color = POWERED; 
		}
		else {
			GetCardAttackText().SetText(GetCardReference().GetCardAttack().ToString());
			GetCardAttackText().GetTextMesh().color = DEFAULT;
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
