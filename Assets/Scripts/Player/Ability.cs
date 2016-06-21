using UnityEngine;
using System.Collections.Generic;

public abstract class Ability {

    protected List<Effect> effects;

    private float counter = 0;

    public float cooldown;

    protected bool _selfAfflict = false;

    public bool selfAfflict {
        get { return _selfAfflict; }
    }

    protected bool _launchesProjectile = false;

    public bool launchesProjectile {
        get { return _launchesProjectile; }
    }

    public bool available {
        get { return counter >= cooldown; }
    }

    public float range;

    // gets area in front of player, in degrees rotation
    public Vector3i aoe = Vector3i.zero;

    protected Weapon owner;

    public string name = "Ability";

    public Ability(Weapon owner) {
        effects = new List<Effect>();
        this.owner = owner;
    }

    public void AddEffect(Effect e) {
        this.effects.Add(e);
    }

    public Effect[] GenerateEffects() {
        List<Effect> gen = new List<Effect>();

        float p = Random.value;

        foreach (Effect i in effects) {
            if (i.probability >= p)
                gen.Add(i.Copy());
        }

        return gen.ToArray();
    }

    public void Update() {
        if (counter < cooldown)
            counter += Time.deltaTime;
    }

    public void Reset() {
        counter = 0;
    }

}
