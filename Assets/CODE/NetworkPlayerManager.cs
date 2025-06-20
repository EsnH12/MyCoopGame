using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class NetworkPlayerManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;    // FPS oyuncu prefabý
    public Transform blueTeam;         // Mavi takým spawn noktasý
    public Transform redTeam;          // Kýrmýzý takým spawn noktasý

    // Odaya baþarýlý þekilde girildiðinde çaðrýlýr
    public override void OnJoinedRoom()
    {
        string team = "Blue"; // Varsayýlan takým (baþka bilgi yoksa)

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            team = (string)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        }
        else
        {
            Debug.LogWarning("Takým bilgisi bulunamadý. Varsayýlan olarak Blue seçildi.");
        }

        Vector3 spawnPos = (team == "Blue") ? blueTeam.position : redTeam.position;

        if (playerPrefab == null)
        {
            Debug.LogError("Player Prefab atanmadý!");
            return;
        }

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
    }
}
