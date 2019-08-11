using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManagerUI : MonoBehaviour {

    [SerializeField] private Card loadedCard;
    [SerializeField] private CharacterFrame characterFrame;
    [SerializeField] private AttackButtonWeaponChanger weaponChanger;

    [SerializeField] private PlayerSensitiveObjectManager playerSensitiveManager;

    private static AttackManagerUI sharedInstance;
    public static AttackManagerUI Instance {
        get { return sharedInstance; }
    }

    void Awake() {
        sharedInstance = this;
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
        this.GetCharacterFrame().LoadCard(this.GetLoadedCard(),isMultiple);
        this.GetWeaponChanger().ChangeWeapon(card);
    }

    /// <summary>
    /// Character frame getter.
    /// </summary>
    /// <returns></returns>
    public CharacterFrame GetCharacterFrame() {
        if(this.characterFrame == null) {
            this.characterFrame = GetComponentInChildren<CharacterFrame>();
        }
        return this.characterFrame;
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
