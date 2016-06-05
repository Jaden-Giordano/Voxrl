using UnityEngine;
using System.Collections.Generic;
using System.Text;

public class Logger : MonoBehaviour {

    private static Logger _Instance;

    public static Logger Instance {
        get { return _Instance; }
    }

    private List<string> logs = new List<string>();

	void Start () {
        _Instance = this;
	}

    public void AddLog(string msg) {
        logs.Add(msg);
    }

    public void OutputLog() {
        List<StringBuilder> bs = new List<StringBuilder>();

        int index = 0;
        foreach (string i in logs) {
            if (index % 100 == 0)
                bs.Add(new StringBuilder());
            bs[bs.Count - 1].Append(i + "\n");
            index++;
        }
        logs.Clear();

        foreach(StringBuilder i in bs) {
            Debug.Log(i.ToString());
        }
    }

}
