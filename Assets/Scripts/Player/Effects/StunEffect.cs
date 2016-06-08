using UnityEngine;
using System.Collections;

public class StunEffect : Effect {

	public StunEffect(BattleSystem owner, float probability) : base(owner) {
        this.probability = probability;
        this._invokesStatus = true;
        this._status.Add(new Status(StatusType.Stunned, 1, 2f));
    }

}
