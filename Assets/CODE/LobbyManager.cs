using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Referansları")]
    public InputField roomInput;
    public Text durumyazısı;
    public GameObject teamSelectionPanel;

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            durumyazısı.text = "Sunucuya bağlanılıyor...";
        }

        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        string roomname = roomInput.text;

        if (!string.IsNullOrEmpty(roomname))
        {
            RoomOptions options = new RoomOptions { MaxPlayers = 4 };
            PhotonNetwork.CreateRoom(roomname, options);
            durumyazısı.text = "Oda oluşturuluyor...";
        }
        else
        {
            durumyazısı.text = "Lütfen oda ismi yaz!";
        }
    }

    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        durumyazısı.text = "Rastgele odaya katılınıyor...";
    }

    public override void OnCreatedRoom()
    {
        durumyazısı.text = "Oda kuruldu!";
    }

    public override void OnJoinedRoom()
    {
        durumyazısı.text = "Odaya girildi!";
        teamSelectionPanel.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        durumyazısı.text = "Oda bulunamadı, yeni oda oluşturuluyor...";
        string randomRoomName = "oda" + Random.Range(0, 10000);
        PhotonNetwork.CreateRoom(randomRoomName, new RoomOptions { MaxPlayers = 4 });
    }

    public override void OnConnectedToMaster()
    {
        durumyazısı.text = "Sunucuya bağlandı!";
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        durumyazısı.text = "Lobiye girildi, artık oda oluşturabilirsin.";
    }
}
