using UnityEngine;
using System.Collections.Generic;

public class Effect {

    protected bool _statsBased = false;

    public bool statsBased {
        get { return _statsBased; }
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

    public GameObject owner;

    public Effect(GameObject owner) {
        this.owner = owner;
    }

    public void ApplyEffect(Transform pT) {
        pT.GetComponent<Rigidbody>().AddForce(force);
    }
	
}
