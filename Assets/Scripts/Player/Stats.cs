using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public float health = 100;
    public float mana = 40;
    public float speed = 5;

    public int experience = 1;

    public int skillPoints = 0;

    public bool alive = true;

    public int Level {
        get { return Mathf.FloorToInt((((.5f*experience)+5)*experience)+1); }
    }

    public int AwardExp {
        get { return Mathf.FloorToInt(.3f*Level); }
    }

    private int lastLevel = 0;

	void Start () {
        this.health = 100;
	}

	void FixedUpdate () {
	    if (this.Level > lastLevel) {
            lastLevel = this.Level;
            this.skillPoints += 2;
        }
	}

    public void ApplyEffect(Effect e) {
        this.health += e.StatsEffected.health;
        this.mana += e.StatsEffected.mana;
        this.speed += e.StatsEffected.speed;

        if (this.health <= 0) {
            e.owner.AwardKill(AwardExp);
            alive = false;
        }
    }

}
