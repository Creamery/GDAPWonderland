using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintTextObjectMarker : TextObjectMarker {
    static HintTextObjectMarker instance;

    public static HintTextObjectMarker Instance {
        get { return instance; }
    }
    private void Awake() {
        instance = this;
    }
}
