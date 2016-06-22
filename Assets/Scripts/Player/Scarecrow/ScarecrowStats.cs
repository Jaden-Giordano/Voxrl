using UnityEngine;
using System.Collections;

public class ScarecrowStats : Stats {

    protected override void Start() {
        this.Vitality = 999;
        base.Start();
        Debug.Log("MaxHP: "+maxHealth);
    }

}
