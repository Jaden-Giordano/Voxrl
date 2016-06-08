using UnityEngine;
using System.Collections;

public class DamageEffect : Effect {

	public DamageEffect(BattleSystem owner, float probability, float damage) : base(owner) {
        this.probability = probability;
        this._statsBased = true;
        this._statsEffected = new StatsEffect(-damage, 0, 0);
    }

}
