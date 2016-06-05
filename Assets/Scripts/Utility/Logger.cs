using UnityEngine;
using System.Collections.Generic;
using System.Text;
<<<<<<< HEAD

public class Logger : MonoBehaviour {
    
=======
using System.Collections;
using System;

public class Logger : MonoBehaviour {

>>>>>>> refs/remotes/origin/Logger
    private static Logger _Instance;

    public static Logger Instance {
        get { return _Instance; }
    }

    private List<string> logs = new List<string>();

	void Start () {
        _Instance = this;
<<<<<<< HEAD
	}

    public void AddLog(string msg) {
        logs.Add(msg);
    }

=======
        StartCoroutine(OutputLogs());
	}

    public void Log(string msg) {
        logs.Add(msg);
    }

    public void Log(object msg) {
        logs.Add(msg.ToString());
    }

    [Obsolete("Use Add Log, outputting is automatic.")]
>>>>>>> refs/remotes/origin/Logger
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

<<<<<<< HEAD
=======
    public string GetTopLog() {
        if (logs.Count == 0)
            return null;
        string r = logs[logs.Count - 1];
        logs.RemoveAt(logs.Count - 1);
        return r;
    }

    private IEnumerator OutputLogs() {
        while (true) {
            string l = Logger.Instance.GetTopLog();
            if (l != null)
                Debug.Log(l);
            yield return null;
        }
    }

>>>>>>> refs/remotes/origin/Logger
}
