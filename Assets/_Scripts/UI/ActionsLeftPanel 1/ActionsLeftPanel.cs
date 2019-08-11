using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActionsLeftPanel : MonoBehaviour {

	static ActionsLeftPanel sharedInstance;
	public static ActionsLeftPanel Instance {
		get { return sharedInstance; }
	}

	[Header("Setup")]
	[SerializeField] private TextMeshProUGUI textUI;

	[Header("Config")]
	[SerializeField] private float lingerDuration = 2f;

	private Animator anim;
	private bool isHiddenComplete;
	public bool IsHiddenComplete {
		get { return isHiddenComplete; }
	}


	private void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameObject.SetActive(false);
	}
    public void DelayedShow() {
        //gameObject.SetActive(true);
        //StartCoroutine(DelayedShowRoutine());

        isHiddenComplete = false;
        gameObject.SetActive(true);
        textUI.SetText(MainScreenManager_GameScene.Instance.GetPlayer().GetMovesLeft().ToString());

        PlayerPanel.Instance.RefreshMovesLeft();
        anim.SetTrigger("DelayShow");
    }

    //public IEnumerator DelayedShowRoutine() {
    //    yield return new WaitForSeconds(0.7f);
    //    this.Show();
    //}


	#region Animator methods
	public void Show() {
		isHiddenComplete = false;
		gameObject.SetActive(true);
		textUI.SetText(MainScreenManager_GameScene.Instance.GetPlayer().GetMovesLeft().ToString());

        PlayerPanel.Instance.RefreshMovesLeft();
        anim.SetTrigger("Show");
	}
    public void PlayBellSound() {
        SoundManager.Instance.Play(AudibleNames.Target.BELL);
    }
	public void OnShowComplete() {
		StartCoroutine(ShowRoutine());
	}

	IEnumerator ShowRoutine() {

		yield return new WaitForSeconds(lingerDuration);
		Hide();
	}

	public void Hide() {
		anim.SetTrigger("Hide");
	}

	public void OnHideComplete() {
		gameObject.SetActive(false);
		isHiddenComplete = true;
	}
	#endregion


}
