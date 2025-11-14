using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform cam;      // Main Camera
    public float sensX = 180f; // 마우스 감도 (°/sec)
    public float sensY = 180f;

    float yaw;   // 좌/우 누적 회전
    float pitch; // 위/아래 누적 회전

    void Awake()
    {
        // 혹시 Inspector에서 안 넣었으면 자동으로 찾기
        if (cam == null)
        {
            var c = GetComponentInChildren<Camera>();
            if (c != null) cam = c.transform;
        }
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (cam == null) return; // 안전장치

        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

        // 누적 회전값 업데이트
        yaw += mouseX;
        pitch -= mouseY;

        // 위/아래 각도 제한
        pitch = Mathf.Clamp(pitch, -80f, 80f);

        // 몸통(Yaw) 회전
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        // 카메라(Pitch) 회전
        cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        // ESC로 마우스 잠금 토글
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}


//using UnityEngine;

//public class PlayerLook : MonoBehaviour {
//    public Transform cam;      // Main Camera 넣기
//    public float sensX = 180f; // 마우스 감도 (°/sec)
//    public float sensY = 180f;
//    float pitch; // 위/아래 회전 누적

//    void Start() {
//        Cursor.lockState = CursorLockMode.Locked; // 재생 중 마우스 락
//        Cursor.visible = false;
//    }

//    void Update() {
//        float mouseX = Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
//        float mouseY = Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;

//        // Yaw: 본체 좌/우
//        transform.Rotate(Vector3.up * mouseX);

//        // Pitch: 카메라 위/아래 (각도 제한)
//        pitch -= mouseY;
//        pitch = Mathf.Clamp(pitch, -80f, 80f);
//        cam.localRotation = Quaternion.Euler(pitch, 0f, 0f);

//        // ESC로 마우스 락 해제 (원하면)
//        if (Input.GetKeyDown(KeyCode.Escape)) {
//            Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked)
//                ? CursorLockMode.None : CursorLockMode.Locked;
//            Cursor.visible = (Cursor.lockState != CursorLockMode.Locked);
//        }
//    }
//}