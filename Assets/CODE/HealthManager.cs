using UnityEngine;
using Photon.Pun;

public class HealthManager : MonoBehaviourPun
{
    public int maxCan = 100;
    public int mevcutCan;
    void Start()
    {
        mevcutCan = maxCan;
    }

 public void TakeDamage(int damage) 
    {
     if (!photonView.IsMine) return; //Bu obje bize ait deðilse iþlem yapma

        mevcutCan -= damage;
        Debug.Log("Can: " + mevcutCan);
        if (mevcutCan<= 0) 
        {
            Die();
        }
    }
    public void Die() 
    {
        Debug.Log("öldü");
    }
}
