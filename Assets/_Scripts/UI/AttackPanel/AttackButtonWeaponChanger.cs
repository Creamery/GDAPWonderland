using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackButtonWeaponChanger : MonoBehaviour {

    [SerializeField] private AttackButtonDiamondsMarker diamondsMarker;
    [SerializeField] private AttackButtonHeartsMarker heartsMarker;
    [SerializeField] private AttackButtonClubsMarker clubsMarker;
    [SerializeField] private AttackButtonSpadesMarker spadesMarker;


    public void ChangeWeapon(Card card) {
        this.HideAll();
        switch (card.GetCardSuit()) {
            case Card.Suit.DIAMONDS:
                this.GetDiamondsMarker().changeImage(card);
                this.GetDiamondsMarker().Show();
                break;

            case Card.Suit.HEARTS:
                this.GetHeartsMarker().changeImage(card);
                this.GetHeartsMarker().Show();
                break;

            case Card.Suit.CLUBS:
                this.GetClubsMarker().changeImage(card);
                this.GetClubsMarker().Show();
                break;
             
            case Card.Suit.SPADES:
                this.GetSpadesMarker().changeImage(card);
                this.GetSpadesMarker().Show();
                break;
        }

    }

    public void HideAll() {
        this.GetDiamondsMarker().Hide();
        this.GetHeartsMarker().Hide();
        this.GetClubsMarker().Hide();
        this.GetSpadesMarker().Hide();
    }


    public AttackButtonDiamondsMarker GetDiamondsMarker() {
        if (this.diamondsMarker == null) {
            this.diamondsMarker = this.GetComponentInChildren<AttackButtonDiamondsMarker>();
        }
        return this.diamondsMarker;
    }

    public AttackButtonHeartsMarker GetHeartsMarker() {
        if (this.heartsMarker == null) {
            this.heartsMarker = this.GetComponentInChildren<AttackButtonHeartsMarker>();
        }
        return this.heartsMarker;
    }

    public AttackButtonClubsMarker GetClubsMarker() {
        if (this.clubsMarker == null) {
            this.clubsMarker = this.GetComponentInChildren<AttackButtonClubsMarker>();
        }
        return this.clubsMarker;
    }

    public AttackButtonSpadesMarker GetSpadesMarker() {
        if (this.spadesMarker == null) {
            this.spadesMarker = this.GetComponentInChildren<AttackButtonSpadesMarker>();
        }
        return this.spadesMarker;
    }
}
