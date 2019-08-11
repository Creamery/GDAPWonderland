using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour {

	[SerializeField] private DeckManager deckManager;
	[SerializeField] private DefenseManager defenseManager;
	[SerializeField] private HandManager handManager;

    private Card lastDrawnCard;
	private List<Card> lastDrawnCards;
	private List<int> burntCardIndeces;
    private bool isDrawSuccessful;

	void Awake() {
		this.Initialize ();
	}

	#region Hand Card-related Functions
	/// <summary>
	/// Draw n number of cards as specified by GameConstants.INITIAL_HAND_CARDS
	/// </summary>
	public void DrawInitialCards() {
		for (int i = 0; i < GameConstants.INITIAL_HAND_CARDS; i++) {
			this.DrawCard ();
		}
    }

    /// <summary>
    /// Draws a card and appends it to hand.
    /// This currently handles when hand is full.
    /// 
    /// Who handles card burning?[TODO]
	/// For now, send to grave.
    /// </summary>
	public void DrawCard(bool keepHistory=false) {
		Card drawnCard = this.GetDeckManager ().DrawCard();
		if (drawnCard == null) {
			// TODO: if no more card left in deck.
			return;
		}

		if (!this.handManager.IsFull ()) {
            if(keepHistory)
                this.SetLastDrawnCard(drawnCard, true);
            this.GetHandManager ().AddToHand (drawnCard);
			Debug.Log("<color=blue>DRAWN "+drawnCard.GetName()+"</color>");
		} else {
            if (keepHistory)
                this.SetLastDrawnCard(drawnCard, false);
            this.GetDeckManager().AddToGrave(drawnCard);
			// TODO: display drawn card
			Debug.Log("<color=red>BURNED "+drawnCard.GetName()+"</color>");
        }
    }

    /// <summary>
    /// Returns the most recently recorded card drawn
    /// from the deck.
    /// </summary>
    /// <returns></returns>
    public Card GetLastDrawnCard() {
        return this.lastDrawnCard;
    }

	public List<Card> GetLastDrawnCards() {
		return this.lastDrawnCards;
	}

	public List<int> GetBurntCardIndeces() {
		return this.burntCardIndeces;
	}

    /// <summary>
    /// Returns true if the last card was not burned.
    /// </summary>
    /// <returns></returns>
    public bool WasLastDrawSuccessful() {
        return this.isDrawSuccessful;
    }

    /// <summary>
    /// Last drawn card setter. Also sets
    /// the boolean checker of whether the draw
    /// was successful or not (burned).
    /// </summary>
    private void SetLastDrawnCard(Card card, bool isSuccessful) {
        this.lastDrawnCard = card;
        this.isDrawSuccessful = isSuccessful;

		this.lastDrawnCards.Add(card);
		if (!isSuccessful) {
			this.burntCardIndeces.Add(this.lastDrawnCards.IndexOf(card));
		}
	}

    /// <summary>
    /// Returns the player's hand cards based on the hand manager.
    /// Called by PlayerManager.cs.
    /// </summary>
    /// <returns></returns>
    public List<Card> GetHandCards() {
        return this.GetHandManager().GetHandCards();
    }

	/// <summary>
	/// Places all hand cards in the graveyard.
	/// </summary>
	public void DiscardHand() {
		this.GetHandManager().DiscardHand();
	}

	/// <summary>
	/// Discards a specific hand card and places it in the graveyard
	/// </summary>
	/// <param name="handCard"></param>
	public void DiscardHandCard(Card handCard) {
		GetHandManager().RemoveFromHand(handCard);
		GetDeckManager().AddToGrave(handCard);
	}

	/// <summary>
	/// Discards a list of handcards and places it in the graveyard
	/// </summary>
	/// <param name="handCards"></param>
	public void DiscardHandCard(List<Card> handCards) {
		foreach (Card handCard in handCards) {
			GetHandManager().RemoveFromHand(handCard);
			GetDeckManager().AddToGrave(handCard);
		}
	}
	#endregion

	#region Defense-related Functions
	/// <summary>
	/// Kills one defense card and places it in the graveyard.
	/// </summary>
	/// <param name="soldierIndex"></param>
	public void KillDefense(int soldierIndex) {
		Card killedCard = this.GetDefenseManager ().KillDefense (soldierIndex);
		this.GetDeckManager ().AddToGrave (killedCard);
	}

	/// <summary>
	/// Called when the player's defense has been cleared.
	/// Implements this Replenish Defense variation:
	/// replenish: 
	///		roll 3 to 6 dice
	///		draw the next n cards from deck and place in defense sequentially
	/// </summary>
	/// <param name="roll"></param>
	public void ReplenishDefense(int roll) {
		for(int i =0; i<roll; i++) {
			Card c = this.GetDeckManager().DrawCardForDefense();
			if (c == null) {
				// TODO: if no more Cards available for defense left
			}

			if (i < 3)
				this.GetDefenseManager().ReplenishFrontDefense(i, c, false);
			else {
				this.GetDefenseManager().ReplenishBackDefense(i-3, c, false);
			}
		}
		this.GetDefenseManager().PostDefenseUpdate();
	}

	/// <summary>
	/// Replenish a player's defense completely
	/// </summary>
	public void ReplenishDefense() {
		for (int i = 0; i < 3; i++) {
			Card c = this.GetDeckManager().DrawCardForDefense();
			if (c == null) {
				// TODO: if no more Cards available for defense left
			}
			this.GetDefenseManager().ReplenishFrontDefense(i, c, false);
			//Debug.Log("<color='green'> Replenish Frontline #" + i + " with: " + c.GetCardSuit() + " Rank " + c.GetCardRank()+"</color>");
		}
		for (int i = 0; i < 3; i++) {
			Card c = this.GetDeckManager().DrawCardForDefense();
			if(c == null) {
				// TODO: if no more Cards available for defense left
			}
			this.GetDefenseManager().ReplenishBackDefense(i, c, false);
			//Debug.Log("<color='green'> Replenish Backup #" + i + " with: " + c.GetCardSuit() + " Rank " + c.GetCardRank() + "</color>");
		}
		this.GetDefenseManager().PostDefenseUpdate();
	}


	/// <summary>
	/// Replenishes the missing defense depending on the specified amount.
	/// Replenishes from the front defense, to the back defense, left to right.
	/// </summary>
	/// <param name="amount"></param>
	public void ReplenishMissingDefense(int amount) {
		DefenseManager dm = GetDefenseManager();
		DeckManager deckM = GetDeckManager();

		Card[] frontCards = dm.GetFrontCards();
		for (int i = 0; i < frontCards.Length; i++) {
			Card c = deckM.DrawCardForDefense();

			if (c == null) {
				// TODO: if no more Cards available for defense left
			}

			if (amount < 1)
				return;
			if (frontCards[i] == null)
				dm.ReplenishFrontDefense(i, c);
		}

		Card[] backCards = dm.GetBackCards();
		for (int i = 0; i < backCards.Length; i++) {
			Card c = deckM.DrawCardForDefense();

			if (c == null) {
				// TODO: if no more Cards available for defense left
			}

			if (amount < 1)
				return;
			if (backCards[i] == null)
				dm.ReplenishBackDefense(i, c);
		}
	}
	#endregion


	#region Getters
	/// <summary>
	/// DefenseManager getter.
	/// </summary>
	/// <returns></returns>
	public DefenseManager GetDefenseManager() {
		if (this.defenseManager == null) {
			this.defenseManager = GetComponentInChildren<DefenseManager> ();
		}
		return this.defenseManager;
	}

	public HandManager GetHandManager() {
		if (this.handManager == null) {
			this.handManager = GetComponentInChildren<HandManager> ();
		}
		return this.handManager;
	}

	public DeckManager GetDeckManager() {
		if (this.deckManager == null) {
			this.deckManager = GetComponentInChildren<DeckManager> ();
		}
		return this.deckManager;
	}
	#endregion

	public void ResetCardHistory() {
		this.lastDrawnCards.Clear();
		this.burntCardIndeces.Clear();
	}

	public void Initialize() {
		this.deckManager = GetComponentInChildren<DeckManager> ();
		this.defenseManager = GetComponentInChildren<DefenseManager> ();
		this.handManager = GetComponentInChildren<HandManager>();
		this.lastDrawnCards = new List<Card>();
		this.burntCardIndeces = new List<int>();
	}
}
