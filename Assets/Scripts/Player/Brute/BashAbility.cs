using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BashAbility : Ability {

    public BashAbility(BattleSystem owner) : base(owner) {
        this.cooldown = 4f;
        this.range = 2f;
        this.aoe = new Vector3i(15, 30, 15);

        this.AddEffect(new StunEffect(this.owner, .4f));

        DamageEffect dm = new DamageEffect(owner, 1f, owner.stats.damage*1.1f);
        this.AddEffect(dm);

        this.name = "Base Ability";
    }

}
