using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;

    private new Transform camera;
    private Transform focus;
    private Transform offset;
    private Vector3 finalOffset;
    [SerializeField]
    private float cameraHorizontalPanSpeed = 5f;
    [SerializeField]
    private float cameraVerticalPanSpeed = 4f;
    private Vector2 pan = Vector2.zero;

    [Range(0, 1)]
    public float zoom = 1;

    void Start() {
        camera = this.transform.FindChild("Camera");
        this.focus = this.transform.FindChild("Focus");
        this.offset = this.focus.FindChild("Offset");
        this.finalOffset = offset.localPosition;
    }

    void Update() {
        pan += new Vector2(-cameraHorizontalPanSpeed * Input.GetAxis("Mouse X"), cameraVerticalPanSpeed * Input.GetAxis("Mouse Y"));
        if (pan.y < -85)
            pan.y = -85;
        else if (pan.y > 85)
            pan.y = 85;

        this.focus.transform.rotation = Quaternion.Euler(-pan.y, -pan.x, 0);

        CharacterController controller = GetComponent<CharacterController>();

        if (controller.isGrounded) {
            float rot = this.focus.transform.rotation.eulerAngles.y;
            Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * rot), 0, Mathf.Cos(Mathf.Deg2Rad * rot)) * Input.GetAxis("Vertical");
            Vector3 right = -new Vector3(Mathf.Sin(Mathf.Deg2Rad * (rot-90)), 0, Mathf.Cos(Mathf.Deg2Rad * (rot-90))) * Input.GetAxis("Horizontal");

            moveDirection = forward + right;

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        UpdateCamera();
    }

    void UpdateCamera() {
        float s = Input.GetAxis("Mouse ScrollWheel");
        if (s < 0)
            zoom += .05f;
        else if (s > 0)
            zoom -= .05f;
        if (zoom > 1)
            zoom = 1;
        else if (zoom < 0)
            zoom = 0;

        Vector3 finalPos = new Vector3(0, finalOffset.y * (zoom * .8f), finalOffset.z * (zoom + .05f));
        offset.localPosition = finalPos;

        camera.position = offset.position;
        camera.LookAt(focus);
    }

}
