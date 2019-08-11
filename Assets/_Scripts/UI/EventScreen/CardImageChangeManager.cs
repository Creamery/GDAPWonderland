using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class CardImageChangeManager : MonoBehaviour {
    [SerializeField] private Image cardImage;

    /// <summary>
    /// Deprecated. Used for backwards compatibility with single card burning.
    /// </summary>
    /// <param name="card"></param>
    public void ChangeImage(Card card) {
        this.GetCardImage().sprite = General.GetCardSprite(card.GetCardSuit(),
            GameMaster.Instance.GetCurPlayer().playerNo, card.GetCardRank());
    }

    public Image GetCardImage() {
        if(this.cardImage == null) {
            this.cardImage = GetComponent<Image>();
        }
        return this.cardImage;
    }
}
