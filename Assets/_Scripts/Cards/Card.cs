using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Should this be MonoBehaviour [TODO] 
 **/
public class Card {
	
	public enum Suit {
		CLUBS,
		DIAMONDS,
		HEARTS,
		SPADES
	}

	[SerializeField] private Suit cardSuit;
	//[SerializeField] private string cardImage;
	[SerializeField] private int cardHealth;
	private int originalHealth;
	public int OriginalHealth {
		get { return originalHealth; }
	}
	[SerializeField] private int cardAttack;
	public readonly int originalAttack;

	[SerializeField] private int cardRank; // use a Range from 1-3 (4 if summon?) [TODO]

    private int numOfAttacks;

	public Card(Suit suit, string image, int health, int attack, int rank) {
		this.cardSuit = suit;
		//this.cardImage = image;
		this.cardHealth = health;
		this.originalHealth = cardHealth;
		this.cardAttack = attack;
		this.originalAttack = cardAttack;
		this.cardRank = rank;
        if (suit == Suit.HEARTS)
            numOfAttacks = 2;
        else
            numOfAttacks = 1;
	}

	public string GetName() {
		return this.cardSuit + " " + this.cardRank;
	}

	public int GetCardHealth() {
		return this.cardHealth;
	}

	public int GetCardAttack() {
		return this.cardAttack;
	}

	public Suit GetCardSuit() {
		return this.cardSuit;
	}

	public int GetCardRank() {
		return this.cardRank;
	}

    public int ReduceNumOfAttacks(int amount=1) {
        this.numOfAttacks -= amount;
        return this.numOfAttacks;
    }

    public int GetNumOfAttacks() {
        return this.numOfAttacks;
    }

	public void SetAttack(int newAtk) {
		this.cardAttack = newAtk;
	}

	public void SetHealth(int newHP) {
		this.cardHealth = newHP;
	}

	public void Print() {
		Debug.Log (this.GetName () + " | A: " + this.GetCardAttack () + " | H: " + this.GetCardHealth ());
	}
}
