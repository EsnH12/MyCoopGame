using UnityEngine;
using Photon.Pun;

public class GunController : MonoBehaviourPun
{
    public Camera playerCamera;
    public float fireRate = 0.2f; // Taramalý için atýþ süresi
    public float range = 100f;    // Mermi menzili
    public LayerMask hitMask;    // Ne tür objeleri vurabilir?

    private float nextTimeToFire = 0f;

    void Update()
    {
        if (!photonView.IsMine) return; // Sadece kendi oyuncumuz ateþ edebilir

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            Debug.Log("Vurdum: " + hit.collider.name);

            // Eðer vurduðu nesnede PhotonView varsa ve bu bir oyuncuya aitse
            PhotonView targetPV = hit.collider.GetComponent<PhotonView>();
            if (targetPV != null)
            {
                targetPV.RPC("TakeDamage", RpcTarget.All, 25); // 25 hasar ver
            }
        }
    }
}
