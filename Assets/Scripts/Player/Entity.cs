using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    BattleSystem battleSystem;
    Animator anim;
    PlayerController controller;

	protected virtual void Start() {
        this.battleSystem = GetComponent<BattleSystem>();
        this.anim = GetComponentInChildren<Animator>();
        this.controller = GetComponent<PlayerController>();
    }

    protected virtual void Update() {

    }

    public virtual void RegularAttack() {
        anim.Play("Attack01");
    }

}
