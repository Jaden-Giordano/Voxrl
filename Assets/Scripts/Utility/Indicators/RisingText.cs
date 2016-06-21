using UnityEngine;

public class RisingText : Indicator {

    public string text;

    public TextMesh textMesh;

    public RisingText(string text, Transform start, float time, float speed) : base(time, speed) {
        this.text = text;

        this.indicatorObj = Object.Instantiate(IndicatorManager.Instance.TextPrefab);
        textMesh = this.indicatorObj.GetComponent<TextMesh>();
        textMesh.text = this.text;
        this.indicatorObj.transform.position = start.position;
    }

    public override void Update() {
        base.Update();

        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, time * 2);

        this.indicatorObj.transform.position = Vector3.Lerp(this.indicatorObj.transform.position, this.indicatorObj.transform.position + Vector3.up, speed*Time.deltaTime);

        this.indicatorObj.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.FindChild("Camera"));
        this.indicatorObj.transform.localEulerAngles += new Vector3(0, 180, 0);
    }

    public void SetColor(Color c) {
        this.textMesh.color = c;
    }

}