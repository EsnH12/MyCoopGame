using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class NewMonoBehaviourScript : MonoBehaviourPunCallbacks
{
    public void Oyna()
    {
        // Photon sunucusuna baðlan
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); // Baðlan diyoruz
        }
        else
        {
            PhotonNetwork.JoinRandomRoom(); // Baðlýysa direkt odaya gir diyoruz
        }
    }
    public void Ayarlar()
    {
        Debug.Log("Ayarlar açýlýyor...");
    }
    public void Cikis()
    {
        Application.Quit(); // Gerçek oyunda çýkýþ yapar

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Baðlantý baþarýlý olursa bu fonksiyon çaðrýlýr
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom(); // Baðlantý tamam, odaya gir
    }
    // Oda yoksa yeni oda oluþtur
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }
    // Odaya girince oyun sahnesine geç
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("lobi"); // "Oyun" sahnesini yüklüyor
    }
}
