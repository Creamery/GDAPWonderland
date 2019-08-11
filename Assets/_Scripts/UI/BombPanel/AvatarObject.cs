using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarObject : MonoBehaviour {

	public const int NONE = -1;
	public const int CLUBS = 0;
	public const int SPADES = 1;
	public const int HEARTS = 2;
	public const int DIAMONDS = 3;

	[Header("Setup")]
	[SerializeField] private GameObject shade;
	[SerializeField] private GameObject[] avatarIcon;
	private int currentAvatarId = -1;

	private void Start() {
		shade.SetActive(false);
	}

	public void SetAvatar(int avatarId) {
		if(avatarId < 0) {
			if (currentAvatarId > -1)
				avatarIcon[currentAvatarId].SetActive(false);
			currentAvatarId = -1;
		}
		else {
			if(currentAvatarId > -1)
				avatarIcon[currentAvatarId].SetActive(false);
			avatarIcon[avatarId].SetActive(true);
			currentAvatarId = avatarId;
		}
	}

	public void SetAvatar(Card.Suit suit) {
		switch (suit) {
			case Card.Suit.CLUBS:
				SetAvatar(CLUBS);
				break;
			case Card.Suit.HEARTS:
				SetAvatar(HEARTS);
				break;
			case Card.Suit.DIAMONDS:
				SetAvatar(DIAMONDS);
				break;
			case Card.Suit.SPADES:
				SetAvatar(SPADES);
				break;
			default:
				SetAvatar(NONE);
				break;
		}
	}

	public bool SetShade(bool val) {
		shade.SetActive(val);
		return val;
	}

	public void ToggleShade() {
		shade.SetActive(!shade.activeSelf);
	}
}
