using UnityEngine;
using System.Collections.Generic;

public class Inventory {

    public enum Slot {
        RightHand,
        LeftArm,
        RightArm,
        Neck,
        Chest,
        Shoulders,
        Hands,
        Feet
    }

    public Entity owner;

    protected List<Item> items;

    protected Weapon rightHand;
    public Weapon RightHand {
        get { return rightHand; }
    }

    protected WearableItem leftArm;
    public WearableItem LeftArm {
        get { return leftArm; }
    }
    protected WearableItem rightArm;
    public WearableItem RightArm {
        get { return rightArm; }
    }

    protected WearableItem neck;
    public WearableItem Neck {
        get { return neck; }
    }

    protected WearableItem chest;
    public WearableItem Chest {
        get { return chest; }
    }

    protected WearableItem shoulders;
    public WearableItem Shoulders {
        get { return shoulders; }
    }

    protected WearableItem hands;
    public WearableItem Hands {
        get { return hands; }
    }

    protected WearableItem feet;
    public WearableItem Feet {
        get { return feet; }
    }

    public Inventory(Entity owner) {
        items = new List<Item>();
        this.owner = owner;
    }

    public void Update() {
        if (RightHand != null)
            rightHand.Update();
        foreach (Item i in items) {
            if (i is Weapon)
                ((Weapon)i).Update();
        }
    }

    public void AttachToSlot(Item i, Slot s) {
        if (items.Contains(i)) {
            switch (s) {
                case Slot.Chest:
                    if (i is ChestWearable) {
                        if (this.chest != null)
                            items.Add(this.chest);
                        items.Remove(i);
                        chest = (ChestWearable)i;
                    }
                    break;
                case Slot.Feet:
                    if (i is FeetWearable) {
                        if (this.feet != null)
                            items.Add(this.feet);
                        items.Remove(i);
                        feet = (FeetWearable)i;
                    }
                    break;
                case Slot.Hands:
                    if (i is HandsWearable) {
                        if (this.hands != null)
                            items.Add(this.hands);
                        items.Remove(i);
                        hands = (HandsWearable)i;
                    }
                    break;
                case Slot.LeftArm:
                    if (i is ArmWearable) {
                        if (this.leftArm != null)
                            items.Add(this.leftArm);
                        items.Remove(i);
                        leftArm = (ArmWearable)i;
                    }
                    break;
                case Slot.RightArm:
                    if (i is ArmWearable) {
                        if (this.leftArm != null)
                            items.Add(this.leftArm);
                        items.Remove(i);
                        leftArm = (ArmWearable)i;
                    }
                    break;
                case Slot.Neck:
                    if (i is NeckWearable) {
                        if (this.neck != null)
                            items.Add(this.neck);
                        items.Remove(i);
                        neck = (NeckWearable)i;
                    }
                    break;
                case Slot.RightHand:
                    if (i is Weapon) {
                        if (this.rightHand != null)
                            items.Add(this.rightHand);
                        items.Remove(i);
                        rightHand = (Weapon)i;
                        rightHand.Attach(owner.controller.player.FindChild("Armature").FindChild("Hand.R").FindChild("Hand.R_end"));
                    }
                    break;
                case Slot.Shoulders:
                    if (i is ShoulderWearable) {
                        if (this.shoulders != null)
                            items.Add(this.shoulders);
                        items.Remove(i);
                        shoulders = (ShoulderWearable)i;
                    }
                    break;
            }

            owner.RecalculateModifiedStats();
        }
    }

    public virtual ModifiedStats CalculateModifiedStats() {
        ModifiedStats m = new ModifiedStats();

        if (rightHand != null)
            m += rightHand.stats;
        if (leftArm != null)
            m += leftArm.stats;
        if (rightArm != null)
            m += rightArm.stats;
        if (neck != null)
            m += neck.stats;
        if (chest != null)
            m += chest.stats;
        if (feet != null)
            m += feet.stats;
        if (shoulders != null)
            m += shoulders.stats;
        if (hands != null)
            m += hands.stats;

        return m;
    }

    public virtual void AddItem(Item i) {
        this.items.Add(i);
    }

}
