using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform cam;      // Main Camera 넣기
    public float sensX = 180f; // 마우스 감도 (°/sec)
    public float sensY = 180f;
    float pitch; // 위/아래 회전 누적

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 재생 중 마우스 락
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        // Yaw: 본체 좌/우
        transform.Rotate(Vector3.up * mouseX);

        // Pitch: 카메라 위/아래 (각도 제한)
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -80f, 80f);
        cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // ESC로 마우스 락 해제 (원하면)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked)
                ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = (Cursor.lockState != CursorLockMode.Locked);
        }
    }
}
