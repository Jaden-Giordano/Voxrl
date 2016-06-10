using UnityEngine;
using System.Collections;

[System.Serializable]
public class Club : Weapon {

	public Club(Entity owner) : base(owner, AnimationType.Bash) {
        this.damage = 5;
    }

}
