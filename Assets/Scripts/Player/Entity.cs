using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    BattleSystem battleSystem;
    Animator anim;
    PlayerController controller;

    [SerializeField]
    public Club testWeapon;

	protected virtual void Start() {
        this.battleSystem = GetComponent<BattleSystem>();
        this.anim = GetComponentInChildren<Animator>();
        this.controller = GetComponent<PlayerController>();

        testWeapon.Attach(controller.player.FindChild("Armature").FindChild("Hand.R").FindChild("Hand.R_end"));
    }

    protected virtual void Update() {

    }

    public virtual void RegularAttack() {
        switch (testWeapon.animType) {
            case AnimationType.Bash:
                anim.Play("Attack01");
                break;
        }
    }

    public virtual Effect[] ApplyInventoryStats(Effect[] effects) {
        foreach (Effect e in effects) {
            if (e is DamageEffect) {
                ((DamageEffect)e).damage = ((DamageEffect)e).damage + testWeapon.damage;
            }
        }

        return effects;
    }

}
