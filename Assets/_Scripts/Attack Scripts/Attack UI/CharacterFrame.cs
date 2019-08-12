using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFrame : CharacterFrameBase {
    [SerializeField] protected SoldierName soldierName;
    [SerializeField] protected SoldierQuote soldierQuote;



    /// <summary>
    /// Load the card and change the image and text accordingly
    /// </summary>
    /// <param name="card"></param>
    public override void LoadCard(Card card, bool isMultiple) {
        //this.loadedCard = card;

        // Change soldier name based on suit (in uppercase)
        this.GetSoldierName().SetText(card.GetCardSuit().ToString().ToUpper());

        // Change quote based on suit and player
        if (isMultiple) {
            this.GetSoldierQuote().SetText(Quotes.UI_DEFAULT_QUOTE_MULTI);
        }
        else {
            this.GetSoldierQuote().SetText(Quotes.GetAttackUIQuote(
                General.GetPlayerNo(MainScreenManager_GameScene.Instance.GetPlayer()),
                card.GetCardSuit()));
        }

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
    /// Soldier name getter.
    /// </summary>
    /// <returns></returns>
    public SoldierName GetSoldierName() {
        if (this.soldierName == null) {
            this.soldierName = GetComponentInChildren<SoldierName>();
        }
        return this.soldierName;
    }

    /// <summary>
    /// Soldier quote getter.
    /// </summary>
    /// <returns></returns>
    public SoldierQuote GetSoldierQuote() {
        if (this.soldierQuote == null) {
            this.soldierQuote = GetComponentInChildren<SoldierQuote>();
        }
        return this.soldierQuote;
    }

}
