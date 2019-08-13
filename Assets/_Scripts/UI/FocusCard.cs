using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCard : MonoBehaviour {

    [SerializeField] private CardAttackText attackText;
    [SerializeField] private CardHealthText healthText;

    public void SetCard(Card card) {
        if (card == null) {
            attackText.SetTextUI("");
            healthText.SetTextUI("");
        }
        else {
            attackText.SetTextUI(card.GetCardAttack().ToString(), card.GetCardSuit());
            healthText.SetTextUI(card.GetCardHealth().ToString(), card.GetCardSuit());
        }
    }
}
