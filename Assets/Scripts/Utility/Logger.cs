using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System;

public class Logger : MonoBehaviour {

    public class DebugLine {
        public Vector3 start;
        public Vector3 end;
        public float time;
        private bool forever = false;
        public bool alive {
            get { return (forever) ? true:(time > 0); }
        }

        public DebugLine(Vector3 start, Vector3 end, float time) {
            this.start = start;
            this.end = end;

            if (time == -1)
                forever = true;
            this.time = time;
        }
    }

    public class DebugRay {
        public Ray ray;
        public float time;
        private bool forever = false;
        public bool alive {
            get { return (forever) ? true : (time > 0); }
        }

        public DebugRay(Ray r, float time) {
            this.ray = r;

            if (time == -1)
                forever = true;
            this.time = time;
        }
    }

    private static Logger _Instance;

    public static Logger Instance {
        get { return _Instance; }
    }

    private List<string> logs = new List<string>();

    private List<DebugLine> lines = new List<DebugLine>();

    private List<DebugRay> rays = new List<DebugRay>();

    void Start() {
        _Instance = this;

        StartCoroutine(OutputLogs());
        StartCoroutine(DrawLines());
        StartCoroutine(DrawRays());
    }

    void Update() {
        List<DebugLine> rmvQ = new List<DebugLine>();
        foreach (DebugLine i in lines) {
            if (i.alive)
                i.time -= Time.deltaTime;
            else
                rmvQ.Add(i);
        }
        foreach (DebugLine i in rmvQ) {
            lines.Remove(i);
        }

        List<DebugRay> rmvQr = new List<DebugRay>();
        foreach (DebugRay i in rays) {
            if (i.alive)
                i.time -= Time.deltaTime;
            else
                rmvQr.Add(i);
        }
        foreach (DebugRay i in rmvQr) {
            rays.Remove(i);
        }
    }

    public void AddLog(string msg) {
        logs.Add(msg);
    }

    public void Log(string msg) {
        logs.Add(msg);
    }

    public void Log(object msg) {
        logs.Add(msg.ToString());
    }

    /// <summary>
    /// Draw line in the editor.
    /// </summary>
    /// <param name="start">Start point of line</param>
    /// <param name="end">End point of line</param>
    /// <param name="time">Amount of time the line will be drawn to the screen; -1 is forever.</param>
    public void DrawLine(Vector3 start, Vector3 end, float time) {
        this.lines.Add(new DebugLine(start, end, time));
    }

    /// <summary>
    /// Draw ray in the editor (with arrow).
    /// </summary>
    /// <param name="ray">Ray to draw.</param>
    /// <param name="time">Amount of time the Ray will be drawn to the screen; -1 is forever.</param>
    public void DrawRay(Ray ray, float time) {
        this.rays.Add(new DebugRay(ray, time));
    }

    [Obsolete("Use Log, outputting is automatic.")]
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

        foreach (StringBuilder i in bs) {
            Debug.Log(i.ToString());
        }
    }

    public string GetTopLog() {
        if (logs.Count == 0)
            return null;
        string r = logs[logs.Count - 1];
        logs.RemoveAt(logs.Count - 1);
        return r;
    }

    public DebugLine[] GetLines() {
        return lines.ToArray();
    }

    public DebugRay[] GetRays() {
        return rays.ToArray();
    }

    private IEnumerator OutputLogs() {
        while (true) {
            string l = Logger.Instance.GetTopLog();
            if (l != null)
                Debug.Log(l);
            yield return null;
        }
    }

    private IEnumerator DrawLines() {
        while (true) {
            DebugLine[] lines = Logger.Instance.GetLines();


            foreach (DebugLine i in lines) {
                Debug.DrawLine(i.start, i.end);
                yield return null;
            }

            yield return null;
        }
    }

    private IEnumerator DrawRays() {
        while (true) {
            DebugRay[] rays = Logger.Instance.GetRays();

            foreach (DebugRay i in rays) {
                Debug.DrawLine(i.ray.origin, i.ray.origin + i.ray.direction);
                Vector3 larr = Quaternion.Euler(-45-90, 0, 0) * (i.ray.direction*0.1f);
                Vector3 rarr = Quaternion.Euler(45+90, 0, 0) * (i.ray.direction * 0.1f);
                Debug.DrawLine(i.ray.origin + i.ray.direction, i.ray.origin + i.ray.direction + larr);
                Debug.DrawLine(i.ray.origin + i.ray.direction, i.ray.origin + i.ray.direction + rarr);
            }

            yield return null;
        }
    }
}
