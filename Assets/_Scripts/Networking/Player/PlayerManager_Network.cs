using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager_Network : NetworkBehaviour {

	public static int playerCount = 0;
	public int playerNo;
	[SerializeField] private CardManager cardManager;


	void Awake() {
		playerNo = ++playerCount;
		this.Initialize();
	}

	void Start() {

	}

	/// <summary>
	/// Returns the player's hand cards. Called by external classes.
	/// </summary>
	/// <returns></returns>
	public List<Card> GetHandCards() {
		return this.GetCardManager().GetHandCards();
	}

	/// <summary>
	/// CardManager getter.
	/// </summary>
	/// <returns></returns>
	public CardManager GetCardManager() {
		if (this.cardManager == null) {
			this.cardManager = GetComponentInChildren<CardManager>();
		}
		return this.cardManager;
	}

	public void Initialize() { 
		
	}
}
