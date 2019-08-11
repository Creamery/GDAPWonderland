using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectManager : MonoBehaviour {

	public static readonly Color PLAYER1_COLOR = Color.white;
	public static readonly Color PLAYER2_COLOR = Color.red;

	bool isPlayer1Turn;

	Hero.Type player1Hero;
	Hero.Type player2Hero;

	private ButtonHero previousBtn;

	[SerializeField] private HeroArtController hac;

	[Header("Left Panel")]
	[SerializeField] private TextMeshProUGUI heroName;
	[SerializeField] private TextMeshProUGUI description;
	[SerializeField] private TextMeshProUGUI skillName;
	[SerializeField] private TextMeshProUGUI skillDesc;
	[SerializeField] private Image skillImage;


    [SerializeField] private WeaponAliceMarker weaponAlice;
    [SerializeField] private WeaponHatterMarker weaponHatter;


    // Use this for initialization
    void Start () {
		isPlayer1Turn = true;
		PostPlayerTurn(1);
	}

	public void PostPlayerTurn(int playerNo) {
		Parameters param = new Parameters();
		param.PutExtra(CharacterSelectAnnouncement.PLAYERNO_PARAM, playerNo);
		EventBroadcaster.Instance.PostEvent(EventNames.UI.SHOW_CHARSELECT_ANNOUNCEMENT, param);
	}

	public bool IsPlayer1Turn() {
		return isPlayer1Turn;
	}

    public void hideAllWeaponIcons() {
        this.GetWeaponAlice().Hide();
        this.GetWeaponHatter().Hide();
    }



	public void ConfirmSelect() {
		if (previousBtn == null)
			return;
		if (isPlayer1Turn) {
            SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
            player1Hero = previousBtn.GetHero();
			isPlayer1Turn = false;
			previousBtn = null;
			PostPlayerTurn(2);
		}
		else {
            SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
            player2Hero = previousBtn.GetHero();
			PlayerPreference.Instance.SetHero1(player1Hero);
			PlayerPreference.Instance.SetHero2(player2Hero);
            //TODO: Connect to GameScene here
            this.GetComponent<SceneLoader>().LoadScene(Scenes.GAME_SCENE);
		}
	}	

	public void SelectHero(ButtonHero btn) {
		if(previousBtn != null) {
			if (previousBtn.GetHero() != player1Hero) {
				previousBtn.SetActive(false);
			}
			else {
				previousBtn.SetColor(PLAYER1_COLOR);
			}
		}
		btn.SetActive(true);
		previousBtn = btn;

		Hero.Type hero = btn.GetHero();
        hideAllWeaponIcons();

		switch (hero) {
			case Hero.Type.ALICE: hac.ShowHeroArt(HeroArtController.ALICE);
                this.GetWeaponAlice().Show();
                break;
			case Hero.Type.HATTER: hac.ShowHeroArt(HeroArtController.HATTER);
                this.GetWeaponHatter().Show();

                break;
		}

		heroName.text = hero.ToString();
		description.text = Quotes.GetHeroDescription(hero);
		skillName.text = Quotes.GetHeroSkill(hero);
		skillDesc.text = Quotes.GetHeroSkillDesc(hero);

	}

    public WeaponAliceMarker GetWeaponAlice() {
        if (this.weaponAlice == null) {
            this.weaponAlice = GetComponentInChildren<WeaponAliceMarker>();
        }
        return this.weaponAlice;
    }
    public WeaponHatterMarker GetWeaponHatter() {
        if (this.weaponHatter == null) {
            this.weaponHatter = GetComponentInChildren<WeaponHatterMarker>();
        }
        return this.weaponHatter;
    }

}
