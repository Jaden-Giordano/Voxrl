using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BruteStats : Stats {

    protected override void Start() {
        base.Start();
        this.maxHealth = (this.health *= 1.4f);
        this.mana *= .4f;
        this.speed = 3;
        this.damage *= 1.1f;
    }

}
