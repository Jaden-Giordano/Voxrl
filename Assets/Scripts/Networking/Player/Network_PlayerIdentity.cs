using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Network_PlayerIdentity : NetworkBehaviour {

    [SyncVar]
    public string playerUniqueIdentity;
    private NetworkInstanceId playerNetID;

    private Transform pTransform;

    public override void OnStartLocalPlayer() {
        GetNetIdentity();
        SetIdentity();
    }

    void Awake() {
        pTransform = this.transform;
    }

    void Update() {
        if (pTransform.name == "" || pTransform.name == "TestPlayer(Clone)")
            SetIdentity();
    }

    [Command]
    void CmdTransmitIdentity(string id) {
        playerUniqueIdentity = name;
    }

    [Client]
    void GetNetIdentity() {
        playerNetID = GetComponent<NetworkIdentity>().netId;
        CmdTransmitIdentity(MakeUniqueIdentity());
    }

    private string MakeUniqueIdentity() {
        return "[Player " + playerNetID.ToString()+"]";
    }

    void SetIdentity() {
        if (!isLocalPlayer)
            pTransform.name = playerUniqueIdentity;
        else
            pTransform.name = MakeUniqueIdentity();
    }

}
