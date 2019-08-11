using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillUsedPanel : MonoBehaviour {

	[Header("Setup")]
	[SerializeField] private TextMeshProUGUI skillText;
	[SerializeField] private GenericTooltip skillDesc;
	[Header("Config")]
	[SerializeField] private float lingerDuration = 2f;
	private Animator anim;
	private bool isAnimComplete;
	public bool IsAnimComplete {
		get { return isAnimComplete; }
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		gameObject.SetActive(false);
	}
	
	public void Show() {
        string desc = Quotes.GetHeroSkillDesc(MainScreenManager_GameScene.Instance.GetPlayer().GetPlayerHero().GetHero());
        skillDesc.ShowText(desc);

        isAnimComplete = false;
		UIHandCardManager.Instance.ShowHand(false);
		gameObject.SetActive(true);
		anim.SetTrigger("Show");
		string skillName = Quotes.GetHeroSkill(MainScreenManager_GameScene.Instance.GetPlayer().GetPlayerHero().GetHero());
		skillText.SetText(skillName);
    }

	public void OnShowComplete() {
		StartCoroutine(ShowRoutine());
	}
    public void PlayItemGet() {
        SoundManager.Instance.Play(AudibleNames.Target.GET);
    }
	IEnumerator ShowRoutine() {
		//string desc = Quotes.GetHeroSkillDesc(MainScreenManager_GameScene.Instance.GetPlayer().GetPlayerHero().GetHero());
		//skillDesc.ShowText(desc);
		yield return new WaitForSeconds(lingerDuration);
		skillDesc.IsShown(false);
		Hide();
	}

	public void Hide() {
		anim.SetTrigger("Hide");
	}

	public void OnHideComplete() {
		UIHandCardManager.Instance.ShowHand(true);
		isAnimComplete = true;
		gameObject.SetActive(false);
	}
}
