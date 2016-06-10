using UnityEngine;
using System.Collections;

public enum ModType {
    Add = 0,
    Mul = 1
}

public struct StatModifier<T> {
    public T val;
    public ModType modType;

    public StatModifier(T val, ModType t) {
        this.val = val;
        this.modType = t;
    }
}

public class ItemStats : MonoBehaviour {

    public StatModifier<float> Strength;
    public StatModifier<float> Vitality;
    public StatModifier<float> Defense;
    public StatModifier<float> Intelligence;
    public StatModifier<float> Agility;
    public StatModifier<float> Charisma;
    public StatModifier<float> Luck;

    public ItemStats() {
        Strength = new StatModifier<float>(0, ModType.Add);
        Intelligence = new StatModifier<float>(0, ModType.Add);
        Agility = new StatModifier<float>(0, ModType.Add);
        Defense = new StatModifier<float>(0, ModType.Add);
        Vitality = new StatModifier<float>(0, ModType.Add);
        Charisma = new StatModifier<float>(0, ModType.Add);
        Luck = new StatModifier<float>(0, ModType.Add);
    }

    public void GetEffectedStats(Stats s) {
        s.Strength = GetPostMod(s.Strength, Strength);
        s.Intelligence = GetPostMod(s.Intelligence, Intelligence);
        s.Agility = GetPostMod(s.Agility, Agility);
        s.Defense = GetPostMod(s.Defense, Defense);
        s.Vitality = GetPostMod(s.Vitality, Vitality);
        s.Charisma = GetPostMod(s.Charisma, Charisma);
        s.Luck = GetPostMod(s.Luck, Luck);
    }

    public float GetPostMod(float stat, StatModifier<float> s) {
        switch (s.modType) {
            case ModType.Add:
                return stat + s.val;
            case ModType.Mul:
                return stat * s.val;
        }
        return stat;
    }

}
