using UnityEngine;
using Photon.Pun;

public class Spawn : MonoBehaviour
{
    public GameObject playerPrefab;

    void Start()
    {
        Vector3 rastgelePozisyon = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));

        // Oyuncuyu sadece kendi i�in do�urur
        PhotonNetwork.Instantiate(playerPrefab.name, rastgelePozisyon, Quaternion.identity);
    }
}
