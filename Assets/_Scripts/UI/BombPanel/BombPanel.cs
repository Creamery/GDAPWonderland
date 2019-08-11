using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BombPanel : MonoBehaviour {
    [SerializeField] private BombPanelAnimatable bombAnimatable;
	[SerializeField] private BombObject bomb;
	[SerializeField] private GameObject cancelBtn;
	[SerializeField] private TargetCircleScreen targetCircleScreen;
    [SerializeField] private KillCountTextMarker killCountText;

    private bool isActive;
	private bool hasHeld;

    private static BombPanel sharedInstance;
    public static BombPanel Instance {
        get { return sharedInstance; }
    }
    private void Awake() {
        sharedInstance = this;
		isActive = false;
    }


    public void Throw() {
		Debug.Log("Thrown");
		MainScreenManager_GameScene.Instance.GetPlayer().ReduceBombCount();
        //this.PlayTargetFall();
        this.GetBombAnimatable().Thrown();
		this.targetCircleScreen.Show();
		this.targetCircleScreen.RollBomb();

		//UIHandCardManager.Instance.ShowHand(true);
		bomb.ResetPosition();
		//Hide();
		//PlayerPanel.Instance.Show();
		//RulePanel.Instance.Show();

	}
    public void PlayTargetFall() {
        SoundManager.Instance.Play(AudibleNames.Target.FALL);
    }
    public void PlayTargetExplode() {
        SoundManager.Instance.Play(AudibleNames.Button.EXPLODE);
    }
    public void Show() {
        PlayerPanel.Instance.Hide();
        RuleManager.Instance.Hide();
		ShowCancelBtn(true);
		this.GetBombAnimatable().Show();
		StartCoroutine(DragCoroutine());
    }

    /// <summary>
    /// This is for hiding the bomb panel only.
    /// Use Cancel() to enable the needed screens.
    /// </summary>
    public void Hide() {
		isActive = false;
		this.targetCircleScreen.Hide();
		this.GetBombAnimatable().Hide();
        RuleManager.Instance.Show();
    }

	public void ShowCancelBtn(bool val) {
		cancelBtn.SetActive(val);
	}

    /// <summary>
    /// Call this to hide the bomb panel and enable
    /// everything else needed.
    /// </summary>
    public void Cancel() {
		isActive = false;
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        UIHandCardManager.Instance.ShowHand(true);
		this.targetCircleScreen.Hide();
		this.GetBombAnimatable().Hide();
        PlayerPanel.Instance.Show();
        RuleManager.Instance.Show();
    }


    public BombPanelAnimatable GetBombAnimatable() {
        if(this.bombAnimatable == null) {
            this.bombAnimatable = GetComponent<BombPanelAnimatable>();
        }
        return this.bombAnimatable;
    }

    public void ShowKillCount(int count) {
        this.GetKillCountText().SetText(count + "");
        this.GetBombAnimatable().Count();
    }

	IEnumerator DragCoroutine() {
		isActive = true;

		hasHeld = false;
		while (isActive) {
			if (hasHeld) {
				Debug.Log("Hit bomb");
				bomb.Move(Input.mousePosition);
			}
			yield return null;
		}
	}

	public void SetBombHeld(bool val) {
		hasHeld = val;
		Debug.Log("held");
		if (!val) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit[] hits = Physics.RaycastAll(ray,Mathf.Infinity,Physics.IgnoreRaycastLayer);
			bool targetHit = false;
			foreach (RaycastHit hit in hits) {
				if (hit.collider.CompareTag("Logo")) {
					targetHit = true;
					break;
				}
			}

			if (targetHit) {
				bomb.Explode();
			}
			else {
				bomb.ResetPosition();
				ShowCancelBtn(true);
			}
		}
		else {
			ShowCancelBtn(false);
		}
	}

    public KillCountTextMarker GetKillCountText() {
        if(this.killCountText == null) {
            this.killCountText = GetComponentInChildren<KillCountTextMarker>();
        }
        return this.killCountText;
    }
}
