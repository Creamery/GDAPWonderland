using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LockScreenPlayerText : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI playerText;

    public TextMeshProUGUI GetPlayerText() {
        if(this.playerText == null) {
            this.playerText = GetComponent<TextMeshProUGUI>();
        }
        return this.playerText;
    }

    /// <summary>
    /// Player text setter.
    /// </summary>
    /// <param name="newText"></param>
    public void SetPlayerText(string newText) {
        this.GetPlayerText().text = newText.ToUpper();
    }
}
