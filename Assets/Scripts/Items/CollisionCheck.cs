using UnityEngine;
using System.Collections;

public class CollisionCheck : MonoBehaviour {

    protected BattleSystem battleSystem;

	protected void Start() {
        this.battleSystem = this.GetComponentInParent<BattleSystem>();
    }

    protected void OnTriggerEnter(Collider other) {
        Entity e = other.GetComponent<Entity>();
        if (e != null) {
            battleSystem.Hit(e);
        }
    }

}
