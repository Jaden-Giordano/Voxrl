using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BashAbility : Ability {

    public BashAbility(Weapon owner, float damage) : base(owner) {
        this.cooldown = 4f;
        this.range = 2f;
        this.aoe = new Vector3i(15, 30, 15);

        this.AddEffect(new StunEffect(owner.owner, .4f));

        DamageEffect dm = new DamageEffect(owner.owner, 1f, damage);
        this.AddEffect(dm);

        this.name = "Base Ability";
    }

}
