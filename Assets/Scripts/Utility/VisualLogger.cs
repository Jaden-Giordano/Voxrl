using UnityEngine;
using System.Collections.Generic;

public class VisualLogger : MonoBehaviour {

    public static VisualLogger Instance;

    struct DebugTexture
    {
        public DebugTexture(Texture2D tex, Rect pos)
        {
            this.tex = tex;
            this.pos = pos;
        }
        public Texture2D tex;
        public Rect pos;
    }

    Dictionary<int, DebugTexture> debugTextures = new Dictionary<int, DebugTexture>();
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public int AddTexture(Texture2D tex, Rect pos)
    {
        int i = debugTextures.Count;
        debugTextures.Add(i, new DebugTexture(tex, pos));
        return i;
    }

    public void UpdateTexture(int id, Texture2D tex)
    {
        debugTextures[id] = new DebugTexture(tex, debugTextures[id].pos);
    }
    public void UpdateTexture(int id, Texture2D tex, Rect pos)
    {
        debugTextures[id] = new DebugTexture(tex, pos);
    }

    private void OnGUI()
    {
        foreach(DebugTexture dTex in debugTextures.Values)
        {
            GUI.DrawTexture(dTex.pos, dTex.tex);
        }
    }

}
