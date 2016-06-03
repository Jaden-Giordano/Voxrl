using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[NetworkSettings(sendInterval = 0.033f)]
public class Network_PlayerTransformSync : NetworkBehaviour {

    Transform pTransform;

    [SyncVar]
    private Vector3 syncPosition;
    [SyncVar]
    private Quaternion syncRotation;

    private Vector3 lastPosition;
    private Quaternion lastRotation;

    [SerializeField]
    private float movementThreshold = 0.5f;
    [SerializeField]
    private float rotationThreshold = 5f;

    [SerializeField]
    float lerpRate = 15;

    void Start() {
        pTransform = this.transform;
        lastPosition = pTransform.position;
        lastRotation = pTransform.rotation;
    }

	void FixedUpdate () {
        TransmitTransform();
	}

    void Update() {
        LerpPosition();
    }

    void LerpPosition() {
        if (!isLocalPlayer) {
            pTransform.position = Vector3.Lerp(pTransform.position, syncPosition, Time.deltaTime * lerpRate);
            pTransform.rotation = Quaternion.Lerp(pTransform.rotation, syncRotation, Time.deltaTime * lerpRate);
        }
    }

    [Command]
    void CmdProvideTransformToServer(Vector3 position, Quaternion rotation) {
        syncPosition = position;
        syncRotation = rotation;
    }

    [ClientCallback]
    void TransmitTransform() {
        if (isLocalPlayer) {
            if (Vector3.Distance(pTransform.position, lastPosition) > movementThreshold || Quaternion.Angle(pTransform.rotation, lastRotation) > rotationThreshold) {
                CmdProvideTransformToServer(pTransform.position, pTransform.rotation);
                lastPosition = pTransform.position;
                lastRotation = pTransform.rotation;
            }
        }
    }

}
