using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScreen_GameScene : MainScreen {
    protected new MainScreenManager_GameScene parent;

    protected PlayerManager player;

    public virtual PlayerManager GetPlayer() {
        return this.GetParent().GetPlayer();
    }

    public new MainScreenManager_GameScene GetParent() {
        if (this.parent == null) {
            this.parent = GetComponentInParent<MainScreenManager_GameScene>();
        }
        General.LogNull(parent);
        return this.parent;
    }
}
