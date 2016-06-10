using UnityEngine;
using System.Collections;

public enum AnimationType {
    Swing,
    Bash,
    Wound,
    Punch
}

public class Weapon : Item {

    public float damage = 0;

    public AnimationType animType;

	public Weapon(Entity owner, AnimationType animType) : base(owner) {
        this.animType = animType;
    }

}
