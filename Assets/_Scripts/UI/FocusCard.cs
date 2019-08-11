using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusCard : MonoBehaviour {

    [SerializeField] private CardAttackText attackText;
    [SerializeField] private CardHealthText healthText;

    public void SetCard(Card card) {
        if (card == null) {
            attackText.SetText("");
            healthText.SetText("");
        }
        else {
            attackText.SetText(card.GetCardAttack().ToString());
            healthText.SetText(card.GetCardHealth().ToString());
        }
    }
}
