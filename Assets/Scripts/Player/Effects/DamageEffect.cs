using UnityEngine;
using System.Collections;

public class DamageEffect : Effect {

    private float _damage;
    public float damage {
        get { return _damage; }
        set {
            this._damage = value;
            this._statsEffected = new StatsEffect(-value, 0, 0);
        }
    }

	public DamageEffect(BattleSystem owner, float probability, float damage) : base(owner) {
        this.probability = probability;
        this._statsBased = true;
        this.damage = damage;
        this._statsEffected = new StatsEffect(-damage, 0, 0);
    }

}
