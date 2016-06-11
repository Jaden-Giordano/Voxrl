﻿using UnityEngine;
using System.Collections;

public enum AnimationType {
    Swing,
    Bash,
    Wound,
    Punch
}

public class Weapon : ModifiedStatsItem {

    public float damage = 0;

    [SerializeField]
    public AnimationType animType;

    protected Ability main;
    protected Ability special;

    protected Vector3 localPos;

	public Weapon(Entity owner, AnimationType animType) : base(owner) {
        this.animType = animType;

        localPos = Vector3.zero;
    }

    public void Update() {
        main.Update();
        special.Update();
    }

    public void Attach(Transform parent) {
        GameObject aW = Object.Instantiate(prefab);
        aW.transform.parent = parent;
        aW.transform.localPosition = localPos;
        aW.transform.localScale = new Vector3(1, 1, 1);
        aW.transform.rotation = Quaternion.identity;
    }

    public Ability[] GetAbilities() {
        Ability[] a = new Ability[2];
        a[0] = main;
        a[1] = special;

        return a;
    }

}
