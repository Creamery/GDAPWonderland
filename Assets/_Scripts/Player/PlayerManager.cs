using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

   
	public static int playerCount = 0;
	public int playerNo;

    private int lifeCount = GameConstants.MAX_PLAYER_LIFE;
    private int movesLeft = GameConstants.DEFAULT_MOVE_COUNT;

    private int skillCount = 0;
	private bool hasUsedHeroPower;
    private int bombCount = 0;

    public bool hasBeenDirectAtk = false;

    [SerializeField] private CardManager cardManager;
	private Hero playerHero;

	[Header("For Testing")]
	[SerializeField] private bool killTrigger=false;

    void Awake() {
		playerCount += 1;
		if (playerNo == 0)
			playerNo = playerCount;
	}

	private void Start() {

        if (playerNo == 1)
            playerHero = new Hero(PlayerPreference.Instance.GetHero1());
        else
            playerHero = new Hero(PlayerPreference.Instance.GetHero2());

		if (playerHero.GetHero().Equals(Hero.Type.DEFAULT))
			playerHero = new Hero(Hero.Type.ALICE);
			
		this.movesLeft = GameConstants.DEFAULT_MOVE_COUNT;

		AcquireSkill();

	}

	private void Update() {
		if (killTrigger) {
			SetLifeCount(0);
		}
	}

	public void AcquireBomb() {
        this.SetBombCount(this.bombCount+1);
    }

    public Card GetLastDrawnCard() {
        return this.GetCardManager().GetLastDrawnCard();
    }

    public bool WasLastDrawSuccessful() {
        return this.GetCardManager().WasLastDrawSuccessful();
    }

    /// <summary>
    /// Give the player one skill point.
    /// </summary>
    /// <param name="count"></param>
    public void AcquireSkill() {
        this.SetSkillCount(this.skillCount+1);
    }
	
    public void SetBombCount(int count) {
        if (count < 0) {
            count = 0;
        }
        this.bombCount = count;
    }

	public void ReduceBombCount() {
		SetBombCount(GetBombCount() - 1);
	}

    public Hero GetPlayerHero() {
        return this.playerHero;
    }

    /// <summary>
    /// Skill count setter. Handles skill cap depending on player hero.
    /// </summary>
    /// <param name="count"></param>
    public void SetSkillCount(int count) {
        if(count < 0) {
            count = 0;
        }
        if(count > this.GetPlayerHero().GetMaxSkillCount()) {
            count = this.GetPlayerHero().GetMaxSkillCount();
        }
        this.skillCount = count;
    }

	public void ReduceSkillCount() {
		SetSkillCount(GetSkillCount() - 1);
	}

    public void SetMovesLeft(int val) {
        if (val < 0) {
            val = 0;
        }
		if (val >= GameConstants.DEFAULT_MAX_MOVES) {
			val = GameConstants.DEFAULT_MAX_MOVES;
		}

        this.movesLeft = val;
    }

    public void ConsumeMove() {
        this.SetMovesLeft(this.GetMovesLeft() - 1);
    }
    public void IncrementMove() {
        this.SetMovesLeft(this.GetMovesLeft() + 1);
    }
    /// <summary>
    /// Resets the player's remaining moves based on the default move count.
    /// </summary>
    public void RefreshMoveCount() {
		//SetMovesLeft(this.movesLeft + GameConstants.DEFAULT_MOVE_INCREMENT);
		// Do nothing.
		SetMovesLeft(GameConstants.DEFAULT_MOVE_COUNT);
    }

	public void UseHeroPower() {
		this.hasUsedHeroPower = true;
		ReduceSkillCount();
	}

	public void RefreshHeroPower() {
		this.hasUsedHeroPower = false;
	}

	public bool HasUsedHeroPower() {
		return this.hasUsedHeroPower;
	}

    /// <summary>
    /// Damage the player life by 1.
    /// </summary>
    public void Damage() {
        this.SetLifeCount(this.GetLifeCount()-1);
        Parameters parameters = new Parameters();
        parameters.PutObjectExtra(LifeObject.PLAYER_REDUCED, this);
		//EventBroadcaster.Instance.PostEvent(EventNames.ARENA.REDUCE_HEALTH);
        EventBroadcaster.Instance.PostEvent(EventNames.ARENA.REDUCE_HEALTH, parameters);
    }
    /// <summary>
    /// Damage the player life as specified by the passed parameter.
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage) {
        this.SetLifeCount(this.GetLifeCount() - damage);
    }


    /// <summary>
    /// Life count setter with constraint implementation.
    /// </summary>
    /// <param name="newLife"></param>
    public void SetLifeCount(int newLife) {
        if(newLife > GameConstants.MAX_PLAYER_LIFE) {
            newLife = GameConstants.MAX_PLAYER_LIFE;
        }
        else if(newLife <= 0) {
			// Player is dead
            newLife = 0;
			GameMaster.Instance.EndGame(this);
        }
        this.lifeCount = newLife;
		Debug.Log("<color='green'>Player No: "+playerNo+"'s new hp: "+lifeCount+"</color>");
    }

    public int GetLifeCount() {
        return this.lifeCount;
    }

    public int GetMovesLeft() {
        return this.movesLeft;
    }
    public int GetSkillCount() {
        return this.skillCount;
    }
    public int GetBombCount() {
        return this.bombCount;
    }

	public string GetDefaultName() {
		return "Player " + playerNo;
	}
    public int GetPlayerNumber() {
        return playerNo;
    }
    public string GetPlayerColor() {
        switch (playerNo) {
            case 1: return "white";
            default:
                return "red";
        }
    }
    /// <summary>
    /// Returns the player's hand cards. Called by external classes.
    /// </summary>
    /// <returns></returns>
    public List<Card> GetHandCards() {
        return this.GetCardManager().GetHandCards();
    }
	
    /// <summary>
    /// CardManager getter.
    /// </summary>
    /// <returns></returns>
	public CardManager GetCardManager() {
		if (this.cardManager == null) {
			this.cardManager = GetComponentInChildren<CardManager> ();
		}
		return this.cardManager;
	}
}
