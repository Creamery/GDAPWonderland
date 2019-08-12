using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterFrame : CharacterFrameBase {
	[SerializeField] protected TextMeshProUGUI supportTextValue;

	[SerializeField] protected SoldierName soldierName;
    [SerializeField] protected SoldierQuote soldierQuote;


	private void Start() {
		ResetCharacterFrame();
	}

	/// <summary>
	/// Load the card and change the image and text accordingly
	/// </summary>
	/// <param name="card"></param>
	public override void LoadCard(Card card, bool hasSupport) {

		ResetCharacterFrame();

		//// Change soldier name based on suit (in uppercase)
		//this.GetSoldierName().SetText(card.GetCardSuit().ToString().ToUpper());

		this.GetAttackValue().SetText(card.GetCardHealth().ToString());

		if (hasSupport) {
			this.supportTextValue.SetText("?");
		}
		else {
			this.supportTextValue.SetText("-");
		}
		
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
