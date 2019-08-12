using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// The soldier game object that appears on screen.
/// It handles all the animation calls(i.e.summon, death, etc.).
/// It is a child of the SoldierManager game object.
/// </summary>
public class Soldier : MonoBehaviour {
	[Header("Debugging")]
	[SerializeField] private Card cardReference;
    [SerializeField] private int currentHealth;
	//[SerializeField] private int defenseIndex;
	[Header("Setup")]
	[SerializeField] private OutlinedModel outline;
	[SerializeField] private SoldierModelHandler modelContainer;
	//[SerializeField] private GameObject placeHolderBlock;
	[SerializeField] private GameObject colliderObject;


    [SerializeField] private SoldierHeartModel heartModel;


    [Header("Animation")]
    [SerializeField] private SoldierSkinnableManager soldierSkinnableManager;
    [SerializeField] private SoldierAnimatable soldierAnimatable;

	// TEMPORARY
	[SerializeField] private GameObject healthTextContainer;
    [SerializeField] private TextMeshPro healthText;
    [SerializeField] private TextMeshPro healthTextBack;

    private SoldierDefenseGroup parent;
    private bool disableOnDamage = false;

	private void Awake() {
		this.parent = GetComponentInParent<SoldierDefenseGroup>();
	}

	private void Start() {
		this.UpdateHealth();
		//this.defenseIndex = GetIndex();
	}

	/// <summary>
	/// Called to shoot a soldier.
	/// (Possibly by AttackManager.cs)
	/// </summary>
	/// <param name="damage"></param>
	public void TakeDamage(int damage, bool isRuleHigher) {
		//bool isRuleHigher = GameMaster.IsHigher;
		if (isRuleHigher) {
			if (damage >=  this.GetReinforcedHealth()) {
				this.currentHealth = 0;
				this.UpdateHealth();
				Kill();
			}
			else {
				// Failed to kill
			}
		}
		else {
			if(damage <= this.GetReinforcedHealth()) {
				this.currentHealth = 0;
				this.UpdateHealth();
				Kill();
			}
			else {
				// Failed to kill
			}
		}

		//// REMOVE LINES BELOW
		//this.currentHealth -= damage;
		//this.UpdateHealth();
  //      // TODO: CALL damage animation
		//if (this.currentHealth <= 0) {
		//	Kill();
		//}
  //      else {
  //          this.EventDisable(); // Check if character disabled skin should be activated
  //      }
	}


	public int GetReinforcedHealth() {
		int thisIndex = this.parent.FindSoldierIndex(this);

		int thisBackupHealth = 0;
		SoldierBackup thisBackup = this.parent.GetSoldierBackup(thisIndex);
		if (thisBackup.GetCard() != null)
			thisBackupHealth = thisBackup.GetCard().GetCardHealth();

		int reinforcedHealth = this.currentHealth + thisBackupHealth;

		return reinforcedHealth;
	}

	public void BuffHealth(int amount) {
		if (cardReference == null)
			return;
		this.currentHealth += amount;
		this.UpdateHealth();
	}

    /// <summary>
    /// Function that simulates the damage and returns true or false
    /// depending on whether the soldier will livee.
    /// </summary>
    /// <param name="damage"></param>
    /// <returns></returns>
    public bool WillLive(int damage) {
        int simulatedHealth = this.currentHealth - damage;
        if(simulatedHealth > 0) {
            return true;
        }
        return false;
    }

        /// <summary>
        /// Called by TakeDamage
        /// </summary>
    public void Kill() {
        if (this.GetHeartModel() != null) {
            this.GetHeartModel().Hide();
        }
		// NEW	8/9/2019
		int thisIndex = this.parent.FindSoldierIndex(this);
		
		SoldierBackup thisBackup = this.parent.GetSoldierBackup(thisIndex);
		if (thisBackup != null)
			thisBackup.RemoveCard();
		// END OF NEW 8/9/2019

		this.GetSoldierAnimatable().Death();
		this.SetHealthTextActive(false);
		// Death animations could be placed here (or before?)
	}

    /// <summary>
    /// Called by the death animation event.
    /// </summary>
    public void OnKillAnimComplete() {
        GetPlayerOwner().GetCardManager().KillDefense(GetIndex());
    }

    /// <summary>
    /// Play spawn animation.
    /// </summary>
    public void Spawn() {
        if(this.GetHeartModel() != null) {
            this.GetHeartModel().Show();
        }

		this.SetHealthTextActive(true);
		this.GetSoldierAnimatable().Spawn();
    }

    // Fully heal the card. Called at the end of each turn. [TODO]
    public void Heal() {
		this.currentHealth = this.cardReference.GetCardHealth ();
		this.UpdateHealth();
	}


    public void SetDisableOnDamage(bool value) {
        this.disableOnDamage = value;
    }

