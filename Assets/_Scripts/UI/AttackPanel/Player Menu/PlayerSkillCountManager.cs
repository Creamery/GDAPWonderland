using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillCountManager : PlayerItemCountManager {

    public void PostShowTooltip() {
		Parameters param = new Parameters();
		param.PutExtra(ConsumableTooltip.TOOLTIP_TEXT, MainScreenManager_GameScene.Instance.GetPlayer().GetPlayerHero().GetSkillText());
		EventBroadcaster.Instance.PostEvent(EventNames.UI.SHOW_CONSUMABLE_TOOLTIP, param);
	}

	public void PostHideTooltip() {
		EventBroadcaster.Instance.PostEvent(EventNames.UI.HIDE_CONSUMABLE_TOOLTIP);
	}
}
