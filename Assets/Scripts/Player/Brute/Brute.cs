using UnityEngine;
using System.Collections;

public class Brute : Entity {

	protected override void Start() {
        base.Start();

        this.baseStats = new BruteStats();
    }

    protected override void Update() {
        base.Update();

        if (Input.GetMouseButton(0)) {
            this.battleSystem.UseAbility(0);
        }
    }

}
