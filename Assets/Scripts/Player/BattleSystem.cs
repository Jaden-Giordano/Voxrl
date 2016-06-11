using UnityEngine;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

    public List<Status> currentStatus = new List<Status>();

    public Entity owner;

    protected virtual void Start() {
        owner = GetComponent<Entity>();
    }
    
    protected virtual void Update() {
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

    public virtual void UseAbility(int index) {
        Ability a = owner.testWeapon.GetAbilities()[index];
        if (a != null) {
            if (a.available) {
                Logger.Instance.Log("Used Ability " + index);
                Effect[] efs = owner.ApplyModifiedStats(a.GenerateEffects());
                if (index == 0)
                    owner.Animate(index);

                if (a.selfAfflict)
                    this.Effected(efs);
                else {
                    GameObject[] hits = a.GetEffectedObjects(this.transform);
                    foreach (GameObject i in hits) {
                        if (i.tag == "Mob") {
                            BattleSystem mob = i.GetComponent<BattleSystem>();
                            mob.Effected(efs);
                        }
                    }
                }
                
                a.Reset();
            }
        }
    }

    public virtual void Effected(Effect[] effects) {
        this.owner.Effected(effects);
    }

    public virtual void InvokeStatus(Status s) {
        currentStatus.Add(s);
    }

    public virtual void InvokeStatus(List<Status> s) {
        foreach (Status i in s)
            InvokeStatus(i);
    }

    public virtual void InvokeStatus(StatusType type, float percentage, float time) {
        InvokeStatus(new Status(type, percentage, time));
    }

    public virtual void AwardKill(int exp) {
        this.owner.baseStats.GiveExp(exp);
    }

}
