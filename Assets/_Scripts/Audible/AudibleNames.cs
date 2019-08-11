using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudibleNames {
    

    /// <summary>
    /// UI buttons
    /// </summary>
    public enum Button {
        DEFAULT,
        BUTTON_YES,
        BUTTON_NO,
        DISABLED,
        DRAG,
        RELEASE,
        CANCEL,
        SWITCH,
        SPIN,
        WRONG,
        SKILL,
        THUD,
        OPEN,
        EMPTY,
        EXPLODE
    }


    /// <summary>
    /// Menu items
    /// </summary>
    public enum Menu {
        OPEN,
        CLOSE
    }

    public enum Target {
        UNLOCK,
        FOUND,
        LOST,
        SHOW,
        BELL,
        GET,
        FALL,
        EXPLODE
    }

    /// <summary>
    /// Arena soldiers
    /// </summary>
    public enum Soldier {
        SPAWN,
        DEATH
    }

    /// <summary>
    /// Fire by soldier type
    /// </summary>
    public enum Fire {
        DEFAULT,
        HEARTS,
        SPADES,
        CLUBS,
        DIAMONDS,
        START,
        BURN
    }
    
    /// <summary>
    /// Bomb
    /// </summary>
    public enum Bomb {
        DRAG,
        THROW,
        EXPLODE
    }


    /// <summary>
    /// Crosshair
    /// </summary>
    public enum Crosshair {
        HIT,
        MISS,
        AGAIN,
        LOAD
    }

    public enum Door {
		OPEN,
		CLOSE
	}
}
