using UnityEngine;
using System.Collections;

public class Brute : Entity {

    [SerializeField]
    public WeaponSettings testWeapon;

    protected override void Start() {
        base.Start();

        this.baseStats = this.gameObject.AddComponent<BruteStats>();

        // Placing Item in inventory and into hand
        Club c = new Club(this, testWeapon);

        inventory.AddItem(c);
        inventory.AttachToSlot(c, Inventory.Slot.RightHand);
    }

    protected override void Update() {
        base.Update();

        if (Input.GetMouseButton(0)) {
            this.battleSystem.UseAbility(0);
        }

        Debug.Log("Brute: "+this.baseStats.Vitality+" : "+this.baseStats.maxHealth);
    }

}