    // Animation event functions
    public void EventDisable() {
        if(this.disableOnDamage) {
            this.SetDisableOnDamage(false);
            this.AnimateDisable();
        }
    }

    public void AnimateDisable() {
        this.GetSoldierSkinnableManager().SkinObject(SoldierSkinnable.Type.DISABLE);
    }

    public void AnimateEnable() {
        this.GetSoldierSkinnableManager().SkinObject(SoldierSkinnable.Type.DEFAULT);

    }

    /// <summary>
    /// Called when the soldier's health visual is to be updated.
    /// </summary>
    public void UpdateHealth() {
		// TEMPORARY
		if (this.cardReference == null) {
            this.healthText.SetText("");
        }
		else {
            this.healthText.SetText(this.currentHealth.ToString());
            this.GetHealthTextBack().SetText(this.currentHealth.ToString());
        }

        //if(this.healthTextBack != null) {
        //    this.healthTextBack.SetText(this.healthText.text);
        //}

	}


	#region Getter Functions
	/**
	 * Getter functions.
	 * Used as a safety measure for null checking
	 * (But ideally should not enter the null check condition).
	 **/

	public OutlinedModel GetOutline() {
		if(this.outline == null) {
			this.outline = GetComponentInChildren<OutlinedModel>();
		}
		return this.outline;
	}

	public Card GetCardReference() {
		return this.cardReference;
	}

	public int GetIndex() {
		return parent.FindSoldierIndex(this);
	}

	public PlayerManager GetPlayerOwner() {
		return this.parent.GetPlayer();
	}

    /// <summary>
    /// Skinnable object manager getter. Assumes the component is a child.
    /// </summary>
    /// <returns></returns>
    public SoldierSkinnableManager GetSoldierSkinnableManager() {
        if(this.soldierSkinnableManager == null) {
            this.soldierSkinnableManager = GetComponentInChildren<SoldierSkinnableManager>();
        }
        return this.soldierSkinnableManager;
    } 

    /// <summary>
    /// Soldier animatable getter. Assumes component is in game object.
    /// </summary>
    /// <returns></returns>
    public SoldierAnimatable GetSoldierAnimatable() {
        if(this.soldierAnimatable == null) {
            this.soldierAnimatable = GetComponent<SoldierAnimatable>();
        }
        return this.soldierAnimatable;
    }

    public TextMeshPro GetHealthTextBack() {
        if(this.healthTextBack == null) {
            this.healthTextBack = GetComponentInChildren<HealthTextBackMarker>().gameObject.GetComponent<TextMeshPro>();
        }
        return this.healthText;
    }

    public SoldierHeartModel GetHeartModel() {
        if(this.heartModel == null) {
            this.heartModel = GetComponentInChildren<SoldierHeartModel>();
        }
        return this.heartModel;
    }
    public int GetCurrentHealth() {
        return this.currentHealth;
    }
	#endregion

	#region Setter Functions
	///// <summary>
	///// Used for replenish defense. Set the gameobject to be targettable by replenish defense.
	///// NOTE: Even though the soldier object contains a card reference (thus Settargettable wont enable),
	///// the object can still be targetted for replenish. This method mainly provides a placeholder visual for
	///// empty soldier slots.
	///// </summary>
	///// <param name="val"></param>
	//public void SetTargetable(bool val) {
	//	if(this.cardReference == null) {
	//		this.placeHolderBlock.SetActive(val);
	//	}
	//}

	public void EnableOutline(bool val) {
		this.GetOutline().enabled = val;
	}

	public void SetHealthTextActive(bool val) {
		this.healthTextContainer.SetActive(val);
	}

	public void SetCardReference(Card card) {
		//if (card == cardReference) {
		//	if(cardReference != null) {
		//		this.cardReference = card;
		//		int healthBonus = cardReference.OriginalHealth - cardReference.GetCardHealth();
		//		this.currentHealth += healthBonus;
		//		this.UpdateHealth();
		//		Debug.Log("update soldier hp");
		//	}
		//	return;
		//}

		if (card == cardReference) {
			return;
		}

		if (card == null) {
			this.cardReference = null;
			this.currentHealth = -1;
			this.modelContainer.SetSoldierModel(Card.Suit.HEARTS, GetPlayerOwner().playerNo, -1);
			this.modelContainer.gameObject.SetActive(false);
		}
		else {
			this.cardReference = card;
			this.currentHealth = card.GetCardHealth();
			this.modelContainer.gameObject.SetActive(true);
			this.modelContainer.SetSoldierModel(card.GetCardSuit(), GetPlayerOwner().playerNo, card.GetCardRank());
            this.Spawn(); // Play spawn animation
		}
		this.UpdateHealth();
	}
	#endregion
}
