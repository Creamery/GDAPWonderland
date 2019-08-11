using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeObjectMarker : ObjectMarker {
    [SerializeField] private Image lifeBar;
    [SerializeField] private Color filledColor = new Color(255, 52, 87, 255);
    private Color emptyColor = Color.white;

    public void Fill() {
        this.GetLifeBar().color = this.GetFilledColor();
    }

    public void Clear() {
        this.GetLifeBar().color = emptyColor;
    }

    public Color GetFilledColor() {
        return this.filledColor;
    }

    public Image GetLifeBar() {
        if(this.lifeBar == null) {
            this.lifeBar = GetComponent<Image>();
        }
        return this.lifeBar;
    }
}
