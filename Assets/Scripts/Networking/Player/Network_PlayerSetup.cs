using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Network_PlayerSetup : NetworkBehaviour {

	void Start () {
	    if (isLocalPlayer) {
            GameObject.Find("Main Camera").SetActive(false);
            this.GetComponent<CharacterController>().enabled = true;
            this.GetComponent<PlayerController>().enabled = true;
            this.transform.FindChild("Camera").gameObject.SetActive(true);
            this.GetComponent<AudioListener>().enabled = true;
        }
	}

}
