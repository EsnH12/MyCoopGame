using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [Header("UI Referansları")]
    public InputField roomInput;              // Oda ismi yazılacak yer
    public Text durumyazısı;                  // Durum yazısı
    public GameObject teamSelectionPanel;     // Takım seçimi paneli

    void Start()
    {
        // Sunucuya bağlan
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings(); // Photon'a bağlan
            durumyazısı.text = "Sunucuya bağlanılıyor...";
        }

        PhotonNetwork.AutomaticallySyncScene = true;
        // Master Client sahne değiştirirse diğer oyuncular da aynı sahneye geçer
    }

    // Oda oluşturma
    public void CreateRoom()
    {
        string roomname = roomInput.text;

        if (roomname != "")
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;

            PhotonNetwork.CreateRoom(roomname, options);
            durumyazısı.text = "Oda oluşturuluyor...";
        }
        else
        {
            durumyazısı.text = "Lütfen oda ismi yaz!";
        }
    }

    // Rastgele odaya katıl
    public void JoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
        durumyazısı.text = "Rastgele odaya katılınıyor...";
    }

    // Oda oluşturulunca çağrılır
    public override void OnCreatedRoom()
    {
        durumyazısı.text = " Oda kuruldu!";
    }

    // Odaya girince çağrılır
    public override void OnJoinedRoom()
    {
        durumyazısı.text = " Odaya girildi!";
        teamSelectionPanel.SetActive(true); // Takım seçimi panelini aç
    }

    // Rastgele oda bulunamadığında çağrılır
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        durumyazısı.text = " Oda bulunamadı, yeni bir oda oluşturuluyor...";
        string randomRoomName = "oda" + Random.Range(0, 10000);
        PhotonNetwork.CreateRoom(randomRoomName, new RoomOptions { MaxPlayers = 4 });
    }

    // Master server'a bağlandığında çalışır (opsiyonel ama yararlı)
    public override void OnConnectedToMaster()
    {
        durumyazısı.text = "Sunucuya bağlandı!";
        PhotonNetwork.JoinLobby(); // Lobiye gir
    }

    public override void OnJoinedLobby()
    {
        durumyazısı.text = "Lobiye girildi, artık oda oluşturabilirsin.";
    }
}
