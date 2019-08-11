using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animatable that handles the soldier parent object (including the text and ring).
/// </summary>
public class SoldierAnimatable : AnimatableClass {
    public const string scriptName = "SoldierAnimatable";

    // List of Params. String must have a corresponding Trigger parameter in animator.
    public const string SPAWN = "spawn";
    public const string DAMAGE = "damage";

    public const string DISABLE = "disable";
    public const string ENABLE = "enable";

    public const string DEATH = "death";

    public const float f_offset = 0.05f;

    // Variables to mark the length of each animation. Base this from the animation files.
    public const float f_SPAWN = 0.3f + f_offset;
    public const float f_DAMAGE = 0.3f + f_offset;
    public const float f_ENABLE = 0.3f + f_offset;
    public const float f_DISABLE = 0.3f + f_offset;

    [SerializeField] private Animator soldierAnimator;

    // Everytime you add a variable, add it to ResetTriggerVariables, ResetTriggers, and SwitchTriggers

    [SerializeField] private bool triggerSpawn = false;
    [SerializeField] private bool triggerDamage = false;

    [SerializeField] private bool triggerDisable = false;
    [SerializeField] private bool triggerEnable = false;

    [SerializeField] private bool triggerDeath = false;

    [SerializeField] private SoldierMeshAnimatable soldierMeshAnimatable;

    void Start() {
        MAX_DURATION = 0.5f;
        if (this.soldierAnimator == null) {
            this.soldierAnimator = gameObject.GetComponent<Animator>();
        }
        this.ResetTriggerVariables();
        this.isPlaying = false;

    }

    public Animator GetAnimator() {
        if(this.soldierAnimator == null) {
            this.soldierAnimator = gameObject.GetComponent<Animator>();
        }
        return this.soldierAnimator;
    }

    // Functions to be called by external scripts
  
    public void Spawn() {
        this.triggerSpawn = true;
    }
    public void Damage() {
        this.triggerDamage = true;
    }
    public void Death() {
        this.triggerDeath = true;
    }
    public void Disable() {
        this.triggerDisable = true;
    }
    public void Enable() {
        this.triggerEnable = true;
    }

    // Functions to be called by animation events. These provide access to SoldierMeshAnimatable
    public void PlayMeshDeath() {
        General.LogEntrance("SoldieAnimatable PlayMeshDeath");
        this.GetSoldierMeshAnimatable().Death();
    }

    public void PlayMeshSpawn() {
        General.LogEntrance("SoldieAnimatable PlayMeshSpawn");
        this.GetSoldierMeshAnimatable().Spawn();
    }

    // Always add
    public override void ResetTriggers() {
        soldierAnimator.ResetTrigger(SPAWN);
        soldierAnimator.ResetTrigger(DAMAGE);

        soldierAnimator.ResetTrigger(ENABLE);
        soldierAnimator.ResetTrigger(DISABLE);

        soldierAnimator.ResetTrigger(DEATH);
    }

    void Update() {
        SwitchTriggers();
    }

    // ADD TRIGGER BOOL
    public override void ResetTriggerVariables() {
        triggerSpawn = false;
        triggerDamage = false;

        triggerEnable = false;
        triggerDisable = false;

        triggerDeath = false;
    }

    public override void SwitchTriggers() {
        ResetTriggers();
        if (triggerSpawn) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(SPAWN);
        }
        else if (triggerDamage) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(DAMAGE);
        }
        else if (triggerEnable) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(ENABLE);
        }
        else if (triggerDisable) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(DISABLE);
        }
        else if (triggerDeath) {
            this.AnimationPlay();
            this.GetAnimator().SetTrigger(DEATH);
        }
        ResetTriggerVariables();
    }

    public SoldierMeshAnimatable GetSoldierMeshAnimatable() {
        if(this.soldierMeshAnimatable == null) {
            this.soldierMeshAnimatable = GetComponentInChildren<SoldierMeshAnimatable>();
        }
        return this.soldierMeshAnimatable;
    }
}
