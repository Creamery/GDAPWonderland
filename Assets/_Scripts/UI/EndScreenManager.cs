using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour {

	//public const string PLAYERNAME_PARAM = "PLAYER_PARAM";
    public const string WINNER = "WINNER";

    const string messageSuffix = " Has Won The Game!";

	private static EndScreenManager sharedInstance;
	public static EndScreenManager Instance {
		get { return sharedInstance; }
	}

	//[SerializeField] private Text endScreenText;
    [SerializeField] private EndScreenAnimatable endScreenAnimatable;
    [SerializeField] private WinnerPortrait winnerPortrait;
    [SerializeField] private WinnerName winnerName;

    private void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		EventBroadcaster.Instance.AddObserver(EventNames.UI.SHOW_END_SCREEN, ShowEndScreen);
		gameObject.SetActive(false);
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.SHOW_END_SCREEN);
	}

    public void ShowEndScreen(Parameters param) {
        gameObject.SetActive(true);
        PlayerManager winner = param.GetObjectExtra(WINNER) as PlayerManager;
        this.SetWinner(winner.GetPlayerHero().GetHero());
		GetEndScreenAnimatable().Show();

    }
    public void SetWinner(Hero.Type heroType) {
        this.GetWinnerPortrait().SetImage(General.GetHeroSprite(heroType));
        this.GetWinnerName().SetImage(General.GetHeroSpriteName(heroType));
    }

	//public void ShowEndText(Parameters param) {
	//	gameObject.SetActive(true);
	//	string playerName = param.GetStringExtra(PLAYERNAME_PARAM, " error");
	//	endScreenText.text = playerName + messageSuffix;
	//}


    public EndScreenAnimatable GetEndScreenAnimatable() {
        if(this.endScreenAnimatable == null) {
            this.endScreenAnimatable = GetComponent<EndScreenAnimatable>();
        }
        return this.endScreenAnimatable;
    }

    public WinnerPortrait GetWinnerPortrait() {
        if(this.winnerPortrait == null) {
            this.winnerPortrait = GetComponent<WinnerPortrait>();
        }
        return this.winnerPortrait;
    }
    public WinnerName GetWinnerName() {
        if (this.winnerName == null) {
            this.winnerName = GetComponent<WinnerName>();
        }
        return this.winnerName;
    }
}
