using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the combat mechanics of the game. (i.e. damage handling, card discards). Singleton monobehaviour -- ONLY
/// ONE INSTANCE OF THE SCRIPT MUST EXIST IN A SCENE.
/// </summary>
public class CombatManager : MonoBehaviour {

	private static CombatManager sharedInstance;
	public static CombatManager Instance
	{
		get { return sharedInstance; }
	}

	[SerializeField] private PlayerManager player1;
	[SerializeField] private PlayerManager player2;

	private int atkModifier;

    private Soldier disabledSoldier;

    private void Awake()
	{
		sharedInstance = this;
		atkModifier = 0;
	}

	// Use this for initialization
	void Start () {
	}
	
	public void AddPlayer(PlayerManager player)
	{
		if (player1 == null)
			this.player1 = player;
		else if (player2 == null)
			this.player2 = player;
		else
			Debug.LogError("Both players have been assigned. Use (Playermanager,int) overload to replace currently assigned player.");
	}

	public void AddPlayer(PlayerManager player, int playerNo) {
		switch (playerNo) {
			case 1:
				this.player1 = player;
				Debug.Log("Player 1 Added to CombatManager");
				break;
			case 2:
				this.player2 = player;
				Debug.Log("Player 2 Added to CombatManager");
				break;
			default:
				Debug.LogError("Invalid PlayerNo!");
				break;
		}
	}

	/// <summary>
	/// handles the attack effects
	/// </summary>
	/// <param name="attacker"></param>
	/// <param name="bullets"></param>
	/// <param name="defender"></param>
	/// <param name="target"></param>
	/// <returns>true if the bullet can be fired again, false if otherwise</returns>
	public bool HandleAttack(PlayerManager attacker, List<Card> bullets, Soldier target) {
		bool isRuleHigher = GameMaster.Instance.IsRuleHigher;

		if (target.GetPlayerOwner() != attacker && target.GetCardReference() != null) {
			int atkDamage = 0;
			foreach (Card c in bullets) {
				atkDamage += c.GetCardAttack() + this.atkModifier;
			}

			if (bullets.Count > 1) {
				target.TakeDamage(atkDamage, isRuleHigher);
				this.RefreshSoldiers();
				return false;
			}
			// If single bullet is being fired
			else if(bullets.Count == 1) { 
				// Store in variable since it has to be checked before and after taking damage
				int atksLeft = bullets[0].ReduceNumOfAttacks();

				// -- For Two-shot bullet types --
				// If target will live, make it wear "Disable" after playing damage animation
				if (target.WillLive(atkDamage) && atksLeft > 0) {
					target.SetDisableOnDamage(true);
					this.disabledSoldier = target;
				}
				else {
					target.SetDisableOnDamage(false);
				}

				target.TakeDamage(atkDamage, isRuleHigher);

				// Successful Shot Heuristics
				if (atksLeft <= 0) {
					// No more shots, stop firing
					this.RefreshSoldiers();
					return false;
				}
				else {
					// If still have shots, check front defense if there are any other targets aside from previous one.
					if (!target.GetPlayerOwner().GetCardManager().GetDefenseManager().HasFrontDefense(target.GetCardReference())) {

                        // Remove transparent skin
                        this.RefreshSoldiers();

                        if (!target.WillLive(atkDamage)) {
                            // Target will die, can direct attack
                            return true;
                        }
                        else {
                            // If there is no more other front defense, stop firing
                            return false;
                        }
					}
					else {
						// If there still are targets, fire again.
						return true;
					}
				}
			}
			
		}
		return true;	// Current target either: does not exist or is the attacker's own target.
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="attacker"></param>
	/// <param name="bullet"></param>
	/// <param name="heartLOC"></param>
	/// <returns>True if attack is valid, false if not.
	public bool HandleDirectAttack(PlayerManager attacker, List<Card> bullets, LifeObjectCollider heartLOC) {
		bool validHit = heartLOC.IsHit(attacker);
		if (validHit) {
			return true;
		}
		else
			return false;
	}

    // Enables the disabled soldier, if applicable
    public void RefreshSoldiers() {
        if (this.disabledSoldier != null) {
            this.disabledSoldier.AnimateEnable();
        }
        this.disabledSoldier = null;
    }

	public void SetAtkModifier(int val) {
		this.atkModifier = val;
	}

	public void ResetAtkModifier() {
		SetAtkModifier(0);
	}

	public int GetAtkModifier() {
		return this.atkModifier;
	}
}
