using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardHealthText : MonoBehaviour {
    private TextMeshProUGUI textMesh;

    public void SetText(string healthText) {
        this.GetTextMesh().text = healthText;
    }

    public string GetText() {
        return this.GetTextMesh().text;
    }
    public TextMeshProUGUI GetTextMesh() {
        if (this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshProUGUI>();
        }
        General.LogNull(this.textMesh);
        return this.textMesh;
    }
}
