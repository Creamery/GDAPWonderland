using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UIAttackMode : MainScreen_GameScene {
    
    private CardPanel_AM cardPanel;
	[SerializeField] private CenterPanelManager_AM centerPanel;

    public virtual void Initialize() {
        this.GetCardPanel().Initialize();
        this.UpdateHand();
    }

    /// <summary>
    /// Shows the attack mode screen.
    /// Updates the hand cards.
    /// Called by MainScreenManager.cs.
    /// </summary>
    public override void Show() {
        base.Show();
        this.UpdateCardPanel();
        // Get player hand cards and update hand card UI.
    }

    /// <summary>
    /// Automatically update hand cards.
    /// Called by MainScreenManager.cs.
    /// </summary>
    /// <param name="newHandCards"></param>
    public void UpdateHand() {
        Debug.Log("<color=green>RECEIVE Hand Card Update (AttackMode)</color>");
        this.UpdateCardPanel();
    }

    /// <summary>
    /// Updates the cards shown in the card panel by checking the current hand of the player.
    /// </summary>
    public void UpdateCardPanel() {
        this.GetCardPanel().UpdateCards(this.GetPlayer().GetHandCards());
    }

    /// <summary>
    /// CardPanel getter.
    /// </summary>
    /// <returns></returns>
    public CardPanel_AM GetCardPanel() {
        if (this.cardPanel == null) {
            this.cardPanel = GetComponentInChildren<CardPanel_AM>();
        }
        General.LogNull(cardPanel);
        return this.cardPanel;
    }

	public CenterPanelManager_AM GetCenterPanel() {
		if(this.centerPanel == null) {
			this.centerPanel = GetComponentInChildren<CenterPanelManager_AM>();
		}
		return this.centerPanel;
	}
}
