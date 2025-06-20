using UnityEngine;
using Photon.Pun;
using ExitGames.Client.Photon;

public class TeamSelection : MonoBehaviourPunCallbacks
{
    public void BlueTeam()
    {
        SetPlayerTeam("Blue");
        PhotonNetwork.LoadLevel("GameScene");
    }

    public void RedTeam()
    {
        SetPlayerTeam("Red");
        PhotonNetwork.LoadLevel("GameScene");
    }

    void SetPlayerTeam(string teamName)
    {
        ExitGames.Client.Photon.Hashtable props = new ExitGames.Client.Photon.Hashtable
        {
            { "Team", teamName }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        Debug.Log("Takým ayarlandý: " + teamName);
    }
}
