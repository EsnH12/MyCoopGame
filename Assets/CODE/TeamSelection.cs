using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TeamSelection : MonoBehaviourPunCallbacks
{
    
public void blueTeam() 
    {
    PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "Team" , "blue"} });
        Debug.Log("mavi takýma katýldýn");
    }

    public void redTeam() 
    {
        PhotonNetwork.LocalPlayer.SetCustomProperties(new ExitGames.Client.Photon.Hashtable() { { "team", "red" } });
        Debug.Log("kýrmýzý takýma katidýn");
    }
}

