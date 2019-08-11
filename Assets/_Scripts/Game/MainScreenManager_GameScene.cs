using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreenManager_GameScene : MonoBehaviour, MainScreenManager {

	private static MainScreenManager_GameScene sharedInstance;
	public static MainScreenManager_GameScene Instance
	{
		get { return sharedInstance; }
	}
	[Header("Debugging")]
    [SerializeField] PlayerManager player;
	[Header("Setup")]
	[SerializeField] private UIHandCardManager handCardPanel;
	[SerializeField] private AttackManager attackPanel;
	[Header("Hideable UI")]
	[SerializeField] private AttackManager atkPanel;
	[SerializeField] private PlayerPanelAnimatable playerPanelAnim;
	[SerializeField] private RulePanelAnimatable rpAnim;
	[SerializeField] private UIHandCardManager uhcAnim;

	private bool isUIhidden = false;
	[Header("Testing")]
	// TODO Remove
	[SerializeField] private Player curPlayer;

    [SerializeField] private SceneLoader sceneLoader;
    private Player prevPlayer;

	void Awake() {
		// TODO Remove (?) For inspector.
		this.UncheckAll();
		prevPlayer = curPlayer;
		// normal
        this.Initialize();
		sharedInstance = this;
	}

	void Update() {
		if (isUIhidden) {
			if (Input.GetMouseButtonDown(0))
				ShowUI(true);
		}

		// TODO Remove, for inspector
		if (prevPlayer != curPlayer) {
			prevPlayer = curPlayer;
			if(curPlayer == Player.PLAYER_1)
				player = GameMaster.Instance.GetPlayer1();
			else
				player = GameMaster.Instance.GetPlayer2();
			UpdateHand();
		}
	}

	/// <summary>
	/// Add observers and call the initialize functions of all MainScreen instances.
	/// </summary>
	public void Initialize() {
        this.AddObservers();
    }
    void OnDestroy() {
        this.RemoveObservers();
    }
    

    /// <summary>
    /// Adds necessary observers.
    /// </summary>
    void AddObservers() {
		// Hand image target listener
		//EventBroadcaster.Instance.AddObserver(EventNames.IMAGE_TARGETS.TRACKED_HAND, this.TrackedHand);

		// Hand card update listener
		EventBroadcaster.Instance.AddObserver(EventNames.UI.HAND_CARD_UPDATE, this.UpdateHand);
	}

    void RemoveObservers() {
        EventBroadcaster.Instance.RemoveObserver(EventNames.UI.HAND_CARD_UPDATE);
    }

    /// <summary>
    /// Function called whenever a HandTargetManager.cs is tracked.
    /// </summary>
    /// <param name="parameters"></param>
    public void TrackedHand(Parameters parameters) {
    }

    /// <summary>
    /// Updates the hand cards for all necessary UI elements.
    /// Hand card event observer.
    /// </summary>
    /// <param name="parameters"></param>
    public void UpdateHand() {
        Debug.Log("<color=green>RECEIVE Hand Card Update</color>");

		//this.GetScreenPlayerMenu().UpdateHand();
		//this.GetScreenAttackMode().UpdateHand();
		this.GetUIHandCardManager().UpdateHand(this.GetPlayer().GetHandCards());
    }
	
	// Required by interface
	public void UncheckAll() {

	}

	/// <summary>
	/// Shows the attack screen.
	/// </summary>
	public void SetShowAttackMode(bool val) {
		AttackManager.Instance.ShowPanel(val);
	}

	public void HideScreens() {
		this.handCardPanel.ShowHand(false);
	}

    public void SetPlayer(PlayerManager playerReference) {
        this.player = playerReference;
		EventBroadcaster.Instance.PostEvent(EventNames.UI.HAND_CARD_UPDATE);
    }

    // TODO: Null check and resolution.
    public PlayerManager GetPlayer() {
        return this.player;
    }

    public void LoadTitle() {
        this.GetSceneLoader().LoadScene(Scenes.TITLE_SCENE);
    }

    public void LoadCharacterSelect() {
        this.GetSceneLoader().LoadScene(Scenes.CHARACTER_SELECT);
    }

    public AttackManager GetAttackManager() {
		if(this.attackPanel == null) {
			this.attackPanel = AttackManager.Instance;
		}
		return this.attackPanel;
	}

	public UIHandCardManager GetUIHandCardManager() {
		if(this.handCardPanel == null) {
			this.handCardPanel = GetComponentInChildren<UIHandCardManager>();
		}
		return this.handCardPanel;
	}

    public SceneLoader GetSceneLoader() {
        if(this.sceneLoader == null) {
            this.sceneLoader = GetComponent<SceneLoader>();
        }
        return this.sceneLoader;
    }

	public void ShowUI(bool val) {
		if (!val) {
			atkPanel.UnloadCard();
			playerPanelAnim.Hide();
			rpAnim.Hide();
		}
		else {
			playerPanelAnim.Show();
			rpAnim.Show();
		}

		uhcAnim.ShowHand(val);
		isUIhidden = !val;
	}
}
