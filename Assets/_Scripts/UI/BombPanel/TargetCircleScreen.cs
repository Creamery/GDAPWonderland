using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircleScreen : MonoBehaviour {

	[Header("Setup")]
	[SerializeField] private AvatarObject[] player1Circles;
	[SerializeField] private AvatarObject[] player2Circles;
	[Header("Config")]
	[SerializeField] private int rollFillerAmount = 5;
	[SerializeField] private float rollInterval = 0.1f;

	private PlayerManager player1;
	private PlayerManager player2;
	private BombPanel bp;

	// Use this for initialization
	void Start () {
		player1 = GameMaster.Instance.GetPlayer1();
		player2 = GameMaster.Instance.GetPlayer2();
		bp = GetComponentInParent<BombPanel>();
	}

	public void Show() {
		gameObject.SetActive(true);
		UpdateCircleAvatars();
	}

	public void Hide() {
		ResetShade();
		gameObject.SetActive(false);
	}

	public void UpdateCircleAvatars() {
		// Setup Player1
		Card[] cards = GameMaster.Instance.GetPlayer1().GetCardManager().GetDefenseManager().GetFrontCards();
		for (int i = 0; i < cards.Length; i++) {
			if (cards[i] == null)
				player1Circles[i].SetAvatar(-1);
			else
				player1Circles[i].SetAvatar(cards[i].GetCardSuit());
		}
		// Setup Player2
		cards = GameMaster.Instance.GetPlayer2().GetCardManager().GetDefenseManager().GetFrontCards();
		for (int i = 0; i < cards.Length; i++) {
			if (cards[i] == null)
				player2Circles[i].SetAvatar(-1);
			else
				player2Circles[i].SetAvatar(cards[i].GetCardSuit());
		}
	}

	public void ResetShade() {
		for (int i = 0; i < 3; i++) {
			player1Circles[i].SetShade(false);
		}

		for (int i = 0; i < 3; i++) {
			player2Circles[i].SetShade(false);
		}
	}

	public void RollBomb() {
		StartCoroutine(BombRollCoroutine());
	}

	IEnumerator BombRollCoroutine() {
        int killCount = 0;
		bool[] hit1 = new bool[3];
		for(int i =0; i<3; i++) {
			for(int j=0; j < rollFillerAmount; j++) {
				player1Circles[i].ToggleShade();
				yield return new WaitForSeconds(rollInterval);
			}
			hit1[i] = player1Circles[i].SetShade(General.FlipCoin(70));
            if(hit1[i]) {
                PlayTargetExplode();
                killCount++;
            }
		}
		bool[] hit2 = new bool[3];
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < rollFillerAmount; j++) {
				player2Circles[i].ToggleShade();
				yield return new WaitForSeconds(rollInterval);
			}
			hit2[i] = player2Circles[i].SetShade(General.FlipCoin(70));
            if (hit2[i]) {
                PlayTargetExplode();
                killCount++;
            }
        }
		//Bomb
		for(int i =0; i<hit1.Length; i++) {
			if (hit1[i]) {
                player1.GetCardManager().KillDefense(i);
            }
			if(hit2[i]) {
                player2.GetCardManager().KillDefense(i);
            }
		}
		
		yield return new WaitForSeconds(.8f);
        bp.ShowKillCount(killCount);
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        yield return new WaitForSeconds(BombPanelAnimatable.f_COUNT);
        bp.Cancel();
	}

    public void PlayTargetExplode() {
        SoundManager.Instance.Play(AudibleNames.Target.EXPLODE);
    }
}
