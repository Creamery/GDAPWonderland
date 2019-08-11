using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCountObjectMarker : CounterObjectMarker {
    public const string ZERO_TEXT = "NO";

    public override void TriggerZero() {
        if (this.count == 0) {
            this.GetCounterObject().SetText(ZERO_TEXT);
        }
    }
}
