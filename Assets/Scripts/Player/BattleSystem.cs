using UnityEngine;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

    public Ability[] abilities;

    public Stats stats;

    public List<Status> currentStatus = new List<Status>();

    void Start() {
        abilities = new Ability[6];
        stats = GetComponent<Stats>();
    }
    
    void Update() {
        List<Status> rmvQueue = new List<Status>();
        foreach (Status i in currentStatus) {
            i.timeLeft -= Time.deltaTime;
            if (i.Fisished)
                rmvQueue.Add(i);
        }
        foreach (Status i in rmvQueue) {
            currentStatus.Remove(i);
        }
    }

    protected void UseAbility(int index) {
        Ability a = abilities[index];
        if (a.available) {
            // play attack animation
            if (a.selfEffect)
                this.Affect(a.GenerateEffects());
            else {
                GameObject[] hits = a.GetObjectsAffected(this.transform.position);
                foreach (GameObject i in hits) {
                    if (i.tag == "Mob") {
                        BattleSystem mob = i.GetComponent<BattleSystem>();
                        mob.Affect(a.GenerateEffects());
                    }
                }
            }
            a.Reset();
        }
    }

    public void Affect(Effect[] effects) {
        foreach (Effect i in effects) {
            if (i.statsBased)
                stats.ApplyEffect(i);
            if (i.transformBased) {
                i.ApplyEffect(this.transform);
            }
            if (i.invokesStatus) {
                this.InvokeStatus(i.status);
            }
        }
    }

    public void InvokeStatus(Status s) {
        currentStatus.Add(s);
    }

    public void InvokeStatus(List<Status> s) {
        foreach (Status i in s)
            InvokeStatus(i);
    }

    public void InvokeStatus(StatusType type, float percentage, float time) {
        InvokeStatus(new Status(type, percentage, time));
    }

    public void AwardKill(int exp) {
        this.stats.experience += exp;
    }

}
