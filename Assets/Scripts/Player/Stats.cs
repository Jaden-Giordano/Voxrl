using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public enum SkillType {
        Strength,
        Vitality,
        Defense,
        Intelligence,
        Agility,
        Charisma,
        Luck
    }

    public float Strength = 1;
    public float Vitality = 1;
    public float Defense = 1;
    public float Intelligence = 1;
    public float Agility = 1;
    public float Charisma = 1;
    public float Luck = 1;

    public float health = 100;
    public float maxHealth {
        get { return (100*(Mathf.Pow(1.1f, Level)))*(1+(Vitality/40)); }
    }

    public float mana = 40;
    public float maxMana {
        get { return (15 * (Mathf.Pow(1.02f, Level))) * (1 + (Intelligence / 40)); }
    }

    public float speed {
        get { return (5 * (Mathf.Pow(1.01f, Level))) * (1 + (Agility / 40)); }
    }
    public float damage {
        get { return (15 * (Mathf.Pow(1.02f, Level))) * (1 + (Strength / 40)); }
    }

    public bool alive = true;

    public int experience = 1;
    public int skillPoints = 0;
    public int Level {
        get { return Mathf.FloorToInt((((.5f*experience)+5)*experience)+1); }
    }

    public int AwardExp {
        get { return Mathf.FloorToInt(.3f*Level+2); }
    }

    private int lastLevel = 0;

	protected virtual void Start () {
	}

	protected virtual void FixedUpdate () {
	    if (this.Level > lastLevel) {
            lastLevel = this.Level;
            this.skillPoints += 2;
        }
	}

    public virtual void ApplyEffect(Effect e) {
        this.health += e.statsEffected.health;
        this.mana += e.statsEffected.mana;

        if (this.health <= 0) {
            e.owner.AwardKill(AwardExp);
            alive = false;
        }
    }

    public virtual void SpendSkillPoints(SkillType t, int amt) {
        if (skillPoints >= amt) {
            skillPoints -= amt;
            switch (t) {
                case SkillType.Agility:
                    this.Agility += amt;
                    break;
                case SkillType.Charisma:
                    this.Charisma += amt;
                    break;
                case SkillType.Defense:
                    this.Defense += amt;
                    break;
                case SkillType.Intelligence:
                    this.Intelligence += amt;
                    break;
                case SkillType.Luck:
                    this.Luck += amt;
                    break;
                case SkillType.Strength:
                    this.Strength += amt;
                    break;
                case SkillType.Vitality:
                    this.Vitality += amt;
                    break;
            }
        }
    }

}
