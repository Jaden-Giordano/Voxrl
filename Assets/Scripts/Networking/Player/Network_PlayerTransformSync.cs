using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Network_PlayerTransformSync : NetworkBehaviour {

    Transform pTransform;

    [SyncVar]
    private Vector3 syncPosition;
    [SerializeField]
    float lerpRate = 15;

    void Start() {
        pTransform = this.transform;
    }

	void FixedUpdate () {
        TransmitPosition();
        LerpPosition();
	}

    void LerpPosition() {
        if (!isLocalPlayer)
            pTransform.position = Vector3.Lerp(pTransform.position, syncPosition, Time.deltaTime*lerpRate);
    }

    [Command]
    void CmdProvidePositionToServer(Vector3 position) {
        syncPosition = position;
    }

    [ClientCallback]
    void TransmitPosition() {
        if (isLocalPlayer)
            CmdProvidePositionToServer(pTransform.position);
    }

}
