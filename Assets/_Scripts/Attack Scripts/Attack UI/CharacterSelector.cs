using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles which character image to
/// show in the mini frame during attack mode
/// (within one color).
/// </summary>
public class CharacterSelector : MonoBehaviour {
    
    [SerializeField] private ObjectMarker hearts;
    [SerializeField] private ObjectMarker diamonds;
    [SerializeField] private ObjectMarker spades;
    [SerializeField] private ObjectMarker clubs;

    /// <summary>
    /// Hides all images by disabling their game objects.
    /// </summary>
    public void HideAll() {
        this.GetHearts().Hide();
        this.GetDiamonds().Hide();
        this.GetSpades().Hide();
        this.GetClubs().Hide();
    }

    /// <summary>
    /// Hides all images then disables the image instance specified by the passed parameter.
    /// </summary>
    /// <param name="suit"></param>
    public void EnableSuit(Card.Suit suit) {
        this.HideAll();
        switch (suit) {
            case Card.Suit.CLUBS:
                this.GetClubs().Show();
                break;
            case Card.Suit.SPADES:
                this.GetSpades().Show();
                break;
            case Card.Suit.DIAMONDS:
                this.GetDiamonds().Show();
                break;
            case Card.Suit.HEARTS:
                this.GetHearts().Show();
                break;
        }
    }

    public ObjectMarker GetHearts() {
        if(this.hearts == null) {
            this.hearts = GetComponentInChildren<Hearts>();
        }
        return this.hearts;
    }

    public ObjectMarker GetDiamonds() {
        if (this.diamonds == null) {
            this.diamonds = GetComponentInChildren<Diamonds>();
        }
        return this.diamonds;
    }

    public ObjectMarker GetSpades() {
        if (this.spades == null) {
            this.spades = GetComponentInChildren<Spades>();
        }
        return this.spades;
    }

    public ObjectMarker GetClubs() {
        if (this.clubs == null) {
            this.clubs = GetComponentInChildren<Clubs>();
        }
        return this.clubs;
    }
}
