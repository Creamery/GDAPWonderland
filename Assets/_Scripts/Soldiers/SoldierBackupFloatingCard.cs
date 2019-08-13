using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBackupFloatingCard : MonoBehaviour {
    [SerializeField] MeshRenderer cardRenderer;
    protected bool isEnabled = true;

    public void Refresh() {
        if(isEnabled) {
            this.Show();
        }
        else {
            this.Hide();
        }
    }
    private void FixedUpdate() {
        this.Refresh();
    }
    public void Show() {
        this.GetCardRenderer().enabled = true;
        isEnabled = true;
    }

    public void Hide() {
        this.GetCardRenderer().enabled = false;
        isEnabled = false;
    }

    public MeshRenderer GetCardRenderer() {
        if(this.cardRenderer == null) {
            this.cardRenderer = GetComponent<MeshRenderer>();
        }
        return this.cardRenderer;
    }
}
