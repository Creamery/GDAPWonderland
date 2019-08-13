using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPanel : MonoBehaviour {
    [SerializeField] private PlayerSensitiveObjectManager playerSenstiveManager;
    [SerializeField] private CardImageChangeManager cardImageChangeManager;
    [SerializeField] private DrawPanelAnimatable drawPanelAnimatable;
    [SerializeField] private CardHealthText cardHealthText;
    [SerializeField] private CardAttackText cardAttackText;




    private static DrawPanel sharedInstance;
	public static DrawPanel Instance {
		get { return sharedInstance; }
	}

	[SerializeField] private GameObject container;


	private void Awake() {
		sharedInstance = this;
	}


	public void DisplayCard(Card drawnCard) {
        // TODO: Display Card here
        this.LoadCardFront(drawnCard);
		Show();
	}

	public void Show() {
		container.SetActive(true);
        // Refresh card back
        this.GetPlayerSensitiveObjectManager().RefreshMarkers();
        // Play Show Animation
        this.GetDrawPanelAnimatable().Show();
	}

    // Attached to Confirm button
    public void ConfirmDraw() {
        SoundManager.Instance.Play(AudibleNames.Button.DEFAULT);
        PlayerManager p = MainScreenManager_GameScene.Instance.GetPlayer();
        
        if (p.GetCardManager().WasLastDrawSuccessful()) {
            this.Hide();
        }
        else {
            // Play burn routine
            StartCoroutine(BurnRoutine());
        }
    }

    public IEnumerator BurnRoutine() {
        this.GetDrawPanelAnimatable().Burn();
        yield return new WaitForSeconds(DrawPanelAnimatable.f_BURN);
        this.Hide();
    }

    public void PlayFireBurn() {
        SoundManager.Instance.Play(AudibleNames.Fire.BURN);
    }
    public void PlayItemGet() {
        SoundManager.Instance.Play(AudibleNames.Target.GET);
    }

    public void LoadCardFront(Card card) {
        this.GetCardImageChangeManager().ChangeImage(card);
        this.GetCardHealthText().SetTextUI(card.GetCardHealth().ToString());
        this.GetCardAttackText().SetTextUI(card.GetCardAttack().ToString(), card.GetCardSuit());


    }



    public void Hide() {
		// Play Hide Anim
		container.SetActive(false);
        ActionsLeftPanel.Instance.Show();
    }

    public PlayerSensitiveObjectManager GetPlayerSensitiveObjectManager() {
        if(this.playerSenstiveManager == null) {
            this.playerSenstiveManager = GetComponent<PlayerSensitiveObjectManager>();
        }
        return this.playerSenstiveManager;
    }

    public CardImageChangeManager GetCardImageChangeManager() {
        if (this.cardImageChangeManager == null) {
            this.cardImageChangeManager = GetComponentInChildren<CardImageChangeManager>();
        }
        return this.cardImageChangeManager;
    }

    public DrawPanelAnimatable GetDrawPanelAnimatable() {
        if (this.drawPanelAnimatable == null) {
            this.drawPanelAnimatable = GetComponent<DrawPanelAnimatable>();
        }
        return this.drawPanelAnimatable;
    }


    public CardHealthText GetCardHealthText() {
        if (this.cardHealthText == null) {
            this.cardHealthText = GetComponentInChildren<CardHealthText>();
        }
        return this.cardHealthText;
    }


    public CardAttackText GetCardAttackText() {
        if (this.cardAttackText == null) {
            this.cardAttackText = GetComponentInChildren<CardAttackText>();
        }
        return this.cardAttackText;
    }
}
