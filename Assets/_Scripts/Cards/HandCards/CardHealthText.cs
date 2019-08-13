using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHealthText : MonoBehaviour {
    private TextMeshProUGUI textMeshUI;
    private TextMeshPro textMesh;
    readonly Color HEALTH = new Color32(236, 233, 95, 255);
    readonly string fontPath = "FontMaterials/";
    readonly string fontHealth = "LiberationSans SDF - HealthOutline";


    
    public void SetTextUI(string healthText) {
        this.GetTextMeshUI().text = healthText;
        this.GetTextMeshUI().color = HEALTH;
    }

    public string GetTextUI() {
        return this.GetTextMeshUI().text;
    }
    public TextMeshProUGUI GetTextMeshUI() {
        if (this.textMeshUI == null) {
            this.textMeshUI = GetComponent<TextMeshProUGUI>();
            this.textMeshUI.fontSharedMaterial = Resources.Load<Material>(fontPath + fontHealth);
        }
        General.LogNull(this.textMeshUI);
        return this.textMeshUI;
    }




    public void SetTextMesh(string healthText) {
        this.GetTextMesh().text = healthText;
        this.GetTextMesh().color = HEALTH;
    }

    public string GetText() {
        return this.GetTextMesh().text;
    }
    public TextMeshPro GetTextMesh() {
        if (this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshPro>();
            this.textMesh.fontSharedMaterial = Resources.Load<Material>(fontPath + fontHealth);
        }
        return this.textMesh;
    }

}
