using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class NewMonoBehaviourScript : MonoBehaviourPunCallbacks
{
    public void Oyna()
    {
        // Photon sunucusuna ba�lan
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); // Ba�lan diyoruz
        }
        else
        {
            PhotonNetwork.JoinRandomRoom(); // Ba�l�ysa direkt odaya gir diyoruz
        }
    }
    public void Ayarlar()
    {
        Debug.Log("Ayarlar a��l�yor...");
    }
    public void Cikis()
    {
        Application.Quit(); // Ger�ek oyunda ��k�� yapar

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    // Ba�lant� ba�ar�l� olursa bu fonksiyon �a�r�l�r
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom(); // Ba�lant� tamam, odaya gir
    }
    // Oda yoksa yeni oda olu�tur
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 4 });
    }
    // Odaya girince oyun sahnesine ge�
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("lobi"); // "Oyun" sahnesini y�kl�yor
    }
}
