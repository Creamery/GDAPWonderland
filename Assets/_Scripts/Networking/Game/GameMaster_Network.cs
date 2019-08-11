using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster_Network: NetworkBehaviour {
	public static string TRACKED_TARGET_HAND = "TRACKED_TARGET_HAND";
	public static string PLAYER = "PLAYER";

	public bool startGame;
	[SerializeField] private MainScreenManager screenManager;

	[SerializeField] private PlayerManager player1;
	[SerializeField] private PlayerManager player2;
	private PlayerManager playerTurn;

	[SerializeField] private ArenaAnimatable arenaAnimator;
	[SerializeField] private Roulette roulette;

	// TODO: Test trigger variables. Remove.
	public bool stopPhase;

	//private bool isPlaying;
	private bool isInPhase;
	 
	private bool isReplenishMode;
	private bool isInActionPhase;
	private bool hasSpun;
	private bool hasEventSettled;
	private bool hasVerifiedPlayer;

	private static GameMaster_Network sharedInstance;
	public static GameMaster_Network Instance {
		get { return sharedInstance; }
	}

	void Awake() {
		sharedInstance = this;
		this.isReplenishMode = false;
		this.isInActionPhase = false;

		this.startGame = false;
		//this.isPlaying = false;
		this.isInPhase = false;

		this.stopPhase = false;
	}

	[ServerCallback]
	private void Start() {
		StartCoroutine(StartGame());
	}
	
	/// <summary>
	/// Starts a new game. Initialize everything here.
	/// </summary>
	/// <returns>The game.</returns>
	IEnumerator StartGame() {
		Debug.Log("<color=cyan> START: StartGame()</color>");
		//initialize defense and draw
		player1.GetCardManager().ReplenishDefense(6);
		player1.GetCardManager().DrawInitialCards();

		player2.GetCardManager().ReplenishDefense(6);
		player2.GetCardManager().DrawInitialCards();

		//this.isPlaying = true;
		SetPlayerTurn(player2);
		Debug.Log("<color=red> END: StartGame()</color>");

		UIHandCardManager.Instance.ShowHand(false);
		yield return StartCoroutine(Phase0());
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
		// Lock the Screen
		EventBroadcaster.Instance.PostEvent(EventNames.UI.OPEN_LOCKSCREEN);
		// WAIT UNTIL VERIFIED
		yield return new WaitUntil(() => this.hasVerifiedPlayer);
		Debug.Log("<color=red> END: Phase0()</color>");
		if (playerTurn.GetCardManager().GetDefenseManager().DoNeedReplenish())
			yield return StartCoroutine(ReplenishDefensePhase());
		else
			yield return StartCoroutine(Phase1());
	}

	IEnumerator ReplenishDefensePhase() {
		Debug.Log("<color=cyan> START: ReplenishDefensePhase()</color>");
		isReplenishMode = true;

		int drawCount = General.RollDice(3, 6);
		Debug.Log("Rolled " + drawCount + " for replenish defense.");
		playerTurn.GetCardManager().ReplenishDefense(drawCount);
		isReplenishMode = false;
		yield return new WaitUntil(() => !isReplenishMode);
		Debug.Log("<color=red> END: ReplenishDefensePhase()</color>");

		yield return StartCoroutine(Phase1());
	}

	/// <summary>
	/// Draw Card Phase.
	/// </summary>
	/// <returns></returns>
	IEnumerator Phase1() {
		this.StartPhase();
		Debug.Log("<color=cyan> START: Phase1()</color>");

		int drawCount = General.RollDice(new List<int> { 1, 1, 1, 2, 2, 3 });
		for (int i = 0; i < drawCount; i++) {
			playerTurn.GetCardManager().DrawCard();
		}
		this.StopPhase();
		// WAIT
		while (this.IsInPhase()) {
			yield return null;
		}

		Debug.Log("<color=red> END: Phase1()</color>");
		yield return StartCoroutine(SpinWheelPhase());
	}

	IEnumerator SpinWheelPhase() {
		hasSpun = false;
		arenaAnimator.OpenRoulette();
		yield return new WaitUntil(() => hasSpun);
		Rules rule = RuleInfo.ToRuleEnum(roulette.GetPointedTo());
		arenaAnimator.CloseRoulette();

		Parameters param = new Parameters();
		param.PutObjectExtra(RuleManager.RULE_PARAM, new RuleInfo(rule));
		EventBroadcaster.Instance.PostEvent(EventNames.UI.RULE_UPDATE, param);

		HandleRouletteEvent(rule);

		yield return StartCoroutine(EvaluateWheelPhase());
	}

	IEnumerator EvaluateWheelPhase() {
		this.hasEventSettled = false;

		// TODO: Used for events that require user input. e.g. Dice roll
		SetEventSettled(true);
		yield return new WaitUntil(() => this.hasEventSettled);

		yield return StartCoroutine(Phase2());
	}

	/// <summary>
	/// Swap and Attack.
	/// Called the Action Phase.
	/// </summary>
	/// <returns></returns>
	IEnumerator Phase2() {
		this.StartPhase();
		Debug.Log("<color=cyan> START: Phase2()</color>");
		this.isInActionPhase = true;

		// WAIT
		yield return new WaitUntil(() => !this.isInActionPhase);

		Debug.Log("<color=red> END: Phase2()</color>");
		//yield return StartCoroutine (EndGame ());
		UIHandCardManager.Instance.ShowHand(false);
		yield return new WaitUntil(() => UIHandCardManager.Instance.IsHidden);
		yield return StartCoroutine(Phase0());
	}

	IEnumerator EndGame() {
		Debug.Log("<color=cyan> START: EndGame()</color>");
		//this.isPlaying = false;
		Debug.Log("<color=red> END: EndGame()</color>");
		yield return null;
	}

	public void StartPhase() {
		this.isInPhase = true;
	}

	public void StopPhase() {
		this.isInPhase = false;
	}

	#region Getters
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
			this.screenManager = GetComponentInChildren<MainScreenManager>();
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

	public void SetInActionPhase(bool val) {
		this.isInActionPhase = val;
	}

	[Server]
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

	[Server]
	public void NextPlayer() {
		if (playerTurn == player1)
			SetPlayerTurn(player2);
		else
			SetPlayerTurn(player1);
	}
	#endregion

	#region Roulette Events
	private void HandleRouletteEvent(Rules rule) {
		switch (rule) {
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
			case Rules.BOMB:
				Bomb();
				break;
			case Rules.SUMMON:
				Summon();
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

	private void RedOnly() {
		// TODO: SHOW ANNOUNCEMENT UI
		// TODO: FORCE RESTRICTION
	}

	private void WhiteOnly() {
		// TODO: SHOW UI
		// TODO: FORCE RESTRICTION
	}

	private void ReplenishDeck() {
		playerTurn.GetCardManager().GetDeckManager().ReplenishDeck();
		Debug.Log("Replenished the current player's deck!");
		// TODO: SHOW UI
	}

	/// <summary>
	/// Has User input
	/// </summary>
	private void ReplenishDefense() {
		// Change to Dice Roll UI. Use the same method to get a random value.
		// DICE ROLL UI : Single button to roll dice
		//int roll = General.RollDice(GameConstants.MIN_REPLENISH_ROLL, GameConstants.MAX_REPLENISH_ROLL);
		// DICE ROLL UI : Show rolled value
		//Debug.Log("Rolled " + roll + " for the Defense Replenish!");
		// TODO: Replenish Missing Defense.

	}

	private void Bomb() {
		// TODO: clearing of defense

		// Called after everything has been settled
		this.SetEventSettled(true);

	}

	private void Summon() {
		// TODO: Replenish hero power
	}

	private void DrawCard() {
		Debug.Log("Card drawn for current player");
		playerTurn.GetCardManager().DrawCard();
		// TODO: Show UI(?)
	}

	private void RemoveCards() {
		int roll = General.RollDice(GameConstants.MIN_REMOVE_CARDS, GameConstants.MAX_REMOVE_CARDS);
		Debug.Log("Rolled " + roll + " for the Remove Cards!");
		// TODO: Remove Hand Cards, Oldest first.
	}

	private void DrinkMe() {
		// TODO: Reduce Defense by 1 until the start of your next turn
	}

	private void EatMe() {
		// TODO: Increase Defense by 1 until the start of your next turn
	}
	#endregion
}
