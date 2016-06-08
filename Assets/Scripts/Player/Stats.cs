using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health = 100;
    public float maxHealth = 100;
    public float mana = 40;
    public float speed = 5;
    public float damage = 20;

    public int experience = 1;

    public int skillPoints = 0;

    public bool alive = true;

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
        this.speed += e.statsEffected.speed;

        if (this.health <= 0) {
            e.owner.AwardKill(AwardExp);
            alive = false;
        }
    }

}
