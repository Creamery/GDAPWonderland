using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBombCountManager : PlayerItemCountManager {

	public void PostShowTooltip() {
		Parameters param = new Parameters();
		param.PutExtra(ConsumableTooltip.TOOLTIP_TEXT, Quotes.BOMB_TOOLTIP);
		EventBroadcaster.Instance.PostEvent(EventNames.UI.SHOW_CONSUMABLE_TOOLTIP, param);
	}

	public void PostHideTooltip() {
		EventBroadcaster.Instance.PostEvent(EventNames.UI.HIDE_CONSUMABLE_TOOLTIP);
	}
}
