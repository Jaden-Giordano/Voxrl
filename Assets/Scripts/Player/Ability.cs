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
                gen.Add(i);
        }

        return gen.ToArray();
    }

    public GameObject[] GetEffectedObjects(Transform b) { // TODO not here
        List<GameObject> ef = new List<GameObject>();
        Debug.Log("AOE: " + aoe.x * aoe.y * aoe.z);
        for (int x = -aoe.x/2; x < aoe.x/2; x++) {
            for (int y = -aoe.y / 2; y < aoe.y / 2; y++) {
                for (int z = -aoe.z / 2; z < aoe.z / 2; z++) {
                    Quaternion angle = Quaternion.Euler(new Vector3(x, y, z));
                    Ray r = new Ray(b.position, angle * b.forward);
                    RaycastHit[] h = Physics.RaycastAll(r, range);
                    Logger.Instance.Log(h.Length);
                    foreach (RaycastHit i in h) {
                        if (i.transform != b)
                            if (i.transform.tag != "World" && i.transform.GetComponentInParent<Entity>() != null)
                                ef.Add(i.transform.gameObject);
                    }
                }
            }
        }
        return ef.ToArray();
    }

    public void Update() {
        if (counter < cooldown)
            counter += Time.deltaTime;
    }

    public void Reset() {
        counter = 0;
    }

}
