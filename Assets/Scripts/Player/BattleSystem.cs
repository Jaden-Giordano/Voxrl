using UnityEngine;
using System.Collections.Generic;

public class BattleSystem : MonoBehaviour {

    public List<Status> currentStatus = new List<Status>();

    public Entity owner;

    protected Effect[] currentEffects;

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
        Ability a = owner.inventory.RightHand.GetAbilities()[index];
        if (a != null) {
            if (a.available) {
                Logger.Instance.Log("Used Ability " + index);
                currentEffects = owner.ApplyModifiedStats(a.GenerateEffects());
                if (index == 0)
                    owner.Animate(index);

                if (a.selfAfflict)
                    this.Affect(currentEffects);
                
                a.Reset();
            }
        }
    }

    public virtual void Affect(Effect[] effects) {
        this.owner.Affect(effects);
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

    public virtual void Hit(Entity e) {
        if (owner.anim.GetBool("Attacking"))
            if (currentEffects != null)
                if (currentEffects.Length > 0) {
                    e.battleSystem.Affect(currentEffects);
                }
    }

}
