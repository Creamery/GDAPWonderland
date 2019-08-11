using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageObjectMarker : ObjectMarker {
    [SerializeField] private Image image;

    public void SetImage(Sprite newImage) {
        this.GetImage().sprite = newImage;
    }

    /// <summary>
    /// Image getter.
    /// </summary>
    /// <returns></returns>
    public Image GetImage() {
        if(this.image == null) {
            this.image = GetComponent<Image>();
        }
        return this.image;
    }
}
