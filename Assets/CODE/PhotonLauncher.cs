using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PhotonLauncher : MonoBehaviourPunCallbacks
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       PhotonNetwork.ConnectUsingSettings(); // photona ba�lan
        Debug.Log("Photona ba�lan�l�yor...");


    }

    public override void OnConnectedToMaster()  // photona ba�lan�nca otomatik �al���r
    {
        Debug.Log("Photona ba�ar�yla ba�land�");
        PhotonNetwork.JoinLobby(); // lobiye katilma
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Lobiye girildi");
    }
}
