using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseCardParentManager : ObjectMarker {
    [SerializeField] private CardBurnParticleObjectMarker particleObjectMarker;
    [SerializeField] private MultipleCardImageChangeManager imageChangeMarker;

    public void UpdateCardValues(Card card, bool isBurned) {
        this.GetImageChangeMarker().UpdateCardValues(card);
        if(isBurned) {
            this.GetParticleObjectMarker().Show();
        }
        else {
            this.GetParticleObjectMarker().Hide();
        }
    }
    public CardBurnParticleObjectMarker GetParticleObjectMarker() {
        if(this.particleObjectMarker == null) {
            this.particleObjectMarker = GetComponentInChildren<CardBurnParticleObjectMarker>();
        }
        return this.particleObjectMarker;
    }
    public MultipleCardImageChangeManager GetImageChangeMarker() {
        if (this.imageChangeMarker == null) {
            this.imageChangeMarker = GetComponentInChildren<MultipleCardImageChangeManager>();
        }
        return this.imageChangeMarker;
    }
}
