using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseManager : MonoBehaviour {
	[Header("Debugging")]
	[SerializeField] private Card[] frontCards;
	[SerializeField] private Card[] backCards;
	[SerializeField] private Card summonCard;

	void Awake() {
		this.Instantiate ();
	}

	/// <summary>
	/// Called everytime a change occured in the player's defense state.
	/// </summary>
	public void PostDefenseUpdate() {
		Debug.Log("<color='green'> POST DEFENSE UPDATE </color>");
		EventBroadcaster.Instance.PostEvent(EventNames.ARENA.DEFENSE_UPDATE);
	}

	/**
	 * Called everytime a card is killed.
	 * Checks if frontCards > backCards > summonCard are empty (in that order).
	 * Note: Consider not checking back cards since it will automatically
	 * be put upfront on death (?) [TODO]
	 * 
	 * Then subtract life (?).
	 **/
	public void OnDeath() {
	
	}

	/**
	 * Called by CardManager.
	 * Handles the back-up call when the defense is killed. Also returns the old front defense card.
	 **/
	public Card KillDefense(int soldierIndex) {
		Card oldDefense = this.GetFrontCards()[soldierIndex];
		this.ClearBackup (soldierIndex);
		PostDefenseUpdate();
		return oldDefense;
	}

	/**
	 * For everytime a card dies, call the equivalent backCard
	 * and send it to frontCards. May return null.
	 **/
	public void ClearBackup(int deathIndex) {
		// Removed for stuf
		//this.GetFrontCards()[deathIndex] = this.GetBackCards()[deathIndex];
		this.GetFrontCards()[deathIndex] = null;
		this.GetBackCards()[deathIndex] = null;
	}

	/// <summary>
	/// Set backCards[backIndex] with newBackCard then return the
	/// old backCard to be placed on hand
	/// </summary>
	/// <param name="backIndex"></param>
	/// <param name="newBackCard"></param>
	/// <returns></returns>
	public Card SwapBackCard(int backIndex, Card newBackCard) {
		Card oldBackCard = this.GetBackCards()[backIndex];
		this.GetBackCards()[backIndex] = newBackCard;
		PostDefenseUpdate();
		return oldBackCard;
	}

	/// <summary>
	/// Adds the specified amount to the hp of the player's front defense.
	/// </summary>
	/// <param name="amount"></param>
	//public void FortifyFrontDefense(int amount) {
	//	foreach(Card c in frontCards) {
	//		if(c!= null)
	//			c.SetHealth(c.GetCardHealth() + amount);
	//	}
	//	PostDefenseUpdate();
	//}

	/// <summary>
	/// Set frontCard [backIndex] with newFrontCard then return the
	/// old frontCard to be placed on hand.
	/// Used for replenishing defense while the front cards is not cleared-
	/// </summary>
	/// <param name="frontIndex"></param>
	/// <param name="newFrontCard"></param>
	/// <returns></returns>
	public Card SwapFrontCard(int frontIndex, Card newFrontCard) {
		Card oldFrontCard = this.GetFrontCards()[frontIndex];
		this.GetFrontCards()[frontIndex] = newFrontCard;
		PostDefenseUpdate();
		return oldFrontCard;
	}

	/// <summary>
	/// Used for replenishing the front defense
	/// </summary>
	/// <param name="frontIndex"></param>
	/// <param name="newFrontCard"></param>
	public void ReplenishFrontDefense(int frontIndex, Card newFrontCard, bool shouldUpdate = true) {
		this.GetFrontCards()[frontIndex] = newFrontCard;
		if(shouldUpdate)
			PostDefenseUpdate();
	}

	/// <summary>
	/// Used for replenishing the defense backup
	/// </summary>
	/// <param name="backIndex"></param>
	/// <param name="newBackCard"></param>
	public void ReplenishBackDefense(int backIndex, Card newBackCard, bool shouldUpdate = true) {
		this.GetBackCards()[backIndex] = newBackCard;
		if(shouldUpdate)
			PostDefenseUpdate();
	}

	/// <summary>
	/// Used for settling the user-placed backup cards when there is no front defense that accompanies it.
	/// </summary>
	public void SettleFrontDefense() {
		for(int i=0; i < frontCards.Length; i++) {
			if(frontCards[i] == null && backCards[i] != null) {
				frontCards[i] = backCards[i];
				backCards[i] = null;
			}
		}
		PostDefenseUpdate();
	}

	/**
	 * Getter functions.
	 * Used as a safety measure for null checking
	 * (But ideally should not enter the null check condition).
	 **/
	public Card[] GetFrontCards() {
		if (this.frontCards == null) {
			this.frontCards = new Card[3];
		}

		return this.frontCards;
	}

	public Card[] GetBackCards() {
		if (this.backCards == null) {
			this.backCards = new Card[3];
		}
		
		return this.backCards;
	}

	public bool HasFrontDefense() {
		foreach(Card c in GetFrontCards()) {
			if (c != null)
				return true;
		}
		return false;
	}

	public bool HasFrontDefense(Card exclude) {
		foreach (Card c in GetFrontCards()) {
			if (c != null && c != exclude)
				return true;
		}
		return false;
	}

	/// <summary>
	/// Returns true if this player's defense is empty. False if otherwise.
	/// </summary>
	/// <returns></returns>
	public bool DoNeedReplenish() {
		Card[] frontCards = GetFrontCards();
		Card[] backCards = GetBackCards();
		for(int i=0; i < frontCards.Length; i++) {
			if (frontCards[i] != null)
				return false;
			if (backCards[i] != null)
				return false;
		}
		return true;
	}

	void Instantiate() {
		this.frontCards = new Card[3];
		this.backCards = new Card[3];
	}
}
