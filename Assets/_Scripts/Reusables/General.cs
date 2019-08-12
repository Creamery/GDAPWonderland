using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Static class that handles general functions.
/// </summary>
public static class General {

    public const string DIR_HERO = "Heroes/";
    public const string DIR_HERO_PORTRAIT = DIR_HERO + "Portrait/"; // Not used
    public const string DIR_HERO_SPRITE = DIR_HERO + "Sprite/";
	//public const string DIR_HERO_IMAGE = "Images/Hero/";
	public const string DIR_CARD_SPRITE = "Cards/";
	public const string DIR_SOLDIERS = "Soldiers/";
	public const string SOLDIER_SUFFIX = " Soldier with Weapons";
    public const string DIR_HERO_NAME_IMAGE = "Heroes/Name/";
    public const string DIR_EVENT_ICON = "Events/";
    public const string DIR_ATTACK_WEAPON_ICON = "ButtonWeapons/";

	public const string DIR_MATERIALS = "Materials/";

    public const string DIR_LIFE_MATERIAL = "PlayerLife/Materials/";

    public const string DIR_LIFE_FILL_MATERIAL = DIR_LIFE_MATERIAL + "LifeBarFill";
    public const string DIR_LIFE_EMPTY_MATERIAL = DIR_LIFE_MATERIAL + "LifeBarEmpty";



    /// <summary>
    /// Variable that enables/disables printing error logs.
    /// </summary>
    private const bool logErrors = true;

    /// <summary>
    /// Variable that enables/disables printing entrance logs.
    /// </summary>
    private const bool logEntrance = true;

    /// <summary>
    /// Variable that enables/disables printing entrance logs.
    /// </summary>
    private const bool logExits = true;

    /// <summary>
    /// Returns the number of the current player.
    /// </summary>
    /// <param name="playerManager"></param>
    /// <returns></returns>
    public static int GetPlayerNo(PlayerManager playerManager) {
        return GameMaster.Instance.GetPlayer1() == playerManager ? 1 : 2;
    }


	/// <summary>
	/// Rolls dice by specifying min and max inclusively.
	/// </summary>
	/// <param name="lowBound"></param>
	/// <param name="hiBound"></param>
	/// <returns></returns>
	public static int RollDice(int lowBound, int hiBound) {
		return Random.Range(lowBound, hiBound);
	}

	/// <summary>
	/// Rolls dice using a list of possible values.
	/// Can be used to simulate a weighted dice roll.
	/// </summary>
	/// <param name="possibleValue"></param>
	/// <returns></returns>
	public static int RollDice(List<int> possibleValues) {
		return possibleValues[Random.Range(0, possibleValues.Count)];
	}

	/// <summary>
	/// Returns a true/false randomized value.
	/// </summary>
	/// <returns></returns>
	public static bool FlipCoin() {
		return Random.Range(1, 100) > 50? true : false;
	}

	/// <summary>
	/// Flips a biased coin towards True (i.e. if bias is 70, there is a 70% chance that the function returns true).
	/// Returns a true/false randomized value.
	/// </summary>
	/// <param name="bias"></param>
	/// <returns></returns>
	public static bool FlipCoin(int bias) {
		return Random.Range(1, 100) > bias ? false : true;
	}

	/// <summary>
	/// Gets sprite image from Resources folder.
	/// </summary>
	/// <param name="path"></param>
	/// <returns></returns>
	public static Sprite GetSprite(string path) {
        return Resources.Load<Sprite>(path);
    }
    
    /// <summary>
    /// Gets the hero image.
    /// </summary>
    /// <param name="hero"></param>
    /// <returns></returns>
    public static Sprite GetHeroSprite(Hero.Type hero) {
        return GetSprite(DIR_HERO_SPRITE + hero.ToString());
    }

    public static Sprite GetHeroSpriteName(Hero.Type hero) {
        return GetSprite(DIR_HERO_NAME_IMAGE + hero.ToString());
    }

    public static Sprite GetEventIconSprite(string icon) {
        return GetSprite(DIR_EVENT_ICON + icon);
    }

    public static Sprite GetAttackWeaponSprite(string icon) {
        return GetSprite(DIR_ATTACK_WEAPON_ICON + icon);
    }

    public static Sprite GetCardSprite(Card.Suit suit, int playerNo, int rank) {
		string color = "";
		string colorDir = "";
		switch (playerNo) {
			case 1:
				color = "white";
				colorDir = "White Soldiers/";
				break;
			case 2:
				color = "red";
				colorDir = "Red Soldiers/";
				break;
			default: throw new System.Exception("Invalid playerno: " + playerNo + "! Should only be either '1' or '2'."); 
		}
		return GetSprite(DIR_CARD_SPRITE + colorDir + suit + "_" + color + "_" + rank);
	}

	public static Material GetMaterial(string materialName) {
		return Resources.Load<Material>(DIR_MATERIALS+materialName);
	}

	//public static Sprite GetHeroImage(Hero.Type hero) {
	//	return GetSprite(DIR_HERO_IMAGE + hero.ToString());
	//}

