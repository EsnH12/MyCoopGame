using UnityEngine;
using Photon.Pun;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon'a baðlanýlýyor...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon'a baþarýyla baðlandý");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi");
    }
}
