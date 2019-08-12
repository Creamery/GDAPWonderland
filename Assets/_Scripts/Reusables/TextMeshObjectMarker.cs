using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextMeshObjectMarker : ObjectMarker {
    [SerializeField] protected TextMeshPro textMesh;
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
    public TextMeshPro GetTextMesh() {
        if (this.textMesh == null) {
            this.textMesh = GetComponent<TextMeshPro>();
        }
        return this.textMesh;
    }
}
