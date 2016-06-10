using UnityEngine;
using System.Collections;

public class CamMove : MonoBehaviour {

    Vector2 pan = Vector2.zero;

	void Update () {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDir.y += (Input.GetKey(KeyCode.Q)) ? -1 : 0;
        moveDir.y += (Input.GetKey(KeyCode.E)) ? 1 : 0;

        pan += new Vector2(-3 * Input.GetAxis("Mouse X"), 3 * Input.GetAxis("Mouse Y"));
        if (pan.y < -65)
            pan.y = -65;
        else if (pan.y > 85)
            pan.y = 85;

        this.transform.rotation = Quaternion.Euler(-pan.y, -pan.x, 0);

        this.transform.Translate(moveDir * 3);
    }

}
