using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetRuleTextManager : MonoBehaviour {
    [SerializeField] private ItemGetDrawCardObjectMarker drawCard;
    [SerializeField] private ItemGetSummonObjectMarker summon;
    [SerializeField] private ItemGetBombObjectMarker bomb;


    /// <summary>
    /// Enable the applicable text based on the passed rule. Note that
    /// the switch case will not cover all rules as some fall under
    /// Announcement and not ItemGet
    /// </summary>
    /// <param name="rule"></param>
    public void Show(Rules rule) {
        switch(rule) {
            case Rules.DRAW_CARD: this.DrawCard(); break;
            case Rules.SUMMON: this.Summon(); break;
            case Rules.BOMB: this.Bomb(); break;
        }
    }

    /// <summary>
    /// Show summon image text.
    /// </summary>
    public void Summon() {
        this.HideAllMarkers();
        this.GetSummonMarker().Show();
    }

    /// <summary>
    /// Show draw card image text.
    /// </summary>
    public void DrawCard() {
        this.HideAllMarkers();
        this.GetDrawCard().Show();
    }

    /// <summary>
    /// Show bomb image text.
    /// </summary>
    public void Bomb() {
        this.HideAllMarkers();
        this.GetBombMarker().Show();
    }

    public void HideAllMarkers() {
        foreach(ItemGetRuleTextObjectMarker marker in GetComponentsInChildren<ItemGetRuleTextObjectMarker>()) {
            marker.gameObject.SetActive(false);
        }
    }

    public ItemGetSummonObjectMarker GetSummonMarker() {
        if(this.summon == null) {
            this.summon = GetComponentInChildren<ItemGetSummonObjectMarker>();
        }
        return this.summon;
    }
    public ItemGetDrawCardObjectMarker GetDrawCard() {
        if (this.drawCard == null) {
            this.drawCard = GetComponentInChildren<ItemGetDrawCardObjectMarker>();
        }
        return this.drawCard;
    }


    public ItemGetBombObjectMarker GetBombMarker() {
        if (this.bomb == null) {
            this.bomb = GetComponentInChildren<ItemGetBombObjectMarker>();
        }
        return this.bomb;
    }
}
