using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsumableTooltip : MonoBehaviour {

	public const string TOOLTIP_TEXT = "toolTxt";

	[Header("Setup")]
	[SerializeField] private TextMeshProUGUI itemTxt;
	[SerializeField] private GameObject container;

	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		Hide();
		EventBroadcaster.Instance.AddObserver(EventNames.UI.SHOW_CONSUMABLE_TOOLTIP, Obs_Show);
		EventBroadcaster.Instance.AddObserver(EventNames.UI.HIDE_CONSUMABLE_TOOLTIP, Hide);
	}

	private void OnDestroy() {
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.SHOW_CONSUMABLE_TOOLTIP);
		EventBroadcaster.Instance.RemoveObserver(EventNames.UI.HIDE_CONSUMABLE_TOOLTIP);
	}

	#region Animator Methods
	public void Obs_Show(Parameters param) {
		string txt = param.GetStringExtra(TOOLTIP_TEXT, "");
		Show(txt);
	}

	public void Show() {
		container.SetActive(true);
		anim.SetBool("IsShown", true);
	}

	public void Show(string text) {
		SetText(text);
		Show();
	}

	public void Hide() {
		anim.SetBool("IsShown", false);
		container.SetActive(false);
	}
	#endregion
    
	public void SetText(string text) {
		this.itemTxt.text = text;
	}
}
