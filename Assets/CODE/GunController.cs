using UnityEngine;
using Photon.Pun;

public class GunController : MonoBehaviourPun
{
    public int MaxMermi = 30;
    public int yedekMermi = 90;
    public int güncelMermi;

    public bool MermiDolduruyorMu = false;
    public float mermiDoldurmaSüresi = 2f;

    public Camera playerCamera;
    public float fireRate = 0.2f;
    public float range = 100f;
    public LayerMask hitMask;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        güncelMermi = MaxMermi;
    }

    void Update()
    {
        if (!photonView.IsMine) return;

        if (MermiDolduruyorMu) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (güncelMermi > 0)
            {
                Fire();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        // Eðer basýlý tutarak taramalý ateþ etmek istiyorsan bu kýsým kullanýlabilir
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            if (güncelMermi > 0)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        güncelMermi--;
        Debug.Log("Ateþ edildi! Kalan mermi: " + güncelMermi);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range, hitMask))
        {
            Debug.Log("Vurdum: " + hit.collider.name);

            PhotonView hedefPV = hit.collider.GetComponent<PhotonView>();
            if (hedefPV != null)
            {
                hedefPV.RPC("TakeDamage", RpcTarget.All, 25);
            }
        }
    }

    System.Collections.IEnumerator Reload()
    {
        if (yedekMermi <= 0 || güncelMermi == MaxMermi)
            yield break;

        MermiDolduruyorMu = true;
        Debug.Log("Þarjör deðiþtiriliyor...");

        yield return new WaitForSeconds(mermiDoldurmaSüresi);

        int eklenecekMermi = MaxMermi - güncelMermi;
        int kullanilacakMermi = Mathf.Min(eklenecekMermi, yedekMermi);

        yedekMermi -= kullanilacakMermi;
        güncelMermi += kullanilacakMermi;

        MermiDolduruyorMu = false;

        Debug.Log("Yeniden dolduruldu! Mermi: " + güncelMermi + " / " + yedekMermi);
    }
}
