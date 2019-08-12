using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManagerUI : MonoBehaviour {

    [SerializeField] private Card loadedCard;
	// Refers to the target's character frame
    [SerializeField] private CharacterFrame enemyCharacterFrame;
	// Refers to the attacker's team character frame
    [SerializeField] private CharacterFrameLead characterFrameLead;
    [SerializeField] private CharacterFrameAssist characterFrameAssist;

    [SerializeField] private AttackButtonWeaponChanger weaponChanger;

    [SerializeField] private PlayerSensitiveObjectManager playerSensitiveManager;

    private static AttackManagerUI sharedInstance;
    public static AttackManagerUI Instance {
        get { return sharedInstance; }
    }

    void Awake() {
        sharedInstance = this;
		UnloadCard();
    }


    /// <summary>
    /// Loaded card getter.
    /// </summary>
    /// <returns></returns>
    public Card GetLoadedCard() {
        return this.loadedCard;
    }


    public PlayerSensitiveObjectManager GetPlayerSensitiveManager() {
        if (this.playerSensitiveManager == null) {
            this.playerSensitiveManager = GetComponent<PlayerSensitiveObjectManager>();
        }
        return this.playerSensitiveManager;
    }

	/// <summary>
	/// Load a card to set the image and quote of the character frame.
	/// </summary>
	/// <param name="card"></param>
	public void LoadCard(Card card, bool isMultiple = false) {
        this.GetPlayerSensitiveManager().RefreshMarkers();
        this.SetLoadedCard(card);

        // this.GetCharacterFrame().LoadCard(this.GetLoadedCard(), isMultiple); // TODO Change to lead or assist
        if(isMultiple) {
            this.GetCharacterFrameAssist().LoadCard(this.GetLoadedCard(), isMultiple); // Change to assist : TODO Cap to 2 cards
        }
        else {
            this.GetCharacterFrameLead().LoadCard(this.GetLoadedCard(), isMultiple); // Change to lead
        }

        this.GetWeaponChanger().ChangeWeapon(card);
    }

	/// <summary>
	/// Removes/Resets the UI components enabled from loading the/a card(s)
	/// </summary>
	public void UnloadCard() {
		this.GetCharacterFrameAssist().ResetCharacterFrame();
		this.GetCharacterFrameLead().ResetCharacterFrame();
	}

    /// <summary>
    /// Character frame getter for target.
    /// </summary>
    /// <returns></returns>
    public CharacterFrame GetEnemyCharacterFrame() {
        if(this.enemyCharacterFrame == null) {
            this.enemyCharacterFrame = GetComponentInChildren<CharacterFrame>();
        }
        return this.enemyCharacterFrame;
    }

    /// <summary>
    /// Character frame getter for lead.
    /// </summary>
    /// <returns></returns>
    public CharacterFrameLead GetCharacterFrameLead() {
        if (this.characterFrameLead == null) {
            this.characterFrameLead = GetComponentInChildren<CharacterFrameLead>();
        }
        return this.characterFrameLead;
    }

    /// <summary>
    /// Character frame getter for assist.
    /// </summary>
    /// <returns></returns>
    public CharacterFrameAssist GetCharacterFrameAssist() {
        if (this.characterFrameAssist == null) {
            this.characterFrameAssist = GetComponentInChildren<CharacterFrameAssist>();
        }
        return this.characterFrameAssist;
    }

    public AttackButtonWeaponChanger GetWeaponChanger() {
        if (this.weaponChanger == null) {
            this.weaponChanger = GetComponentInChildren<AttackButtonWeaponChanger>();
        }
        return this.weaponChanger;
    }


    /// <summary>
    /// Loaded card setter.
    /// </summary>
    /// <param name="card"></param>
    public void SetLoadedCard(Card card) {
        this.loadedCard = card;
    }
}
