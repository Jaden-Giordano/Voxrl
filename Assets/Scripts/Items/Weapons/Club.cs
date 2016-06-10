using UnityEngine;
using System.Collections;

public class Club : Weapon {

	public Club(Entity owner) : base(owner, AnimationType.Bash) {
        this.damage = 20;
    }

}
