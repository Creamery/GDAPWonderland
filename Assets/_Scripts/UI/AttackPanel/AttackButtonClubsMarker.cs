using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButtonClubsMarker : ObjectMarker {
    [SerializeField] private Image image;
    private const string strPrefix = "Weapon Clubs_";

    public void changeImage(Card card) {
        string strColor = GameMaster.Instance.GetCurPlayer().GetPlayerColor();
        this.GetImage().sprite = General.GetAttackWeaponSprite(strPrefix + strColor + " " + card.GetCardRank());
    }

    public Image GetImage() {
        if (this.image == null) {
            this.image = this.GetComponentInChildren<Image>();
        }
        return this.image;
    }
}
