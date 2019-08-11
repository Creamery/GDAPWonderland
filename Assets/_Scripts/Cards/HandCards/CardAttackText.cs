using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardAttackText : MonoBehaviour {
    private TextMeshProUGUI textMesh;

    public void SetText(string attackText) {
        this.GetTextMesh().text = attackText;
    }

    public string GetText() {
        return this.GetTextMesh().text;
    }
    public TextMeshProUGUI GetTextMesh() {
        if(this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshProUGUI>();
        }
        General.LogNull(this.textMesh);
        return this.textMesh;
    }
}
