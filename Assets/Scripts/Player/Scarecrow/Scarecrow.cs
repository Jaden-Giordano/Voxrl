using UnityEngine;
using System.Collections;

public class Scarecrow : Entity {

    protected override void Start() {
        base.Start();

        this.baseStats = this.gameObject.AddComponent<ScarecrowStats>();
    }

}
