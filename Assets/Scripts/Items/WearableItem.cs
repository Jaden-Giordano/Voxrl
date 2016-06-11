using UnityEngine;
using System.Collections;

public class WearableItem : ModifiedStatsItem { // TODO Add weight so it can only be carried with a certain amount of strength

    public Vector3 localPos;

    public WearableItem(Entity owner) : base(owner) {

    }

}
