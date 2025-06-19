using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       PhotonNetwork.ConnectUsingSettings(); // photona baðlan
        Debug.Log("Photona baðlanýlýyor...");


    }

    public override void OnConnectedToMaster()  // photona baðlanýnca otomatik çalýþýr
    {
        Debug.Log("Photona baþarýyla baðlandý");
        PhotonNetwork.JoinLobby(); // lobiye katilma
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi");
    }
}
