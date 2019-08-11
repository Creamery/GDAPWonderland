﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class MultipleCardImageChangeManager : MonoBehaviour {
    private Card cardReference;
    [SerializeField] private Image cardImage;
    [SerializeField] private CardAttackText cardAttack;
    [SerializeField] private CardHealthText cardHealth;



    /// <summary>
    /// Deprecated. Used for backwards compatibility with single card burning.
    /// </summary>
    /// <param name="card"></param>
    public void ChangeImage(Card card) {
        this.GetCardImage().sprite = General.GetCardSprite(card.GetCardSuit(),
            GameMaster.Instance.GetCurPlayer().playerNo, card.GetCardRank());
    }

    /// <summary>
    /// Call this to update card values
    /// </summary>
    public void UpdateCardValues(Card card) {
        this.cardReference = card;
        if (this.GetCardReference() != null) {
            Card c = GetCardReference();
            GetCardImage().sprite = General.GetCardSprite(c.GetCardSuit(), MainScreenManager_GameScene.Instance.GetPlayer().playerNo, c.GetCardRank());
            this.GetCardAttackText().SetText(c.GetCardAttack().ToString());
            this.GetCardHealthText().SetText(c.GetCardHealth().ToString());
            //EnableBonus(GetComponentInParent<UIHandCardManager>().IsPoweredUp);
        }
        else {
            this.GetCardAttackText().SetText("0");
            this.GetCardHealthText().SetText("0");
        }
    }

    public Card GetCardReference() {
        return this.cardReference;
    }

    public Image GetCardImage() {
        if(this.cardImage == null) {
            this.cardImage = GetComponent<Image>();
        }
        return this.cardImage;
    }

    public CardHealthText GetCardHealthText() {
        if (this.cardHealth == null) {
            this.cardHealth = GetComponentInChildren<CardHealthText>();
        }
        return this.cardHealth;
    }
    public CardAttackText GetCardAttackText() {
        if (this.cardAttack == null) {
            this.cardAttack = GetComponentInChildren<CardAttackText>();
        }
        return this.cardAttack;
    }
}