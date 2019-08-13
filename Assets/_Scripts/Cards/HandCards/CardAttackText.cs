using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardAttackText : MonoBehaviour {
    private TextMeshProUGUI textMeshUI;
    private TextMeshPro textMesh;


    readonly Color CLUBS = new Color32(185, 255, 70, 255); // (73, 239, 33, 255);
    readonly Color DIAMONDS = new Color32(255, 102, 238, 255); // (252, 51, 101, 255);
    readonly Color SPADES = new Color32(255, 172, 255, 255); // (238, 81, 255, 255);
    readonly Color HEARTS = new Color32(132, 255, 255, 255); // (66, 236, 232, 255);


    const string fontPath = "FontMaterials/";
    const string fontCLUBS = "LiberationSans SDF - ClubsOutline";
    const string fontDIAMONDS = "LiberationSans SDF - DiamondsOutline";
    const string fontSPADES = "LiberationSans SDF - SpadesOutline";
    const string fontHEARTS = "LiberationSans SDF - HeartsOutline";

    protected Material matCLUBS;
    protected Material matDIAMONDS;
    protected Material matSPADES;
    protected Material matHEARTS;

    private void Awake() {
        matCLUBS = Resources.Load<Material>(fontPath + fontCLUBS);
        matDIAMONDS = Resources.Load<Material>(fontPath + fontDIAMONDS);
        matSPADES = Resources.Load<Material>(fontPath + fontSPADES);
        matHEARTS = Resources.Load<Material>(fontPath + fontHEARTS);
    }

    public void SetText(string attackText, Card.Suit suit) {
        this.SetText(attackText);
        this.recolorText(suit);
    }

    public void SetText(string attackText) {
        this.GetTextMesh().text = attackText;
    }


    public void SetTextUI(string attackText, Card.Suit suit) {
        this.SetTextUI(attackText);
        this.recolorTextUI(suit);
    }

    public void SetTextUI(string attackText) {
        this.GetTextMeshUI().text = attackText;
    }

    public string GetText() {
        return this.GetTextMeshUI().text;
    }

    public TextMeshProUGUI GetTextMeshUI() {
        if(this.textMeshUI == null) {
            this.textMeshUI = GetComponent<TextMeshProUGUI>();
        }
        General.LogNull(this.textMeshUI);
        return this.textMeshUI;
    }

    public TextMeshPro GetTextMesh() {
        if (this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshPro>();
        }
        return this.textMesh;
    }

    public void recolorTextUI(Card.Suit suit) {
        this.GetTextMeshUI().color = DIAMONDS;
        switch (suit) {
            case Card.Suit.CLUBS:
                this.GetTextMeshUI().color = CLUBS;
                this.GetTextMeshUI().fontSharedMaterial = matCLUBS;
                break;
            case Card.Suit.SPADES:
                this.GetTextMeshUI().color = SPADES;
                this.GetTextMeshUI().fontSharedMaterial = matSPADES;
                break;
            case Card.Suit.HEARTS:
                this.GetTextMeshUI().color = HEARTS;
                this.GetTextMeshUI().fontSharedMaterial = matHEARTS;
                break;
            case Card.Suit.DIAMONDS:
                this.GetTextMeshUI().color = DIAMONDS;
                this.GetTextMeshUI().fontSharedMaterial = matDIAMONDS;
                break;
        }
    }

    public void recolorText(Card.Suit suit) {
        this.GetTextMesh().color = DIAMONDS;
        switch (suit) {
            case Card.Suit.CLUBS:
                this.GetTextMesh().color = CLUBS;
                this.GetTextMesh().fontSharedMaterial = matCLUBS;
                break;
            case Card.Suit.SPADES:
                this.GetTextMesh().color = SPADES;
                this.GetTextMesh().fontSharedMaterial = matSPADES;
                break;
            case Card.Suit.HEARTS:
                this.GetTextMesh().color = HEARTS;
                this.GetTextMesh().fontSharedMaterial = matHEARTS;
                break;
            case Card.Suit.DIAMONDS:
                this.GetTextMesh().color = DIAMONDS;
                this.GetTextMesh().fontSharedMaterial = matDIAMONDS;
                break;
        }
    }
}
