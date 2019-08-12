using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour {
	[SerializeField] private List<Card> handCards;

	private void Awake()
	{
		this.Initialize();
	}

	/// <summary>
	/// Called by CardManager.
	/// Card limit handling is done in CardManager.
	/// </summary>
	/// <param name="newCard"></param>
	public void AddToHand(Card newCard) {
		this.GetHandCards ().Add (newCard);
		this.PostHandCardEvent();
	}

	/// <summary>
	/// Called by CardManager.
	/// Card limit handling is done in CardManager. Inserts a card into the specified position instead of adding at the end.
	/// </summary>
	/// <param name="newCard"></param>
	/// <param name="index"></param>
	public void AddToHand(Card newCard, int index) {
		this.GetHandCards().Insert(index, newCard);
		this.PostHandCardEvent();
	}

	/// <summary>
	/// Used to remove a card from hand.
	/// </summary>
	/// <param name="usedCard"></param>
	/// <returns>the index of the card removed</returns>
	public int RemoveFromHand(Card usedCard) {
		int index = GetCardIndex(usedCard);
		this.GetHandCards().Remove(usedCard);
		//TODO: put to graveyard
		this.PostHandCardEvent();
		return index;
    }

	/// <summary>
	/// Used to get the card's index in the handcard list.
	/// </summary>
	/// <param name="toFind"></param>
	/// <returns>the index of the card, returns -1 if not found.</returns>
	public int GetCardIndex(Card toFind) {
		int index = this.GetHandCards().FindIndex(0, (x) => x == toFind);
		return index;
	}

	/// <summary>
	/// Swaps newCard into Oldcard. Maintains the oldcard's original index.
	/// </summary>
	/// <param name="cardOld"></param>
	/// <param name="cardNew"></param>
	public void SwapToHand(Card cardOld, Card cardNew) {
		int index = RemoveFromHand(cardOld);
		if (cardNew != null) {
			this.AddToHand(cardNew, index);
		}
	}

    /// <summary>
    /// Called by CardManager.
    /// Used for initial draw.
    /// Ensures that the hand is empty before drawing
    /// the initial cards.
    /// Discards all cards at hand.
    /// </summary>
	public void DiscardHand() {
		if (!this.IsEmpty ()) {
			foreach (Card card in this.GetHandCards()) {
				this.RemoveFromHand (card);
			}
		}
		this.PostHandCardEvent();
	}

    /// <summary>
    /// Returns true if the size of handCards is at max.
    /// </summary>
    /// <returns></returns>
	public bool IsFull() {
		if (this.GetHandCards ().Count == GameConstants.MAX_HAND_CARDS) {
			return true;
		}
		return false;
    }

    /// <summary>
    /// Returns true if the size of handCards is 0.
    /// Also takes care of null via GetHandCards()
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty() {
		if (this.GetHandCards ().Count == 0) {
			return true;
		}
		return false;
    }

    /// <summary>
    /// Getter functions.
    /// Used as a safety measure for null checking
    /// (But ideally should not enter the null check condition).
    /// </summary>
    /// <returns></returns>
    public List<Card> GetHandCards() {
		if(this.handCards == null) {
			this.handCards = new List<Card> ();
		}
		return this.handCards;
	}

	public void Initialize() {
		this.handCards = new List<Card> ();
	}

	public void BuffHand(int amount) {
		foreach(Card c in handCards) {
			c.SetAttack(c.GetCardAttack() + amount);
			if (c.GetCardSuit() == Card.Suit.DIAMONDS) {
				c.SetHealth(c.GetCardHealth() + amount);
			}
		}
		this.PostHandCardEvent();
	}

	/// <summary>
	/// Posts a hand card update event (EventNames.UI.HAND_CARD_UPDATE).
	/// </summary>
	public void PostHandCardEvent()
	{
		Debug.Log("<color=green>POST Hand Card Update</color>");
		EventBroadcaster.Instance.PostEvent(EventNames.UI.HAND_CARD_UPDATE);
	}
}
