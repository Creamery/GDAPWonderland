using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_UIPlayerMenu : MainScreen_GameScene {
    [SerializeField] private UIHandCardContainer handCardContainer;

    public virtual void Initialize() {
    }

    /// <summary>
    /// Shows the player menu screen.
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
        Debug.Log("<color=green>RECEIVE Hand Card Update (PlayerMenu)</color>");
        this.UpdateCardPanel();
    }

    /// <summary>
    /// Updates the cards shown in the card panel by checking the current hand of the player.
    /// </summary>
    public void UpdateCardPanel() {
        //this.GetHandCardContainer().UpdateHand(this.GetPlayer().GetHandCards());
    }

    /// <summary>
    /// UIHandCardContainer getter.
    /// </summary>
    /// <returns></returns>
    public UIHandCardContainer GetHandCardContainer() {
        if(this.handCardContainer == null) {
            this.handCardContainer = GetComponentInChildren<UIHandCardContainer>();
        }
        return this.handCardContainer;
    }
}
