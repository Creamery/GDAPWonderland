using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;


public class AttackCardHand : MonoBehaviour, IPointerClickHandler {
    private Card cardReference;
    [SerializeField] private CardAttackText cardAttack;
    [SerializeField] private CardHealthText cardHealth;

    public void Set(Card card) {
        this.cardReference = card;
        this.UpdateCardValues();
        this.Show();
    }
    public void Show() {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Hides the game object.
    /// </summary>
    public void Hide() {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// CardAttackText getter.
    /// </summary>
    /// <returns></returns>
    public CardAttackText GetCardAttackText() {
        if (this.cardAttack == null) {
            this.cardAttack = GetComponentInChildren<CardAttackText>();
        }
        return this.cardAttack;
    }
    public CardHealthText GetCardHealthText() {
        if (this.cardHealth == null) {
            this.cardHealth = GetComponentInChildren<CardHealthText>();
        }
        return this.cardHealth;
    }
    public void UpdateCardValues() {
        if (this.GetCardReference() != null) {
            this.GetCardAttackText().SetTextUI(this.GetCardReference().GetCardAttack().ToString(), this.GetCardReference().GetCardSuit());
            this.GetCardHealthText().SetTextUI(this.GetCardReference().GetCardHealth().ToString(), this.GetCardReference().GetCardSuit());
        }
        else {
            this.GetCardAttackText().SetTextUI("");
            this.GetCardHealthText().SetTextUI("");
        }
    }

    /// <summary>
    /// Clears the card value by setting the card reference to null.
    /// </summary>
    public void Clear() {
        this.cardReference = null;
        UpdateCardValues();
        this.Hide();
    }

    /// <summary>
    /// Card getter.
    /// </summary>
    /// <returns></returns>
    public Card GetCardReference() {
        return this.cardReference;
    }
    
    /// <summary>
    /// Called when the user 'clicks' on the card
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("DOWN");
        Debug.Log("ref: " + cardReference);
        if(AttackManager.Instance.LoadCard(cardReference))
            CenterPanelManager_AM.Instance.SetState(AttackViewStates.FOCUS, cardReference);
    }
}
