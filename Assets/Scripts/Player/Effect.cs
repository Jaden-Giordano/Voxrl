using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StatsEffect {
    public float health;
    public float mana;
    public float speed;

    public StatsEffect(float h, float m, float speed) {
        this.health = h;
        this.mana = m;
        this.speed = speed;
    }

    public StatsEffect(StatsEffect c) {
        this.health = c.health;
        this.mana = c.mana;
        this.speed = c.speed;
    }
}

public abstract class Effect {

    #region Base Types

    protected bool _statsBased = false;

    public bool statsBased {
        get { return _statsBased; }
    }

    protected StatsEffect _statsEffected;

    public StatsEffect statsEffected {
        get { return _statsEffected; }
    }

    protected bool _transformBased = false;

    public bool transformBased {
        get { return _transformBased; }
    }

    private Vector3 force = Vector3.zero;

    protected bool _invokesStatus = false;

    public bool invokesStatus {
        get { return _invokesStatus; }
    }

    protected List<Status> _status = new List<Status>();

    public List<Status> status {
        get { return _status; }
    }

    #endregion

    public Entity owner;

    public float probability = 0;

    public Effect(Entity owner) {
        this.owner = owner;
    }

    public Effect(Effect c) {
        this.owner = c.owner;
        this.probability = c.probability;
        this._statsBased = c.statsBased;
        this._transformBased = c.transformBased;
        this._invokesStatus = c.invokesStatus;
        this._statsEffected = new StatsEffect(c.statsEffected);
        this.force = c.force;
        List<Status> s = new List<Status>();
        foreach (Status i in c.status) {
            s.Add(new Status(i));
        }
        this._status = s;
    }

    public void ApplyEffect(Transform pT) {
        pT.GetComponent<Rigidbody>().AddForce(force);
    }

    public virtual Effect Copy() {
        return null;
    }
	
}
