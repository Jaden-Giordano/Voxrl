using UnityEngine;
using System.Collections;

[System.Serializable]
public class Club : Weapon {

	public Club(Entity owner) : base(owner, AnimationType.Bash) {
        this.damage = 5;

        localPos = new Vector3(0, -0.1f, 0);

        this.main = new BashAbility(this);
    }

}
