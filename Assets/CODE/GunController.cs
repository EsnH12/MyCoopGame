using UnityEngine;
using Photon.Pun;

public class GunController : MonoBehaviourPun
{
    public int MaxMermi = 30;
    public int yedekMermi = 90;
    public int g�ncelMermi;

    public bool MermiDolduruyorMu = false;
    public float mermiDoldurmaS�resi = 2f;

    public Camera playerCamera;
    public float fireRate = 0.2f;
    public float range = 100f;
    public LayerMask hitMask;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        g�ncelMermi = MaxMermi;
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
            if (g�ncelMermi > 0)
            {
                Fire();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }

        // E�er bas�l� tutarak taramal� ate� etmek istiyorsan bu k�s�m kullan�labilir
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            if (g�ncelMermi > 0)
            {
                Fire();
            }
        }
    }

    void Fire()
    {
        g�ncelMermi--;
        Debug.Log("Ate� edildi! Kalan mermi: " + g�ncelMermi);

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
        if (yedekMermi <= 0 || g�ncelMermi == MaxMermi)
            yield break;

        MermiDolduruyorMu = true;
        Debug.Log("�arj�r de�i�tiriliyor...");

        yield return new WaitForSeconds(mermiDoldurmaS�resi);

        int eklenecekMermi = MaxMermi - g�ncelMermi;
        int kullanilacakMermi = Mathf.Min(eklenecekMermi, yedekMermi);

        yedekMermi -= kullanilacakMermi;
        g�ncelMermi += kullanilacakMermi;

        MermiDolduruyorMu = false;

        Debug.Log("Yeniden dolduruldu! Mermi: " + g�ncelMermi + " / " + yedekMermi);
    }
}
