﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterFrameBase : MonoBehaviour {

    [SerializeField] protected RedSelector redSelector;
    [SerializeField] protected WhiteSelector whiteSelector;

    [SerializeField] protected TextMeshProUGUI textAttackValue;

    // protected Card loadedCard;

    /// <summary>
    /// Load the card and change the image and text accordingly
    /// </summary>
    /// <param name="card"></param>
    public virtual void LoadCard(Card card, bool isMultiple) {
        // this.loadedCard = card;

        this.HideAllSelectorAvatars();
        // Enable the image based on suit and player
        switch (General.GetPlayerNo(MainScreenManager_GameScene.Instance.GetPlayer())) {
            case 1:
                this.GetWhiteSelector().EnableSuit(card.GetCardSuit());
                break;
            case 2:
                this.GetRedSelector().EnableSuit(card.GetCardSuit());
                break;
        }
    }

    /// <summary>
    /// Hides the avatars from all selectors.
    /// </summary>
    public void HideAllSelectorAvatars() {
        this.GetWhiteSelector().HideAll();
        this.GetRedSelector().HideAll();
    }

    /// <summary>
    /// White selector.
    /// </summary>
    /// <returns></returns>
    public WhiteSelector GetWhiteSelector() {
        if (this.whiteSelector == null) {
            this.whiteSelector = GetComponentInChildren<WhiteSelector>();
        }
        return this.whiteSelector;
    }

    /// <summary>
    /// Red selector getter.
    /// </summary>
    /// <returns></returns>
    public RedSelector GetRedSelector() {
        if (this.redSelector == null) {
            this.redSelector = GetComponentInChildren<RedSelector>();
        }
        return this.redSelector;
    }
}