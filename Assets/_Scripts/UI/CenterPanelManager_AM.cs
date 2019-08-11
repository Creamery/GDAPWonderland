using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackViewStates {
    FOCUS, SELECT
}

/// <summary>
/// Handles the panel views switching. Singleton.
/// </summary>
public class CenterPanelManager_AM : MonoBehaviour {

    private static CenterPanelManager_AM sharedInstance;
    public static CenterPanelManager_AM Instance {
        get { return sharedInstance; }
    }

    [SerializeField] RightPanel_AM rightPanel;
    [SerializeField] LeftPanel_AM leftPanel;

    private void Awake() {
        sharedInstance = this;
    }

    public void SetState(AttackViewStates state, Card focusedCard=null) {
        switch (state) {
            case AttackViewStates.FOCUS:
                if (focusedCard == null)
                    throw new System.NullReferenceException();
                else {
                    rightPanel.EnableFocus(focusedCard);
                    leftPanel.EnableFocus(focusedCard);
                }
                break;
            case AttackViewStates.SELECT:
                rightPanel.DisableFocus();
                leftPanel.DisableFocus();

                break;
            default: throw new System.NotImplementedException();
        }
    }

	public LeftPanel_AM GetLeftPanel() {
		if (this.leftPanel == null) {
			this.leftPanel = GetComponentInChildren<LeftPanel_AM>();
		}
		return this.leftPanel;

	}

	public RightPanel_AM GetRightPanel() {
		if (this.rightPanel == null) {
			this.rightPanel = GetComponentInChildren<RightPanel_AM>();
		}
		return this.rightPanel;

	}
}
