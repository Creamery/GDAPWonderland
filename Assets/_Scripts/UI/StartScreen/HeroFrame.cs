using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroFrame : MonoBehaviour {

    [SerializeField] private WinnerPortrait playerPortrait;
    [SerializeField] private WinnerName playerName;

    public void SetHero(Hero.Type heroType) {
        this.GetWinnerPortrait().SetImage(General.GetHeroSprite(heroType));
        this.GetWinnerName().SetImage(General.GetHeroSpriteName(heroType));
    }

    public WinnerPortrait GetWinnerPortrait() {
        if (this.playerPortrait == null) {
            this.playerPortrait = GetComponentInChildren<WinnerPortrait>();
        }
        return this.playerPortrait;
    }
    public WinnerName GetWinnerName() {
        if (this.playerName == null) {
            this.playerName = GetComponentInChildren<WinnerName>();
        }
        return this.playerName;
    }
}
