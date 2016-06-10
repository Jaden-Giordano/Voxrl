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

    [SerializeField]
    public AnimationType animType;

	public Weapon(Entity owner, AnimationType animType) : base(owner) {
        this.animType = animType;
    }

    public void Attach(Transform parent) {
        GameObject aW = Object.Instantiate(prefab);
        aW.transform.parent = parent;
        aW.transform.localPosition = new Vector3(0, 0, 0);
        aW.transform.localScale = new Vector3(1, 1, 1);
        aW.transform.rotation = Quaternion.identity;
    }

}
