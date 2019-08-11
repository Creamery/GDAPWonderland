using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObjectMarker : ObjectMarker {
    [SerializeField] protected TextMeshProUGUI textMesh;
    private string currentText;

    public void SetText(string text) {
        this.currentText = text;
        this.GetTextMesh().SetText(this.currentText);
    }

    /// <summary>
    /// Current text getter.
    /// </summary>
    /// <returns></returns>
    public string GetText() {
        return this.currentText;
    }

    /// <summary>
    /// Text mesh getter.
    /// </summary>
    /// <returns></returns>
    public TextMeshProUGUI GetTextMesh() {
        if(this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshProUGUI>();
        }
        return this.textMesh;
    }
}
