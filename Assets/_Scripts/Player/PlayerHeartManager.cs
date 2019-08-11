using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeartManager : MonoBehaviour {
    [SerializeField] private HeartAnimatable heartAnimatable;


    public void Show() {
        this.GetHeartAnimatable().Show();
    }
    public void Hide() {
        this.GetHeartAnimatable().Hide();
    }

    public HeartAnimatable GetHeartAnimatable() {
        if(this.heartAnimatable == null) {
            this.heartAnimatable = GetComponent<HeartAnimatable>();
        }
        return this.heartAnimatable;
    }
}
