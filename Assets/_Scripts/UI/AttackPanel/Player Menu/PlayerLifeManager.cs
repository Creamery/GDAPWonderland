using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerLifeManager : MonoBehaviour {
    [SerializeField] private List<PlayerLifeObjectMarker> lifeBars;

    [Header("Testing (Remove)")]
    [SerializeField] private bool triggerLifeBarUpdate = false;
    [SerializeField] private int testLifeCount;

    private void Update() {
        if(this.triggerLifeBarUpdate) {
            this.triggerLifeBarUpdate = false;
            this.UpdateLife(this.testLifeCount);
        }
    }
    public void UpdateLife(int lifeCount) {
        this.ClearLifeBars();
        this.FillLifeBars(lifeCount);
    }

    public void FillLifeBars(int lifeCount) {
        lifeCount = this.ValidateValue(lifeCount);
        for(int i = 0; i < lifeCount; i++) {
            this.GetLifeBars()[i].Fill();
        }
    }

    public int ValidateValue(int count) {
        if(count < 0) {
            return 0;
        }
        else if(count > GameConstants.MAX_PLAYER_LIFE) {
            return GameConstants.MAX_PLAYER_LIFE;
        }
        return count;
    }
    public void ClearLifeBars() {
        foreach(PlayerLifeObjectMarker bar in this.GetLifeBars()) {
            bar.Clear();
        }
    }

    public List<PlayerLifeObjectMarker> GetLifeBars() {
        if(this.lifeBars == null) {
            this.lifeBars = GetComponentsInChildren<PlayerLifeObjectMarker>().ToList();
        }
        return this.lifeBars;
    }
}
