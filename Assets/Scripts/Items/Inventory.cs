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

    protected List<Item> items;

    protected Weapon rightHand;

    protected WearableItem leftArm;
    protected WearableItem rightArm;

    protected WearableItem neck;

    protected WearableItem chest;

    protected WearableItem shoulders;

    protected WearableItem hands;

    protected WearableItem feet;

    public Inventory() {
        items = new List<Item>();
    }

    public void Update() {
        rightHand.Update();
        foreach (Item i in items) {
            if (i is Weapon)
                ((Weapon)i).Update();
        }
    }

    public void AttachToSlot(Item i, Slot s) { // TODO Create and rename parts
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
        }
    }

    public virtual ModifiedStats CalculateModifiedStats() {
        ModifiedStats m = new ModifiedStats();

        m += rightHand.stats;
        m += leftArm.stats;
        m += rightArm.stats;
        m += neck.stats;
        m += chest.stats;
        m += feet.stats;
        m += shoulders.stats;
        m += hands.stats;

        return m;
    }

}
