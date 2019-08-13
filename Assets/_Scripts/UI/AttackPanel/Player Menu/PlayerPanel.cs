using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {

    [SerializeField] PlayerPanelAnimatable playerPanelAnimatable;
    [SerializeField] PlayerLifeManager playerLifeManager;
    [SerializeField] PlayerMoveCountManager playerMoveCountManager;

    [SerializeField] PlayerBombCountManager playerBombCountManager;
    [SerializeField] PlayerSkillCountManager playerSkillCountManager;
	[SerializeField] SkillUsedPanel skillUsedPrompt;

    [SerializeField] HeroName heroName;
    [SerializeField] HeroImage heroImage;


    [SerializeField] MovesLeftAnimatable movesLeftAnimatable;

    [SerializeField] PlayerSensitiveObjectManager drawCardBackManager;

    private static PlayerPanel sharedInstance;
    public static PlayerPanel Instance {
        get { return sharedInstance; }
    }
    private void Awake() {
        sharedInstance = this;
    }

    // Called by external scripts
    public void Show() {
        this.GetPlayerPanelAnimatable().Show();
		this.RefreshData();
	}

    public void Hide() {
        this.GetPlayerPanelAnimatable().Hide();
    }

    /// <summary>
    /// Call whenever PlayerPanel shows or after a current player update.
    /// Refreshes the life, moves, bombs, and skills of the player.
    /// </summary>
    public void RefreshData() {
        PlayerManager currentPlayer = GameMaster.Instance.GetCurPlayer();
        this.GetPlayerLifeManager().UpdateLife(currentPlayer.GetLifeCount());
        this.GetPlayerMoveCountManager().UpdateCount(currentPlayer.GetMovesLeft());

        this.GetPlayerBombCountManager().UpdateCount(currentPlayer.GetBombCount());
        this.GetPlayerSkillCountManager().UpdateCount(currentPlayer.GetSkillCount());
		//this.GetHeroImage().SetImage(General.GetHeroImage(currentPlayer.GetPlayerHero().GetHero()));
        this.GetHeroImage().SetImage(General.GetHeroSprite(currentPlayer.GetPlayerHero().GetHero()));
        this.GetHeroName().SetText(currentPlayer.GetPlayerHero().GetHero().ToString());
        this.GetDrawCardBackManager().RefreshMarkers();
		HintTextObjectMarker.Instance.SetText(Quotes.HINT_DEFAULT);
    }
    public void RefreshMovesLeft() {
        PlayerManager currentPlayer = GameMaster.Instance.GetCurPlayer();
        this.GetPlayerMoveCountManager().UpdateCount(currentPlayer.GetMovesLeft());
    }

    public HeroName GetHeroName() {
        if(this.heroName == null) {
            this.heroName = GetComponentInChildren<HeroName>();
        }
        return this.heroName;
    }

    public HeroImage GetHeroImage() {
        if (this.heroImage == null) {
            this.heroImage = GetComponentInChildren<HeroImage>();
        }
        return this.heroImage;
    }

    public PlayerLifeManager GetPlayerLifeManager() {
        if (this.playerLifeManager == null) {
            this.playerLifeManager = GetComponentInChildren<PlayerLifeManager>();
        }
        return this.playerLifeManager;
    }

    /// <summary>
    /// PlayerMoveCountManager getter. Assumes that it is a child of the component.
    /// </summary>
    /// <returns></returns>
    public PlayerMoveCountManager GetPlayerMoveCountManager() {
        if (this.playerMoveCountManager == null) {
            this.playerMoveCountManager = GetComponentInChildren<PlayerMoveCountManager>();
        }
        return this.playerMoveCountManager;
    }

	public void UseSkill() {
		// Check if bomb count is greater
		// TODO: Change condition to zero. Made it to -1 for testing purposes
		PlayerManager curPlayer = GameMaster.Instance.GetCurPlayer();
		Debug.Log("Use Count: " + curPlayer.GetSkillCount());
		if (curPlayer.GetSkillCount() > 0 && !curPlayer.HasUsedHeroPower()) {
			// If Use of skill is successful
            SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
			skillUsedPrompt.Show();
			StartCoroutine(WaitPromptComplete(curPlayer));
		}
		else {
			// If Use of skill is unsuccessful
			SoundManager.Instance.Play(AudibleNames.Button.CANCEL);
            HintTextObjectMarker.Instance.SetText(Quotes.NO_SKILL_HINT);
			// Play disabled button effect
		}
	}

	IEnumerator WaitPromptComplete(PlayerManager curPlayer) {

		yield return new WaitUntil(() => skillUsedPrompt.IsAnimComplete);
		GameMaster.Instance.UseAbility(curPlayer);
	}

    public void UseBomb() {
		// Check if bomb count is greater
		// TODO: Change condition to zero. Made it to -1 for testing purposes

		Debug.Log("Bomb Count: " + GameMaster.Instance.GetCurPlayer().GetBombCount());
		if (GameMaster.Instance.GetCurPlayer().GetBombCount() > 0) {
            this.ShowBombPanel();
        }
        else {
            SoundManager.Instance.Play(AudibleNames.Button.CANCEL);
            HintTextObjectMarker.Instance.SetText(Quotes.NO_BOMB_HINT);
            // Play disabled button effect
        }

    }

	public void DrawBtnPressed() {
		PlayerManager p = MainScreenManager_GameScene.Instance.GetPlayer();

		if(p.GetMovesLeft() <= 0) {
			RuleManager.Instance.GetRulePanelAnimatable().Shake();
			return;
		}

		p.GetCardManager().ResetCardHistory();
		//int roll = General.RollDice(GameConstants.MIN_DRAWN_CARDS, GameConstants.MAX_DRAWN_CARDS);
		//for (int i = 0; i < roll; i++) {

		if (p.GetCardManager().DrawCard(true)) {
			//}

			p.ConsumeMove();
			// EDITED (CANDY) Moved after confirm
			//ActionsLeftPanel.Instance.Show();
			StartCoroutine(WaitForActionsLeftComplete(p));
		}
		else {
			// failed draw, do nothing.
		}
    }

    IEnumerator WaitForActionsLeftComplete(PlayerManager p) {
		//yield return new WaitUntil(() => ActionsLeftPanel.Instance.IsHiddenComplete);

		Card drawnCard = p.GetCardManager().GetLastDrawnCard();

		DrawPanel.Instance.DisplayCard(drawnCard);
		p.GetCardManager().ResetCardHistory(); // Always at the end of the function
        yield return null;
	}

    public void ShowBombPanel() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        PlayerPanel.Instance.Hide();
		UIHandCardManager.Instance.ShowHand(false);
		//RulePanel.Instance.Hide();
		RuleManager.Instance.Hide();
        BombPanel.Instance.Show();
    }

    public void ShowMenuPanel() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        //PlayerPanel.Instance.Hide();
        MenuPanel.Instance.Show();
    }


    public PlayerBombCountManager GetPlayerBombCountManager() {
        if (this.playerBombCountManager == null) {
            this.playerBombCountManager = GetComponentInChildren<PlayerBombCountManager>();
        }
        return this.playerBombCountManager;
    }

    public PlayerSkillCountManager GetPlayerSkillCountManager() {
        if (this.playerSkillCountManager == null) {
            this.playerSkillCountManager = GetComponentInChildren<PlayerSkillCountManager>();
        }
        return this.playerSkillCountManager;
    }

    public PlayerPanelAnimatable GetPlayerPanelAnimatable() {
        if(this.playerPanelAnimatable == null) {
            this.playerPanelAnimatable = GetComponent<PlayerPanelAnimatable>();
        }
        return this.playerPanelAnimatable;
    }
    public MovesLeftAnimatable GetMovesLeftAnimatable() {
        if (this.movesLeftAnimatable == null) {
            this.movesLeftAnimatable = GetComponentInChildren<MovesLeftAnimatable>();
        }
        return this.movesLeftAnimatable;
    }

    public PlayerSensitiveObjectManager GetDrawCardBackManager() {
        if(this.drawCardBackManager == null) {
            this.drawCardBackManager = GetComponentInChildren<PlayerSensitiveObjectManager>();
        }
        return this.drawCardBackManager;
    }
}
