using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BruteStats : Stats {

    protected override void Start() {
        base.Start();
        this.Strength = 2;
        this.Vitality = 2;
    }

}
