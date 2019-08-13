using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour {
	public const int HEARTS = 0;
	public const int SPADES = 1;
	public const int DIAMONDS = 2;
	public const int CLUBS = 3;

	// Did not use Queue cause of shuffle
	[SerializeField] private List<Card> deckList;
	[SerializeField] private List<Card> graveList;

	private int totalDeckCount;
	public int TotalDeckCount {
		get {
			totalDeckCount = this.deckList.Count;
			return totalDeckCount; 
		}
	}

	private int[] cardCountBySuit;
	public int[] CardCountBySuit {
		get {
			// Reset cardcounts 
			for (int i = 0; i < cardCountBySuit.Length; i++)
				cardCountBySuit[i] = 0;

			// Recount cards by suit
			foreach(Card c in deckList) {
				switch (c.GetCardSuit()) {
					case Card.Suit.HEARTS:
						cardCountBySuit[HEARTS] += 1;
						break;
					case Card.Suit.SPADES:
						cardCountBySuit[SPADES] += 1;
						break;
					case Card.Suit.DIAMONDS:
						cardCountBySuit[DIAMONDS] += 1;
						break;
					case Card.Suit.CLUBS:
						cardCountBySuit[CLUBS] += 1;
						break;
				}
			}

			return this.cardCountBySuit;
		}
	}
	
	void Awake() {
		this.Instantiate ();

    }

    /// <summary>
    /// Draws one card and returns it.
    /// 
    /// [TODO] If deck is empty, replenish immediately or allow it to return null?
    /// (Meaning the only way to get more cards once your deck is empty is to land on Reshuffle Deck)
    /// Currently implements the former(via CheckDeck()).
    /// 
    /// </summary>
    /// <returns></returns>
	public Card DrawCard() {
		//this.CheckDeck ();
		if (this.GetDeckList().Count != 0) {
			Card drawnCard = this.GetDeckList()[0];
			this.GetDeckList().RemoveAt(0);
			return drawnCard;
		}
		else
			return null;
	}

	public Card DrawCardForDefense() {
		if (this.GetDeckList().Count != 0) {
			Card drawnCard;
			int i = 0;
			do {
				drawnCard = this.GetDeckList()[i];
				i++;
			}
			while (drawnCard.GetCardSuit() == Card.Suit.HEARTS && i < this.GetDeckList().Count);
			if (i < this.GetDeckList().Count) {
				// If deck still has cards other than heart 
				this.GetDeckList().RemoveAt(i-1);
				return drawnCard;
			}
			else
				return null;
		}
		else
			return null;
	}

    /// <summary>
    /// Shuffles the deck.
    /// </summary>
	public void ShuffleDeck() {
		// ListExtensions.cs function
		this.GetDeckList ().Shuffle ();
	}

    /// <summary>
    /// Appends grave list to deck list, clears the grave list, and shuffles the deck.
    /// </summary>
	public void ReplenishDeck() {
		// Check if this works properly [TODO]
		this.GetDeckList ().AddRange (this.GetGraveList ());
		this.GetGraveList ().Clear ();
	}

    /// <summary>
    /// Ensures the deck is never empty.
    /// Replenishes deck when there are no cards left.
    /// </summary>
	public void CheckDeck() {
		if (this.GetDeckList ().Count == 0) {
			this.ReplenishDeck ();
		}
	}


	// Called by CardManager. Adds deadCard to graveList
	public void AddToGrave(Card deadCard) {
		this.GetGraveList ().Add (deadCard);
    }


    
    /// <summary>
    /// Grave list getter.
    /// </summary>
    /// <returns></returns>
	public List<Card> GetGraveList() {
		if (this.graveList == null) {
			this.graveList = new List<Card> ();
		}
		return this.graveList;
	}

    /// <summary>
    /// Deck list getter.
    /// </summary>
    /// <returns></returns>
	public List<Card> GetDeckList() {
		if (this.deckList == null) {
			this.deckList = new List<Card> ();
		}
		return this.deckList;
	}

    /// <summary>
    /// Instantiate functions used to generate the deck.
    /// </summary>
	public void Instantiate() {
		this.deckList = new List<Card> ();
		this.graveList = new List<Card> ();
		this.cardCountBySuit = new int[4];

		this.GenerateDeck ();
		//this.PrintDeck ();
		this.ShuffleDeck ();
	}

	/// <summary>
    /// Generates the initial deck.
    /// </summary>
	public void GenerateDeck() {
		// CLUBS : [H] 3-5 | [A] 1-3 <Generally highest health>
		Card.Suit suit = Card.Suit.CLUBS;

		for (int i = 0; i < GameConstants.MAX_RANK_1; i++) {
			this.GetDeckList().Add(new Card(suit, "", 3, 1, 1));
		}

		for (int i = 0; i < GameConstants.MAX_RANK_2; i++) {
			this.GetDeckList().Add(new Card(suit, "", 4, 2, 2));
		}

		for (int i = 0; i < GameConstants.MAX_RANK_3; i++) {
			this.GetDeckList().Add(new Card(suit, "", 5, 3, 3));
		}

		// DIAMONDS : [H] 2-4 | [A] 2-4
        suit = Card.Suit.DIAMONDS;

        for (int i = 0; i < GameConstants.MAX_RANK_1; i++) {
            this.GetDeckList().Add(new Card(suit, "", 2, 2, 1));
        }
        for (int i = 0; i < GameConstants.MAX_RANK_2; i++) {
            this.GetDeckList().Add(new Card(suit, "", 3, 3, 2));
        }

        for (int i = 0; i < GameConstants.MAX_RANK_3; i++) {
            this.GetDeckList().Add(new Card(suit, "", 4, 4, 3));
        }

        // HEARTS : [H] 2-4 | [A] 1-3 <Lit. Twice on different targets>
		suit = Card.Suit.HEARTS;

		for (int i = 0; i < GameConstants.MAX_H_RANK_1; i++) {
			this.GetDeckList().Add(new Card(suit, "", 0, 0, 1));
		}
		for (int i = 0; i < GameConstants.MAX_H_RANK_2; i++) {
			this.GetDeckList().Add(new Card(suit, "", 0, 0, 2));
		}

		//for (int i = 0; i < GameConstants.MAX_RANK_3; i++) {
		//	this.GetDeckList().Add(new Card(suit, "", 4, 3, 3));
		//}

        // SPADES : [H] 1-3 | [A] 3-5 <Lit. Twice on single target<
		suit = Card.Suit.SPADES;

		for (int i = 0; i < GameConstants.MAX_RANK_1; i++) {
			this.GetDeckList().Add(new Card(suit, "", 1, 3, 1));
		}
		for (int i = 0; i < GameConstants.MAX_RANK_2; i++) {
			this.GetDeckList().Add(new Card(suit, "", 2, 4, 2));
		}

		for (int i = 0; i < GameConstants.MAX_RANK_3; i++) {
			this.GetDeckList().Add(new Card(suit, "", 3, 5, 3));
		}

    }

    /// <summary>
    /// Prints the contents of the deck.
    /// </summary>
    public void PrintDeck() {
		foreach(Card card in this.deckList) {
			card.Print ();
		}
	}
}
