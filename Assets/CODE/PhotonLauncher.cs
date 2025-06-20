using UnityEngine;
using Photon.Pun;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Photon'a ba�lan�l�yor...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Photon'a ba�ar�yla ba�land�");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi");
    }
}
