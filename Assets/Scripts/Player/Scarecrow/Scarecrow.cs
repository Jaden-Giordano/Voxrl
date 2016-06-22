using UnityEngine;
using System.Collections;

public class Scarecrow : Entity {

    protected override void Start() {
        base.Start();

        this.baseStats = this.gameObject.AddComponent<ScarecrowStats>();
    }

    protected override void Update() {
        base.Update();

        Debug.Log("Scarecrow: " + this.baseStats.Vitality + " : " + this.baseStats.maxHealth);
    }

}
