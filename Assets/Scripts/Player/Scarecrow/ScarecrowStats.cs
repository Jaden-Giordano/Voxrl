using UnityEngine;
using System.Collections;

public class ScarecrowStats : Stats {

    protected override void Start() {
        base.Start();
        this.SpendSkillPoints(SkillType.Vitality, 999);
        Debug.Log(maxHealth);
    }

}
