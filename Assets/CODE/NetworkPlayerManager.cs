using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class NetworkPlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;    // FPS oyuncu prefab�
    public Transform blueTeam;         // Mavi tak�m spawn noktas�
    public Transform redTeam;          // K�rm�z� tak�m spawn noktas�

    // Odaya ba�ar�l� �ekilde girildi�inde �a�r�l�r
    public override void OnJoinedRoom()
    {
        string team = "Blue"; // Varsay�lan tak�m (ba�ka bilgi yoksa)

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            team = (string)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        }
        else
        {
            Debug.LogWarning("Tak�m bilgisi bulunamad�. Varsay�lan olarak Blue se�ildi.");
        }

        Vector3 spawnPos = (team == "Blue") ? blueTeam.position : redTeam.position;

        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab atanmad�!");
            return;
        }

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
