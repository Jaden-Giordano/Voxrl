using UnityEngine;
using System.Collections.Generic;

public abstract class Effect {

    public struct StatsEffect {
        public float health;
        public float mana;
        public float speed;

        public StatsEffect(float h, float m, float speed) {
            this.health = h;
            this.mana = m;
            this.speed = speed;
        }
    }

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

    public BattleSystem owner;

    public float probability = 0;

    public Effect(BattleSystem owner) {
        this.owner = owner;
    }

    public void ApplyEffect(Transform pT) {
        pT.GetComponent<Rigidbody>().AddForce(force);
    }
	
}
