using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFrameAssist : CharacterFrameBase {

    /// <summary>
    /// Load the card and change the image and text accordingly
    /// </summary>
    /// <param name="card"></param>
    public override void LoadCard(Card card, bool isMultiple) {
        base.LoadCard(card, isMultiple);

        this.GetAttackValue().SetText("" + card.GetCardAttack());
    }
}
