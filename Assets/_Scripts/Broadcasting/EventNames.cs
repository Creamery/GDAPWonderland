using UnityEngine;
using System.Collections;

/**
 * Holder for event names
 * Created By: NeilDG
 **/ 
public class EventNames {
	public class UI {
		
        /// <summary>
        /// Event that notifies a hand card update.
        /// </summary>
		public const string HAND_CARD_UPDATE = "HAND_CARD_UPDATE";

        /// <summary>
        /// Event that passes the rule icon and text.
        /// </summary>
        public const string RULE_UPDATE = "RULE_UPDATE";

		public const string OPEN_LOCKSCREEN = "OPEN_LOCKSCREEN";
        public const string CLOSE_LOCKSCREEN = "CLOSE_LOCKSCREEN";

        public const string SHOW_END_SCREEN = "SHOW_END_SCREEN";

		public const string SHOW_CHARSELECT_ANNOUNCEMENT = "SHOW_CHARSELECT_ANNOUNCEMENT";

		public const string SHOW_DIRECT_ATK_SUCCESS = "SHOW_DIRECT_ATK_SUCCESS";

		public const string SHOW_CONSUMABLE_TOOLTIP = "SHOW_CONS_TOOLTIP";
		public const string HIDE_CONSUMABLE_TOOLTIP = "HIDE_CONS_TOOLTIP";
	}

    public class IMAGE_TARGETS {

        public const string TRACKED_HAND = "TRACKED_HAND";
    }

	public class ARENA {

		public const string REDUCE_HEALTH = "REDUCE_HEALTH";
		public const string DEFENSE_UPDATE = "DEFENSE_UPDATE";
		public const string CLOSE_BACKUPMAT = "CLOSEMAT";
	}
}