	public static Material GetCardMaterial(Card.Suit suit, int playerNo, int rank) {
		string color = "";
		string colorDir = "";
		switch (playerNo) {
			case 1:
				color = "white";
				colorDir = "White Soldiers/";
				break;
			case 2:
				color = "red";
				colorDir = "Red Soldiers/";
				break;
			default: throw new System.Exception("Invalid playerno: " + playerNo + "! Should only be either '1' or '2'.");
		}

		return Resources.Load<Material>(DIR_CARD_SPRITE + colorDir + "Materials/" + suit + "_" + color + "_" + rank);
	}
   
	public static Material GetBlankCardMaterial(int playerNo) {
		string colorDir = "";
		switch (playerNo) {
			case 1:
				colorDir = "White Soldiers/";
				break;
			case 2:
				colorDir = "Red Soldiers/";
				break;
			default: throw new System.Exception("Invalid playerno: " + playerNo + "! Should only be either '1' or '2'.");
		}

		return Resources.Load<Material>(DIR_CARD_SPRITE + colorDir + "Materials/" + "Blank");
	}
    
    public static Material GetCardBackMaterial(int playerNo) {
		string color = "";
		switch (playerNo) {
			case 1:
				color = "white";
				break;
			case 2:
				color = "red";
				break;
			default: throw new System.Exception("Invalid playerno: " + playerNo + "! Should only be either '1' or '2'.");
		}
		return Resources.Load<Material>(DIR_CARD_SPRITE + "Card Back/" + "Materials/" + "CardBack_"+color);
	}

	public static Material GetCardHiddenMaterial() {
		return Resources.Load<Material>(DIR_CARD_SPRITE + "Card Back/" + "Materials/" + "Hidden");
	}

    public static Material GetLifeFillMaterial(string player) {
        return Resources.Load<Material>(DIR_LIFE_FILL_MATERIAL+"_"+player);
    }

    public static Material GetLifeEmptyMaterial() {
        return Resources.Load<Material>(DIR_LIFE_EMPTY_MATERIAL);
    }

    public static GameObject GetSoldierModelPrefab(Card.Suit suit, int number) {
		return Resources.Load<GameObject>(DIR_SOLDIERS + suit + NumberToColorText(number) + SOLDIER_SUFFIX);
	}

    public static string NumberToColorText(int number) {
        switch(number) {
            case 1: return "_White";
            case 2: return "_Red";
            default: return "_White";
        }
    }

    public static Material GetSoldierDisabledMaterial() {
        return Resources.Load<Material>(DIR_SOLDIERS + "Materials/" + "DisabledSoldierMaterial");
    }
    public static Material GetSoldierDamagedMaterial() {
        return Resources.Load<Material>(DIR_SOLDIERS + "Materials/" + "DamagedSoldierMaterial");
    }
    /// <summary>
    /// Call to print debug log to mark entrance in a function.
    /// </summary>
    /// <param name="nullObjectName"></param>
    public static void LogEntrance(string functionName) {
        if (logEntrance)
            Debug.Log("<color=green>Entered function " + functionName + "</color>");
    }
    /// <summary>
    /// Call to print debug log to mark exit in a function.
    /// </summary>
    /// <param name="nullObjectName"></param>
    public static void LogExit(string functionName) {
        if (logExits)
            Debug.Log("<color=red>Exited function " + functionName + "</color>");
    }
    public static void LogNull(Rect checkObject) {
		LogError("Rect");
	}
   
    public static void LogNull(RectTransform checkObject) {
        if (checkObject == null) {
            LogError("RectTransform");
        }
    }

    public static void LogNull(VerticalLayoutGroup checkObject) {
        if (checkObject == null) {
            LogError("VerticalLayoutGroup");
        }
    }

    public static void LogNull(TextMeshProUGUI checkObject) {
        if (checkObject == null) {
            LogError("TextMeshProUGUI");
        }
    }

    public static void LogNull(AttackCardsContent checkObject) {
        if (checkObject == null) {
            LogError("AttackCardsContent");
        }
    }

    public static void LogNull(MainScreenManager checkObject) {
        if (checkObject == null) {
            LogError("MainScreenManager");
        }
    }

    public static void LogNull(CardPanel_AM checkObject) {
        if (checkObject == null) {
            LogError("CardPanel_AM");
        }
    }

    public static void LogNull(Soldier checkObject) {
        if (checkObject == null) {
            LogError("Soldier");
        }
    }

    /// <summary>
    /// Log null error.
    /// </summary>
    /// <param name="nullObjectName"></param>
    public static void LogError(string nullObjectName) {
        if(logErrors)
            Debug.LogError(nullObjectName + " is NULL");
    }

	#region Extension methods
	/// <summary>
	/// By Unity Answers user fafase.
	/// From link: https://answers.unity.com/questions/893966/how-to-find-child-with-tag.html
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="parent"></param>
	/// <param name="tag"></param>
	/// <returns></returns>
	public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag) where T : Component {
		Transform t = parent.transform;
		foreach (Transform tr in t) {
			if (tr.tag == tag) {
				return tr.GetComponent<T>();
			}
		}
		return null;
	}
	#endregion
}
