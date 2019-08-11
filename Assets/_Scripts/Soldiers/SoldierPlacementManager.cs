using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages the Soldier placement/swap actions. Also checks if the action requested is valid. Follows the Singleton pattern.
/// Same Type of Global Utility Class as CombatManager.cs
/// Server Authority.
/// </summary>
public class SoldierPlacementManager : MonoBehaviour {

	[Header("Setup")]
	[SerializeField] private SoldierDefenseGroup player1;
	[SerializeField] private SoldierDefenseGroup player2;

	private static SoldierPlacementManager sharedInstance;
	public static SoldierPlacementManager Instance {
		get { return sharedInstance; }
	}

	private void Awake() {
		sharedInstance = this;
	}

	private void Start() {
		if (player1 == null || player2 == null) {
			foreach(SoldierDefenseGroup sdg in GetComponentsInChildren<SoldierDefenseGroup>()) {
				if (sdg.GetPlayerNo() == 1)
					player1 = sdg;
				else if (sdg.GetPlayerNo() == 2)
					player2 = sdg;
			}
		}
	}

	public void SwapBackup(PlayerManager p_raycaster, Card handCard, SoldierBackup backupTarget) {
		if (p_raycaster == backupTarget.GetPlayer()) {
			if (backupTarget.GetCard() != null) {
				// Update SoldierBackup
				Card backupCard = p_raycaster.GetCardManager().GetDefenseManager().SwapBackCard(backupTarget.GetIndex(), handCard);
				// Update Hand
				Debug.Log("Swapping " + handCard.GetCardSuit() + handCard.GetCardRank() + " to " + backupCard.GetCardSuit() + backupCard.GetCardRank());
				p_raycaster.GetCardManager().GetHandManager().SwapToHand(handCard, backupCard);
			}
		}
	}

	public void ReplenishDefense(PlayerManager p_raycaster, Card handCard, SoldierBackup backupTarget) {
		if (p_raycaster == backupTarget.GetPlayer()) {
			if (backupTarget.GetCard() == null) {
				if (GameConstants.REPLENISH_AS_ACTION) {
					if (p_raycaster.GetMovesLeft() <= 0) {
						// If no more moves left
						PlayerPanel.Instance.GetMovesLeftAnimatable().Shake();
						p_raycaster.GetCardManager().GetDefenseManager().PostDefenseUpdate();
						return;
					}
				}
				// Update DefenseManager
				p_raycaster.GetCardManager().GetDefenseManager().ReplenishBackDefense(backupTarget.GetIndex(), handCard);
				// Update Hand
				p_raycaster.GetCardManager().GetHandManager().RemoveFromHand(handCard);

				if (GameConstants.REPLENISH_AS_ACTION) {
					// Successful defense replenish
					p_raycaster.ConsumeMove();
					ActionsLeftPanel.Instance.Show();
				}
			}
			else {
				SwapBackup(p_raycaster, handCard, backupTarget);
			}
			p_raycaster.GetCardManager().GetDefenseManager().PostDefenseUpdate();
		}
	}

	public void ReplenishDefense(PlayerManager p_raycaster, Card handCard, Soldier soldierTarget) {
		if (p_raycaster == soldierTarget.GetPlayerOwner()) {
            if (soldierTarget.GetCardReference() != null) {
                // Update DefenseManager
                p_raycaster.GetCardManager().GetDefenseManager().ReplenishFrontDefense(soldierTarget.GetIndex(), handCard);
				// Update Hand
				p_raycaster.GetCardManager().GetHandManager().RemoveFromHand(handCard);
			}
			else {
				//Update DefenseManager
				Card soldierCard = p_raycaster.GetCardManager().GetDefenseManager().SwapFrontCard(soldierTarget.GetIndex(), handCard);
				//Update Hand
				p_raycaster.GetCardManager().GetHandManager().SwapToHand(handCard, soldierCard);
			}
		}
	}

	/// <summary>
	/// Sets the backup cards of the specified player to be targetable.
	/// This gives the misisng backup cards a placeholder block.
	/// </summary>
	/// <param name="player"></param>
	/// <param name="val"></param>
	public void SetTargetable(PlayerManager player, bool val) {
		SoldierDefenseGroup targetPlayer = player == player1.GetPlayer() ? player1 : player2;
		targetPlayer.SetTargetableAll(val);
	}

    public void ShowPlayerHeart(int player) {
        switch(player) {
            case 1:
                this.player1.GetHeartManager().Show();
                this.player2.GetHeartManager().Hide();
                break;
            case 2:
                this.player2.GetHeartManager().Show();
                this.player1.GetHeartManager().Hide();
                break;
        }
    }
}
