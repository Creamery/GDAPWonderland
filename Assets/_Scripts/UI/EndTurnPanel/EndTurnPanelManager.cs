using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnPanelManager : MonoBehaviour {

	[Header("Successful Direct Attack")]
	[SerializeField] private GameObject SDAComponents;
    [SerializeField] private DirectAttackAnimatable directAttackAnimatable;

    private static EndTurnPanelManager sharedInstance;
	public static EndTurnPanelManager Instance {
		get { return sharedInstance; }
	}

	private void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		SDAComponents.SetActive(false);
		EventBroadcaster.Instance.AddObserver(EventNames.UI.SHOW_DIRECT_ATK_SUCCESS, ShowSuccessScreen);
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.SHOW_DIRECT_ATK_SUCCESS);
	}

	public void ShowSuccessScreen() {
		if(GameConstants.ONE_SHOT_DIRECT &&
            GameMaster.Instance.GetOpposingPlayer(MainScreenManager_GameScene.Instance.GetPlayer()).GetLifeCount() > 0) {
            this.GetDirectAttackAnimatable().Show();
            SoundManager.Instance.Play(AudibleNames.Target.GET);
            SDAComponents.SetActive(true);
        }
	}
	public void ConfirmSucessScreen() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        this.GetDirectAttackAnimatable().Hide();
        SDAComponents.SetActive(false);
        GameMaster.Instance.EndTurn(true);
    }

    public DirectAttackAnimatable GetDirectAttackAnimatable() {
        if(this.directAttackAnimatable == null) {
            this.directAttackAnimatable = GetComponent<DirectAttackAnimatable>();
        }
        return this.directAttackAnimatable;
    }
}
