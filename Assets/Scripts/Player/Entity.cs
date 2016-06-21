using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponSettings {
    public string name = "";
    public Rarity rarity = Rarity.Normal;
    public GameObject prefab;
    public float damage = 0;
    public AnimationType animType = AnimationType.Swing;
}

public class Entity : MonoBehaviour {

    [HideInInspector]
    public BattleSystem battleSystem;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public PlayerController controller;

    [HideInInspector]
    public Stats baseStats;

    [HideInInspector]
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
    public WeaponSettings testWeapon;

    [HideInInspector]
    public Inventory inventory;

	protected virtual void Start() {
        this.battleSystem = GetComponent<BattleSystem>();
        this.anim = GetComponentInChildren<Animator>();
        this.controller = GetComponent<PlayerController>();

        baseStats = new Stats();

        modifiedStats = new ModifiedStats();

        inventory = new Inventory(this);

        if (this is Brute) {
            // Placing Item in inventory and into hand
            Club c = new Club(this, testWeapon);

            inventory.AddItem(c);
            inventory.AttachToSlot(c, Inventory.Slot.RightHand);
        }
    }

    protected virtual void Update() {
        inventory.Update();
    }

    public virtual void Animate(int index) {
        switch (inventory.RightHand.animType) {
            case AnimationType.Bash:
                anim.SetBool("Attacking", true);
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

    public virtual void Affect(Effect[] effects) {
        Debug.Log("Before" + this.baseStats.health);
        foreach (Effect i in effects) {
            if (i.statsBased) {
                Debug.Log(this.FinalStats.damageReduce);
                float hm = i.statsEffected.health + this.FinalStats.damageReduce;
                if (i.statsEffected.health <= 0)
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
        Debug.Log("After: " + this.baseStats.health);
    }

}
