using UnityEngine;

public abstract class Indicator {

    public float time;
    public float speed;

    public GameObject indicatorObj;

    public bool Alive {
        get { return time > 0; }
    }

    public Indicator(float time, float speed) {
        this.time = time;
        this.speed = speed;
    }

    public virtual void Update() {
        this.time -= Time.deltaTime * speed;
    }

    public virtual void Remove() {
        Object.Destroy(indicatorObj);
    }

}