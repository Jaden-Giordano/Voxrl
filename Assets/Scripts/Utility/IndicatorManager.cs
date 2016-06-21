using UnityEngine;
using System.Collections.Generic;

public class IndicatorManager : MonoBehaviour {

    #region Prefabs

    [SerializeField]
    public GameObject TextPrefab;

    #endregion

    private static IndicatorManager _Instance;
    public static IndicatorManager Instance {
        get { return _Instance; }
    }

    protected List<Indicator> indicators;

	void Start () {
        _Instance = this;
        indicators = new List<Indicator>();
	}
	
	void Update () {
        List<Indicator> rmvq = new List<Indicator>();
	    foreach (Indicator i in indicators) {
            if (i.Alive)
                i.Update();
            else
                rmvq.Add(i);
        }
        foreach (Indicator i in rmvq) {
            i.Remove();
            indicators.Remove(i);
        }
	}

    public void DisplayDamage(float damage, Transform location) {
        RisingText rt = new RisingText(Mathf.FloorToInt(Mathf.Abs(damage)).ToString(), location, 1, 1);
        if (damage < 0)
            rt.SetColor(new Color(255, 0, 0));
        else
            rt.SetColor(new Color(0, 255, 0));
        indicators.Add(rt);
    }

}
