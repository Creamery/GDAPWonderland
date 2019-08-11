using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class handles the interactions and animation event triggers for the focus panel.
/// </summary>
public class FocusPanel_AM : MonoBehaviour {

	[SerializeField] private Button cancelBtn;
    private RightPanel_AM parent;
	[SerializeField] private FocusCard fCard;

    private void Start() {
        parent = GetComponentInParent<RightPanel_AM>();
    }

    /// <summary>
    /// Invoked by the cancel button for the focus panel
    /// </summary>
    public void CancelFireBtn() {
        AttackManager.Instance.UnloadCard();
        CenterPanelManager_AM.Instance.SetState(AttackViewStates.SELECT);
    }

    /// <summary>
    /// Directly called at the end of the hide animation.
    /// </summary>
    public void FocusPanelHidden() {
        parent.FocusHidden();
    }

	public void SetEnableCancelBtn(bool value)
	{
		cancelBtn.enabled = value;
	}

	public void FocusPanelShow(Card cardToFocus){
		if (cardToFocus == null){
			this.GetFocusCard().SetCard(cardToFocus);
			gameObject.SetActive(false);
		}
		else{
			gameObject.SetActive(true);
			this.GetFocusCard().SetCard(cardToFocus);
		}
	}

	public FocusCard GetFocusCard() {
		if(this.fCard == null)
			this.fCard = GetComponentInChildren<FocusCard>();
		return this.fCard;
	}
}
