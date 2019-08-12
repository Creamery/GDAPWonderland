using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {
    public static string TRACKED_TARGET_HAND = "TRACKED_TARGET_HAND";
    public static string PLAYER = "PLAYER";

    public bool startGame;
	[SerializeField] private MainScreenManager screenManager;

    [SerializeField] private PlayerManager player1;
    [SerializeField] private PlayerManager player2;
	private PlayerManager playerTurn;

	[SerializeField] private ArenaAnimatable arenaAnimator;
	[SerializeField] private Roulette roulette;

	[Header("Debug")]
	[SerializeField] private bool giveCurPlayerSkill = false;
	[SerializeField] private bool giveCurPlayerBomb = false;

	private Rules currentRule;
    
    // TODO: Test trigger variables. Remove.
    public bool stopPhase;

	private bool isPlaying;
	private bool isInPhase;

	private bool isReplenishMode;
	private bool isInActionPhase;
	private bool hasSpun;
	private bool hasEventSettled;
	private bool hasVerifiedPlayer;
	private bool isRuleHigher;

	private Coroutine currentCoroutine;

    private static GameMaster sharedInstance;
    public static GameMaster Instance {
        get { return sharedInstance; }
    }

	public bool IsRuleHigher {
		get { return this.isRuleHigher; }
	}

	void Awake() {
        sharedInstance = this;
        this.Initialize();
	}

	private void Start() {
        StopAllCoroutines();
        this.Initialize();
		StartCoroutine(StartGame());
	}

    void Initialize() {
        this.isReplenishMode = false;
        this.isInActionPhase = false;

        this.startGame = false;
        this.isPlaying = false;
        this.isInPhase = false;

        this.stopPhase = false;
        PlayerManager.playerCount = 0;
    }

	void Update() {

		// TODO: Remove
		if (!isPlaying && this.startGame) {
			this.startGame = false;
			StartCoroutine (StartGame ());
		}

		// TODO: Remove
		if (this.stopPhase) {
			this.stopPhase = false;
			this.StopPhase ();
		}

		// TODO: Remove -- for Debugging only.
		if (giveCurPlayerBomb) {
			Debug.Log("Get Bomb");
			GetCurPlayer().AcquireBomb();
			PlayerPanel.Instance.RefreshData();
			this.giveCurPlayerBomb = false;
		}

		// TODO: Remove -- for Debugging only.
		if (giveCurPlayerSkill) {
			Debug.Log("Get Skill");
			GetCurPlayer().AcquireSkill();
			PlayerPanel.Instance.RefreshData();
			this.giveCurPlayerSkill = false;
		}
	}

	/// <summary>
	/// Starts a new game. Initialize everything here.
	/// </summary>
	/// <returns>The game.</returns>
	IEnumerator StartGame() {
		Debug.Log ("<color=cyan> START: StartGame()</color>");
		//initialize defense and draw
		player1.GetCardManager().ReplenishDefense();
		player1.GetCardManager().DrawInitialCards();

		player2.GetCardManager().ReplenishDefense();
		player2.GetCardManager().DrawInitialCards();

		this.isPlaying = true;
		SetPlayerTurn(player2);
		Debug.Log ("<color=red> END: StartGame()</color>");

		UIHandCardManager.Instance.ShowHand(false);
        this.HideAllPanels();
        EventBroadcaster.Instance.PostEvent(EventNames.UI.CLOSE_LOCKSCREEN);

        UIHandCardManager.Instance.ShowHand(false);
        PlayerPanel.Instance.Hide();
        BombPanel.Instance.Hide();
        RuleManager.Instance.Hide();
        yield return StartCoroutine(StartPanel.Instance.ShowRoutine());

		yield return StartCoroutine (Phase0());
	}

	/// <summary>
	/// Switches control to the next player.
	/// Check current state for optional phase enabling.
	/// Currently checks if replenish Defense Phase should be done.
	/// </summary>
	/// <returns></returns>
	IEnumerator Phase0() {
		this.StartPhase();
		Debug.Log("<color=cyan> START: Phase0()</color>");
		NextPlayer();
		playerTurn.RefreshHeroPower();
        SoldierPlacementManager.Instance.ShowPlayerHeart(GetOpposingPlayer(GetCurPlayer()).playerNo);

        Debug.Log("<color=cyan>Opposing player: "+GetOpposingPlayer(GetCurPlayer()).playerNo+"</color>");
		// Hide all panels
		this.HideAllPanels();
        // Lock the Screen
        EventBroadcaster.Instance.PostEvent(EventNames.UI.OPEN_LOCKSCREEN);
		
        // WAIT UNTIL VERIFIED
		yield return new WaitUntil(() => this.hasVerifiedPlayer);
  
		Debug.Log("<color=red> END: Phase0()</color>");
        if (playerTurn.hasBeenDirectAtk)
            currentCoroutine = StartCoroutine(ReplenishDefensePhase());
        else
            currentCoroutine = StartCoroutine(Phase1());
	}

	IEnumerator ReplenishDefensePhase() {
		Debug.Log("<color=cyan> START: ReplenishDefensePhase()</color>");
		isReplenishMode = true;

		//int drawCount = General.RollDice(3, 6);
		//Debug.Log("Rolled " + drawCount + " for replenish defense.");
		//playerTurn.GetCardManager().ReplenishDefense(); // Changed to replenish front only
		playerTurn.GetCardManager().ReplenishDefense(3);
		isReplenishMode = false;
		yield return new WaitUntil(() => !isReplenishMode);
		Debug.Log("<color=red> END: ReplenishDefensePhase()</color>");
        playerTurn.hasBeenDirectAtk = false;
		currentCoroutine = StartCoroutine(Phase1());
	}

	/// <summary>
	/// Draw Card Phase.
	/// </summary>
	/// <returns></returns>
	IEnumerator Phase1() {
		this.StartPhase ();
		Debug.Log ("<color=cyan> START: Phase1()</color>");

		// Randomized number of 1-3 cards drawn
		//int drawCount = General.RollDice(GameConstants.MIN_DRAWN_CARDS, GameConstants.MAX_DRAWN_CARDS);
		//for(int i = 0; i < drawCount; i++) {
		//    playerTurn.GetCardManager().DrawCard(true);
		//}
		playerTurn.GetCardManager().DrawCard(true);

        MainScreenManager_PhaseScreen.Instance.ShowDrawPhase();
        General.LogEntrance("UI MainScreenManager_PhaseScreen PHASE");
        // Play card burn then stop phase
        //this.StopPhase();
        // WAIT
        while (this.IsInPhase ()) {
			yield return null;			
		}

		playerTurn.GetCardManager().ResetCardHistory();

		Debug.Log ("<color=red> END: Phase1()</color>");
		currentCoroutine = StartCoroutine(SpinWheelPhase ());
	}


	IEnumerator SpinWheelPhase() {

        MainScreenManager_PhaseScreen.Instance.ShowRoulettePhase();

        hasSpun = false;
		arenaAnimator.OpenRoulette();
		yield return new WaitUntil(() => hasSpun);

        // Hide roulette phase panel
        MainScreenManager_PhaseScreen.Instance.Hide();

        this.currentRule = RuleInfo.ToRuleEnum(roulette.GetPointedTo());
		arenaAnimator.CloseRoulette();
        Debug.Log("CURRENT RULE: "+currentRule + "--" + roulette.GetPointedTo());
        Parameters param = new Parameters();
		param.PutObjectExtra(RuleManager.RULE_PARAM, new RuleInfo(currentRule));
		EventBroadcaster.Instance.PostEvent(EventNames.UI.RULE_UPDATE, param);

		HandleRouletteEvent(currentRule);

		currentCoroutine = StartCoroutine(EvaluateWheelPhase());
	}

	IEnumerator EvaluateWheelPhase() {
		this.hasEventSettled = false;
		
		yield return new WaitUntil(() => this.hasEventSettled);

		currentCoroutine = StartCoroutine(Phase2());
	}

	/// <summary>
	/// Swap and Attack.
	/// Called the Action Phase.
	/// </summary>
	/// <returns></returns>
	IEnumerator Phase2() {
		
		MainScreenManager_PhaseScreen.Instance.ShowAttackPhase();
		yield return new WaitForSeconds(DrawPhaseAnimatable.f_ATTACK + DrawPhaseAnimatable.f_ATTACK_SHOW);

		this.StartPhase();

		Debug.Log("<color=cyan> START: Phase2()</color>");
		this.isInActionPhase = true;

		MainScreenManager_PhaseScreen.Instance.Hide();
		PlayerPanel.Instance.Show();
		RuleManager.Instance.RuleDebut(this.currentRule);
		UIHandCardManager.Instance.ShowHand(true);

		// WAIT
		yield return new WaitUntil(() => !this.isInActionPhase);

		Debug.Log("<color=red> END: Phase2()</color>");
		UIHandCardManager.Instance.ShowHand(false);

		yield return new WaitUntil(() => UIHandCardManager.Instance.IsHidden);
		currentCoroutine = StartCoroutine(PostEndTurnPhase());
	}

	/// <summary>
	/// This phase handles the backup cards placed by the player midturn to move forward to the front lines.
	/// </summary>
	/// <returns></returns>
	IEnumerator PostEndTurnPhase() {
		EventBroadcaster.Instance.PostEvent(EventNames.ARENA.CLOSE_BACKUPMAT);
		player1.GetCardManager().GetDefenseManager().SettleFrontDefense();
		player2.GetCardManager().GetDefenseManager().SettleFrontDefense();
		ResetAttackBonus();
		yield return null;
		currentCoroutine = StartCoroutine(Phase0());
	}

	IEnumerator EndGamePhase(PlayerManager winner) {
		Debug.Log ("<color=cyan> START: EndGame()</color>");
		StopCoroutine(currentCoroutine);
		this.isPlaying = false;

		Parameters param = new Parameters();
		//param.PutExtra(EndScreenManager.PLAYERNAME_PARAM, winner.GetDefaultName());
        param.PutObjectExtra(EndScreenManager.WINNER, winner);
		EventBroadcaster.Instance.PostEvent(EventNames.UI.SHOW_END_SCREEN, param);

		Debug.Log ("<color=red> END: EndGame()</color>");
		yield return null;
	}

	public void EndGame(PlayerManager loser) {
		PlayerManager winner = loser == player1 ? player2 : player1;
		StartCoroutine(EndGamePhase(winner));
		PlayerManager.playerCount = 0;
	}

	/// <summary>
	///  End the game without winners. Called usually by quit.
	/// </summary>
	public void EndGame() {
		StopCoroutine(currentCoroutine);
		this.isPlaying = false;
		PlayerManager.playerCount = 0;
	}

	public void StartPhase() {
		this.isInPhase = true;
	}

	public void StopPhase() {
		this.isInPhase = false;
	}

	#region Getters
	public Rules GetCurrentRule() {
		return this.currentRule;
	}

	public bool IsInPhase() {
		return this.isInPhase;
	}

	public bool IsReplenishMode() {
		return this.isReplenishMode;
	}

	public bool IsInActionPhase() {
		return this.isInActionPhase;
	}

	public MainScreenManager GetScreenManager() {
		if (this.screenManager == null) {
			this.screenManager = GetComponentInChildren<MainScreenManager> ();
		}
		return this.screenManager;
	}

	public PlayerManager GetPlayer1() {
		return this.player1;
	}

	public PlayerManager GetPlayer2() {
		return this.player2;
	}

	public PlayerManager GetCurPlayer() {
		return this.playerTurn;
	}

	public PlayerManager GetOpposingPlayer(PlayerManager player) {
		if (player == player1)
			return player2;
		else
			return player1;
	}

    public bool GetHasSpun() {
        return this.hasSpun;
    }
    public bool GetHasVerified() {
        return this.hasVerifiedPlayer;
    }
    #endregion

    #region Setters
    public void SetHasSpun(bool val) {
		this.hasSpun = val;
	}
	// After UI close, call GameMaster.Instance.SetEventSettled(true).
	public void SetEventSettled(bool val) {
		this.hasEventSettled = val;
	}

	public void SetHasVerified(bool val) {
		this.hasVerifiedPlayer = val;
	}

    public void EndTurn(bool directAtkSuccess=false) {

        if (directAtkSuccess)
            GetOpposingPlayer(GetCurPlayer()).hasBeenDirectAtk = true;

        this.isInActionPhase = false;
    }

	public void SetInActionPhase(bool val) {
		this.isInActionPhase = val;
	}

	public void SetPlayerTurn(PlayerManager newTurn) {
		playerTurn = newTurn;
		// Remove this on Multi-device
		MainScreenManager_GameScene.Instance.SetPlayer(playerTurn);
		this.hasVerifiedPlayer = false;

		Parameters param = new Parameters();
		string playerName = playerTurn == player1 ? "Player 1's Turn" : "Player 2's Turn";
		param.PutObjectExtra(RuleManager.RULE_PARAM, new RuleInfo(Rules.TEXT_ONLY, playerName));
		EventBroadcaster.Instance.PostEvent(EventNames.UI.RULE_UPDATE, param);
	}

	public void NextPlayer() {
		if (playerTurn == player1)
			SetPlayerTurn(player2);
		else
			SetPlayerTurn(player1);
		GetCurPlayer().RefreshMoveCount();
	}
	#endregion

	#region Roulette Events
	private void HandleRouletteEvent(Rules rule) {
        switch (rule) {
			case Rules.HIGHER:
				this.isRuleHigher = true;
				MainScreenManager_EventScreen.Instance.ShowAnnouncement(rule);
				break;
			case Rules.LOWER:
				this.isRuleHigher = false;
				MainScreenManager_EventScreen.Instance.ShowAnnouncement(rule);
				break;

			case Rules.MOVED2:
				MoveEvent(2, "divide");
				break;
			case Rules.MOVEX2:
				MoveEvent(2, "multiply");
				break;
			case Rules.MOVEP2:
				MoveEvent(2);
				break;
			case Rules.MOVEP3:
				MoveEvent(3);
				break;
			case Rules.MOVEP4:
				MoveEvent(4);
				break;
			case Rules.MOVEP5:
				MoveEvent(5);
				break;
			case Rules.BOMB:
				Bomb();
				break;
			case Rules.SUMMON:
				Summon();
				break;
			case Rules.STRDEF:
				StrengthenDefense();
				break;
			case Rules.STRHAND:
				StrengthenHand();
				break;
			
			case Rules.WHITE_CARDS_ONLY:
                WhiteOnly();
                break;
            case Rules.RED_CARDS_ONLY:
                RedOnly();
                break;
            case Rules.REPLENISH_DECK:
                ReplenishDeck();
                break;
            case Rules.REPLENISH_DEFENSE:
                ReplenishDefense();
                break;
            case Rules.DRAW_CARD:
                DrawCard();
                break;
            case Rules.REMOVE_CARDS:
                RemoveCards();
                break;
            case Rules.DRINK_ME:
                DrinkMe();
                break;
            case Rules.EAT_ME:
                EatMe();
                break;
            default: throw new System.Exception("Invalid rule keyword: " + rule + "!");

        }
    }

	private void MoveEvent(int amount, string type = "add") {
		int curMoves = GetCurPlayer().GetMovesLeft();
		Debug.Log("old movecount: " + GetCurPlayer().GetMovesLeft());
		if (type == "add") {
			GetCurPlayer().SetMovesLeft(curMoves + amount);
			switch (amount) {
				case 2: MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVEP2); break;
				case 3: MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVEP3); break;
				case 4: MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVEP4); break;
				case 5: MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVEP5); break;
			}
		}
		else if (type == "multiply") {
			GetCurPlayer().SetMovesLeft(curMoves * amount);
			GetCurPlayer().IncrementMove();
			MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVEX2);
		}
		else if (type == "divide") {
			GetCurPlayer().SetMovesLeft(Mathf.CeilToInt(curMoves / amount));
			GetCurPlayer().IncrementMove();
			MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.MOVED2); 
		}
		else
			throw new UnityException("Move event type " + type + " is invalid!");
		Debug.Log("new movecount: " + GetCurPlayer().GetMovesLeft());
	}

	private void StrengthenDefense() {
		//this.playerTurn.GetCardManager().GetDefenseManager().FortifyFrontDefense(1);
		ArenaUpdateHandler.Instance.GetSoldierDefenseGroup(GetCurPlayer().playerNo).FortifyFrontDefense(1);
		GetCurPlayer().IncrementMove();
		MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.STRDEF);
	}

	private void StrengthenHand() {
		this.playerTurn.GetCardManager().GetHandManager().BuffHand(1);
		GetCurPlayer().IncrementMove();
		MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.STRHAND);
	}

    private void RedOnly() {
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.RED_CARDS_ONLY);
		// Restriction is handled on UIHandCardRaycaster.Update
	}

	private void WhiteOnly() {
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.WHITE_CARDS_ONLY);
        // Restriction is handled on UIHandCardRaycaster.Update
    }

	private void ReplenishDeck() {
		playerTurn.GetCardManager().GetDeckManager().ReplenishDeck();
		Debug.Log("Replenished the current player's deck!");
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.REPLENISH_DECK);
    }

    //public void EventSettledRoutine() {
    //    StartCoroutine(EventSettledAnimations());
    //}

    //public IEnumerator EventSettledAnimations() {
    //    MainScreenManager_PhaseScreen.Instance.ShowAttackPhase();
    //    yield return new WaitForSeconds(DrawPhaseAnimatable.f_ATTACK+ DrawPhaseAnimatable.f_ATTACK_SHOW);

    //    MainScreenManager_PhaseScreen.Instance.Hide();
    //    PlayerPanel.Instance.Show();
    //    RuleManager.Instance.RuleDebut(this.currentRule);
    //    UIHandCardManager.Instance.ShowHand(true);
    //}

    /// <summary>
    /// Has User input
    /// </summary>
    private void ReplenishDefense() {
		// Change to Dice Roll UI. Use the same method to get a random value.
		// DICE ROLL UI : Single button to roll dice
		int roll = General.RollDice(GameConstants.MIN_REPLENISH_ROLL, GameConstants.MAX_REPLENISH_ROLL);
		// DICE ROLL UI : Show rolled value
		Debug.Log("Rolled " + roll + " for the Defense Replenish!");
		GetCurPlayer().GetCardManager().ReplenishMissingDefense(roll);
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.REPLENISH_DEFENSE);

    }

	private void Bomb() {
        this.GetCurPlayer().AcquireBomb();
		PlayerPanel.Instance.RefreshData();
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.BOMB);

	}

	private void Summon() {
		this.GetCurPlayer().AcquireSkill();
		PlayerPanel.Instance.RefreshData();
		MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.SUMMON);
    }

	private void DrawCard() {
		Debug.Log("Card drawn for current player");
		playerTurn.GetCardManager().DrawCard(true);
		MainScreenManager_EventScreen.Instance.ShowItemGet(Rules.DRAW_CARD);

		//MainScreenManager_PhaseScreen.Instance.ShowDrawPhase();
		playerTurn.GetCardManager().ResetCardHistory();
	}

	private void RemoveCards() {
		int roll = General.RollDice(GameConstants.MIN_REMOVE_CARDS, GameConstants.MAX_REMOVE_CARDS);
		Debug.Log("Rolled " + roll + " for the Remove Cards!");
        // TODO: Remove Hand Cards, Oldest first.
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.REMOVE_CARDS);
    }

	private void DrinkMe() {
		//GetCurPlayer().GetCardManager().GetDefenseManager().FortifyFrontDefense(GameConstants.DRINK_ME_MODIFIER);
        MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.DRINK_ME);
    }

	private void EatMe() {
		//GetCurPlayer().GetCardManager().GetDefenseManager().FortifyFrontDefense(GameConstants.EAT_ME_MODIFIER);
		MainScreenManager_EventScreen.Instance.ShowAnnouncement(Rules.EAT_ME);
    }
	#endregion

	#region Hero Abilities
	public void UseAbility(PlayerManager user) {
		Hero.Type type = user.GetPlayerHero().GetHero();

		switch (type) {
			case Hero.Type.ALICE:
				Alice(user);
				break;
			case Hero.Type.QUEEN_OF_HEARTS:
				QueenOfHearts(user);
				break;
			case Hero.Type.HATTER:
				Hatter(user);
				break;
			case Hero.Type.CHESHIRE:
				Cheshire(user);
				break;
		}
		PlayerPanel.Instance.RefreshData();
	}

	public void Alice(PlayerManager user) {
		SetAttackBonus(GameConstants.SkillValues.VORPAL_SWORD_BONUS);
		user.UseHeroPower();
	}

	private void SetAttackBonus(int val) {
		CombatManager.Instance.SetAtkModifier(val);
		UIHandCardManager.Instance.EnableBonus(true);
	}

	private void ResetAttackBonus() {
		CombatManager.Instance.ResetAtkModifier();
		UIHandCardManager.Instance.EnableBonus(false);
	}

	public void QueenOfHearts(PlayerManager user) {
		// TODO: Show Announcement
		GetOpposingPlayer(user).SetSkillCount(0);
		user.UseHeroPower();
	}

	public void Hatter(PlayerManager user) {
		PlayerPanel.Instance.Hide();
		UIHandCardManager.Instance.ShowHand(false);
        //RulePanel.Instance.Hide();
        RuleManager.Instance.Hide();
        StopCoroutine(currentCoroutine);
		currentCoroutine = StartCoroutine(SpinWheelPhase());
		user.UseHeroPower();
	}

	public void Cheshire(PlayerManager user) {
		// TODO: Show Announcement
		GetOpposingPlayer(user).SetSkillCount(0);
		user.UseHeroPower();
	}
	#endregion

	#region Utility Functions
	public void HideAllPanels() {
		BackgroundRaycaster.Instance.ResetBackupMat();
        StartPanel.Instance.Hide();
        PlayerPanel.Instance.Hide();
		BombPanel.Instance.Hide();
		AttackManager.Instance.ShowPanel(false);
		UIHandCardManager.Instance.ShowHand(false);
	}

	/// <summary>
	/// Checks if the specified card is a valid card to use given the current rule's restrictions.
	/// </summary>
	/// <param name="c"></param>
	/// <returns></returns>
	public bool IsValidCard(Card c) {
		Card.Suit suit = c.GetCardSuit();
		if(currentRule == Rules.WHITE_CARDS_ONLY) {
			return suit == Card.Suit.SPADES || suit == Card.Suit.CLUBS;
		}
		else if(currentRule == Rules.RED_CARDS_ONLY) {
			return suit == Card.Suit.HEARTS || suit == Card.Suit.DIAMONDS;
		}
		else {
			return true;
		}
	}
	#endregion
}
