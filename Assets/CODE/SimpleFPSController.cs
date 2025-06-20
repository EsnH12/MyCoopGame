using UnityEngine;

public class SimpleFPSController : MonoBehaviour
{
    public float speed = 5f; // Hareket hızı
    public float mouseSensitivity = 2f; // Fare hassasiyeti
    public Transform PlayerCamera; // Oyuncu kamerası

    public Rigidbody rb; // Rigidbody bileşeni

    float verticalRotation = 0; // Kamera yukarı-aşağı rotasyonu

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        PlayerCamera.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = transform.right * h + transform.forward * v;

        // Yeni Unity sürümlerinde linearVelocity kullanımı
        rb.linearVelocity = move * speed + new Vector3(0, rb.linearVelocity.y, 0);
    }
}
