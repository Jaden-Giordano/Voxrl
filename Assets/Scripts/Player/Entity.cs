using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public BattleSystem battleSystem;
    Animator anim;
    PlayerController controller;

    public Stats baseStats;

    public ModifiedStats modifiedStats;

    public ModifiedStats FinalStats {
        get {
            ModifiedStats m = new ModifiedStats();

            m.maxHealth = modifiedStats.maxHealth + baseStats.maxHealth;
            m.maxMana = modifiedStats.maxMana + baseStats.maxMana;
            m.damage = modifiedStats.damage + baseStats.damage;
            m.speed = modifiedStats.speed + baseStats.speed;
            m.damageReduce = modifiedStats.damageReduce + baseStats.damageReduce;

            return m;
        }
    }

    [SerializeField]
    public Club testWeapon;

    public Inventory inventory;

	protected virtual void Start() {
        this.battleSystem = GetComponent<BattleSystem>();
        this.anim = GetComponentInChildren<Animator>();
        this.controller = GetComponent<PlayerController>();

        baseStats = new Stats();

        modifiedStats = new ModifiedStats();

        testWeapon.Attach(controller.player.FindChild("Armature").FindChild("Hand.R").FindChild("Hand.R_end")); // TODO make more modular

        inventory = new Inventory();
    }

    protected virtual void Update() {

    }

    public virtual void Animate(int index) {
        switch (testWeapon.animType) {
            case AnimationType.Bash:
                anim.Play("BashAttack0" + index);
                break;
        }
    }

    public virtual void RecalculateModifiedStats() {
        modifiedStats = inventory.CalculateModifiedStats();
    }

    public virtual Effect[] ApplyModifiedStats(Effect[] effects) {
        foreach (Effect e in effects) {
            if (e.statsBased) {
                if (e.statsEffected.health < 0) {
                    e.statsEffected.health -= FinalStats.damage;
                    if (e.statsEffected.health > 0)
                        e.statsEffected.health = 0;
                }
            }
        }

        return effects;
    }

    public virtual void Effected(Effect[] effects) {
        foreach (Effect i in effects) {
            if (i.statsBased) {
                float hm = i.statsEffected.health + this.FinalStats.damageReduce;
                if (i.statsEffected.health < 0)
                    hm = (hm > 0) ? 0 : hm;
                this.baseStats.health += hm;
                this.baseStats.mana += i.statsEffected.mana;
                //this.baseStats.speed += i.statsEffected.speed; TODO make speed maxSpeed and speed, so it can be modified
            }
            if (i.transformBased) {
                // TODO make applying forces work
            }
            if (i.invokesStatus) {
                this.battleSystem.InvokeStatus(i.status);
            }
        }
    }

}
