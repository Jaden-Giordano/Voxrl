using UnityEngine;
using System.Collections;

[System.Serializable]
public class Club : Weapon {

	public Club(Entity owner) : base(owner, AnimationType.Bash) {
        this.damage = 5;

        localPos = new Vector3(0, 0, -0.0003f);

        this.main = new BashAbility(this, this.damage);
    }

    public Club(Entity owner, WeaponSettings settings) : base(owner, settings) {
        localPos = new Vector3(0, 0, -0.0003f);

        this.main = new BashAbility(this, this.damage);
    }

}
