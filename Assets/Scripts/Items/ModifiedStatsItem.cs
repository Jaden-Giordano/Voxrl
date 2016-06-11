using UnityEngine;
using System.Collections;

public class ModifiedStatsItem : Item {

    protected ModifiedStats _stats;

    public ModifiedStats stats {
        get { return _stats; }
        protected set { _stats = value; }
    }

	public ModifiedStatsItem(Entity owner) : base(owner) {
        _stats = new ModifiedStats();
    }

}
