using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnnouncementTextObjectMarker : TextObjectMarker {
    public void SetText(Rules rule) {
        switch (rule) {
            case Rules.RED_CARDS_ONLY:
                this.SetText(Quotes.ANNOUNCEMENT_RED_CARDS_ONLY);
                break;
            case Rules.WHITE_CARDS_ONLY:
                this.SetText(Quotes.ANNOUNCEMENT_WHITE_CARDS_ONLY);
                break;
			case Rules.EAT_ME:
				this.SetText(Quotes.ANNOUNCEMENT_EAT_ME);
				break;
			case Rules.DRINK_ME:
				this.SetText(Quotes.ANNOUNCEMENT_DRINK_ME);
				break;
			case Rules.DRAW_CARD:
				this.SetText(Quotes.ANNOUNCEMENT_DRAW_CARD);
				break;
			case Rules.REPLENISH_DEFENSE:
				this.SetText(Quotes.ANNOUNCEMENT_REPLENISH_DEFENSE);
				break;
			case Rules.REPLENISH_DECK:
				this.SetText(Quotes.ANNOUNCEMENT_REPLENISH_DECK);
				break;
			case Rules.BOMB:
				this.SetText(Quotes.ANNOUNCEMENT_BOMB);
				break;
			case Rules.SUMMON:
				this.SetText(Quotes.ANNOUNCEMENT_SUMMON);
				break;
			case Rules.MOVEP2:
				this.SetText(Quotes.ANNOUNCEMENT_MOVEP2);
				break;
			case Rules.MOVEP3:
				this.SetText(Quotes.ANNOUNCEMENT_MOVEP3);
				break;
			case Rules.MOVEP4:
				this.SetText(Quotes.ANNOUNCEMENT_MOVEP4);
				break;
			case Rules.MOVEP5:
				this.SetText(Quotes.ANNOUNCEMENT_MOVEP5);
				break;
			case Rules.MOVEX2:
				this.SetText(Quotes.ANNOUNCEMENT_MOVEX2);
				break;
			case Rules.MOVED2:
				this.SetText(Quotes.ANNOUNCEMENT_MOVED2);
				break;
			case Rules.STRDEF:
				this.SetText(Quotes.ANNOUNCEMENT_STRDEF);
				break;
			case Rules.STRHAND:
				this.SetText(Quotes.ANNOUNCEMENT_STRHAND);
				break;
		}
    }
}
