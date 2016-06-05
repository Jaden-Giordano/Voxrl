using UnityEngine;
using System.Collections;

public class Brute_BattleSystem : BattleSystem {

	protected override void Start() {
        base.Start();

        this.stats = new BruteStats();

        this.abilities[0] = new BashAbility();
    }

}
