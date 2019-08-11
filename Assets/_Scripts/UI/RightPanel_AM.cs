using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mainly handles the switching and switch animations of the right panel objects.
/// </summary>
public class RightPanel_AM : MonoBehaviour {
    
    [SerializeField] private FocusPanel_AM rightFocusPanel;
    [SerializeField] private CardPanel_AM rightCardPanel;

    //private Card focusedCard;

    /// <summary>
    /// This method sets the appropriate states for the right panel objects that are affected by selecting a card for attack. Use this to be called by other functions.
    /// </summary>
    public void EnableFocus(Card focusedCard) {
        //this.focusedCard = focusedCard;
        rightFocusPanel.gameObject.SetActive(true);
        rightFocusPanel.FocusPanelShow(focusedCard);
        rightFocusPanel.GetComponent<Animator>().SetBool("shouldShow", true);
    }

    /// <summary>
    /// This method sets the appropriate states for the right panel objects that are affected by cancelling the selected card for attack. Use this to be called by other functions.
    /// </summary>
    public void DisableFocus() {
        rightFocusPanel.GetComponent<Animator>().SetBool("shouldShow", false);
        //this.focusedCard = null;
    }

    /// <summary>
    /// Only used to be called by FocusPanel_AM. Called when the animation ends.
    /// </summary>
    public void FocusHidden() {
        rightFocusPanel.FocusPanelShow(null);
    }

	public CardPanel_AM GetCardPanel() {
		if (this.rightCardPanel == null) {
			this.rightCardPanel = GetComponentInChildren<CardPanel_AM>();
		}
		return this.rightCardPanel;
	}

	public FocusPanel_AM GetFocusPanel() {
		if (this.rightFocusPanel == null) {
			this.rightFocusPanel = GetComponentInChildren<FocusPanel_AM>();
		}
		return this.rightFocusPanel;
	}
}
