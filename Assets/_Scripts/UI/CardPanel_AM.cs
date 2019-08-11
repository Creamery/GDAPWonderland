using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanel_AM : MonoBehaviour {
    /// <summary>
    /// AfI. Hand card prefab for attack mode UI.
    /// </summary>
    [SerializeField] private GameObject prefabAttackCardHand;

    /// <summary>
    /// AfI. The attack card scroll view manager for attack mode.
    /// </summary>
    [SerializeField] private AttackCardsContent attackCardsContent;

    public void Initialize() {
        this.GetAttackCardsContent().Initialize(this.prefabAttackCardHand);
    }

    public void UpdateCards(List<Card> cardList) {
        this.ClearCards();
        foreach(Card card in cardList) {
            this.GetAttackCardsContent().AddCardContent(card);
        }
    }

    public void ClearCards() {
        this.GetAttackCardsContent().ClearCards();
    }

    /// <summary>
    /// AttackCardsContent getter.
    /// </summary>
    /// <returns></returns>
    public AttackCardsContent GetAttackCardsContent() {
        if (this.attackCardsContent == null) {
            this.attackCardsContent = GetComponentInChildren<AttackCardsContent>();
        }
        General.LogNull(attackCardsContent);
        return this.attackCardsContent;
    }
}
