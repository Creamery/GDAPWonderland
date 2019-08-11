using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardObject : MonoBehaviour {
	[SerializeField] private Card cardReference;


	[SerializeField] private TextMeshProUGUI tmAttack;
	[SerializeField] private TextMeshProUGUI tmHealth;
	[SerializeField] private TextMeshProUGUI tmSuit;

	public void SetCardReference(Card card) {
		this.cardReference = card;
		this.UpdateCard ();
	}

	public void UpdateCard() {
		this.GetTmAttack ().SetText ("" + cardReference.GetCardAttack ());
		this.GetTmHealth ().SetText ("" + cardReference.GetCardHealth ());
		this.GetTmSuit ().SetText ("" + cardReference.GetCardSuit ());
	}

	public TextMeshProUGUI GetTmAttack() {
		return this.tmAttack;
	}

	public TextMeshProUGUI GetTmHealth() {
		return this.tmHealth;
	}

	public TextMeshProUGUI GetTmSuit() {
		return this.tmSuit;
	}

    /// <summary>
    /// Attack getter.
    /// </summary>
    /// <returns></returns>
	public int GetAttack() {
		return this.cardReference.GetCardAttack ();
	}

    /// <summary>
    /// Health getter.
    /// </summary>
    /// <returns></returns>
	public int GetHealth() {
		return this.cardReference.GetCardHealth ();
	}

    /// <summary>
    /// Width getter.
    /// </summary>
    /// <returns></returns>
    public float GetWidth() {
        return gameObject.GetComponent<RectTransform>().rect.width;
    }
}
