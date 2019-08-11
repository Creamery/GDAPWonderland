using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonSpadesMarker : ObjectMarker {
    [SerializeField] private Image image;
    [SerializeField] private Image image2;
    private const string strPrefix = "Weapon Spades_";

    public void changeImage(Card card) {
        string strColor = GameMaster.Instance.GetCurPlayer().GetPlayerColor();
        this.GetImage().sprite = General.GetAttackWeaponSprite(strPrefix + strColor + " " + card.GetCardRank());
        this.GetImage2().sprite = this.GetImage().sprite;
    }

    public Image GetImage() {
        if (this.image == null) {
            this.image = this.GetComponentInChildren<Image>();
        }
        return this.image;
    }
    public Image GetImage2() {
        if (this.image2 == null) {
            this.image2 = this.GetComponentsInChildren<Image>()[1];
        }
        return this.image;
    }
}
