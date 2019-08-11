using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that handles the player choices before the game starts.
/// </summary>
public class PlayerPreference {
	static PlayerPreference instance;
	public static PlayerPreference Instance {
		get {
			if (instance == null)
				instance = new PlayerPreference();
			return instance;
		}
	}

	private Hero.Type hero1;
	private Hero.Type hero2;

	public Hero.Type GetHero1() {
		return this.hero1;
	}

	public Hero.Type GetHero2() {
		return this.hero2;
	}

	public void SetHero1(Hero.Type hero) {
		hero1 = hero;
	}

	public void SetHero2(Hero.Type hero) {
		hero2 = hero;
	}


}
