using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;

    private Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    private Transform player;

    private new Transform camera;
    [SerializeField]
    private Transform focus;
    private Vector3 playerLocalFocus;
    [SerializeField]
    private Transform offset;
    private Vector3 finalOffset;
    [SerializeField]
    private float cameraHorizontalPanSpeed = 5f;
    [SerializeField]
    private float cameraVerticalPanSpeed = 4f;
    private Vector2 pan = Vector2.zero;

    [Range(0, 1)]
    public float zoom = 1;

    private Animator anim;

    void Start() {
        this.camera = this.transform.FindChild("Camera");
        if (focus == null)
            this.focus = this.transform.FindChild("Focus");
        this.playerLocalFocus = player.position+focus.position;
        if (offset == null)
            this.offset = this.focus.FindChild("Offset");
        this.finalOffset = offset.localPosition;
        this.anim = this.player.GetComponent<Animator>();
        anim.speed = 1.2f;
    }

    void Update() {
        this.focus.position = this.player.position + this.playerLocalFocus;

        pan += new Vector2(-cameraHorizontalPanSpeed * Input.GetAxis("Mouse X"), cameraVerticalPanSpeed * Input.GetAxis("Mouse Y"));
        if (pan.y < -65)
            pan.y = -65;
        else if (pan.y > 85)
            pan.y = 85;

        this.focus.transform.rotation = Quaternion.Euler(-pan.y, -pan.x, 0);

        CharacterController controller = this.player.GetComponent<CharacterController>();

        Vector3 inputAxis = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (controller.isGrounded) {
            float rot = this.focus.transform.rotation.eulerAngles.y;
            Vector3 forward = new Vector3(Mathf.Sin(Mathf.Deg2Rad * rot), 0, Mathf.Cos(Mathf.Deg2Rad * rot)) * inputAxis.z;
            Vector3 right = -new Vector3(Mathf.Sin(Mathf.Deg2Rad * (rot-90)), 0, Mathf.Cos(Mathf.Deg2Rad * (rot-90))) * inputAxis.x;

            moveDirection = forward + right;

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

            if (Mathf.Abs(inputAxis.x) > 0 || Mathf.Abs(inputAxis.z) > 0) {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
                    anim.SetBool("Walking", true);
            }
            else {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                    anim.SetBool("Walking", false);
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        player.LookAt(player.position + new Vector3(moveDirection.x, 0, moveDirection.z), Vector3.up);
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

        float dist = finalPos.magnitude;

        Vector3 dir = this.focus.TransformPoint(finalPos) - focus.position;
        Ray r = new Ray(focus.position, dir);

        Debug.DrawLine(focus.position, this.focus.TransformPoint(finalPos));

        float closestDist = finalPos.magnitude;

        RaycastHit[] hits = Physics.RaycastAll(r, dist);
        if (hits.Length > 0) {
            foreach (RaycastHit i in hits) {
                if (i.transform != this.transform) {
                    if (i.distance < closestDist)
                        closestDist = i.distance;
                }
            }
        }

        offset.localPosition = Vector3.Lerp(Vector3.zero, finalPos, (closestDist / finalPos.magnitude)-0.05f);

        camera.position = offset.position;
        camera.LookAt(focus);
    }

}
