using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [Header("이동 설정")]
    public float moveSpeed = 4.5f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 1.0f;

    [Header("물리 설정")]
    public float gravity = -9.81f;

    private CharacterController cc;
    private Vector3 velocity;
    private bool isGrounded;

    void Awake()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 땅 체크
        isGrounded = cc.isGrounded;
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;  // 살짝 바닥으로 눌러 붙이기
        }

        // WASD 입력
        float x = Input.GetAxis("Horizontal"); // A/D
        float z = Input.GetAxis("Vertical");   // W/S

        // Shift 달리기
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        // 평면 이동 방향
        Vector3 inputDir = new Vector3(x, 0f, z);
        Vector3 move = transform.TransformDirection(inputDir);
        move.y = 0f; // 혹시 모를 Y 성분 제거

        cc.Move(move * speed * Time.deltaTime);

        // 점프
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 중력
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}


//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 4.5f;
//    public float sprintSpeed = 7.0f;
//    public float jumpHeight = 1.0f;
//    public float gravity = -9.81f;

//    CharacterController cc;
//    Vector3 velocity;
//    bool isGrounded;

//    void Awake() => cc = GetComponent<CharacterController>();

//    void Update()
//    {
//        // 땅 체크 (CharacterController의 isGrounded 사용)
//        isGrounded = cc.isGrounded;
//        if (isGrounded && velocity.y < 0) velocity.y = -2f;

//        // WASD 입력 (Old Input Manager: Horizontal/Vertical)
//        float x = Input.GetAxis("Horizontal");   // A/D
//        float z = Input.GetAxis("Vertical");     // W/S

//        // 달리기(Shift)
//        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

//        Vector3 inputDir = new Vector3(x, 0f, z);
//        Vector3 move = transform.TransformDirection(inputDir);
//        move.y = 0f;

//        // Vector3 move = transform.right * x + transform.forward * z;
//        cc.Move(move * speed * Time.deltaTime);

//        // 점프(Space)
//        if (Input.GetButtonDown("Jump") && isGrounded)
//            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

//        // 중력
//        velocity.y += gravity * Time.deltaTime;
//        cc.Move(velocity * Time.deltaTime);
//    }
//}


//using UnityEngine;

//[RequireComponent(typeof(CharacterController))]
//public class PlayerMove : MonoBehaviour
//{
//    public float moveSpeed = 4.5f;
//    public float sprintSpeed = 7.0f;
//    public float jumpHeight = 1.0f;
//    public float gravity = -9.81f;

//    CharacterController cc;
//    Vector3 velocity;
//    bool isGrounded;

//    void Awake()
//    {
//        cc = GetComponent<CharacterController>();
//    }

//    void Update()
//    {
//        // 땅 체크
//        isGrounded = cc.isGrounded;
//        if (isGrounded && velocity.y < 0f)
//            velocity.y = -2f;   // 바닥에 붙게 약간만 눌러줌

//        // 입력
//        float x = Input.GetAxis("Horizontal");
//        float z = Input.GetAxis("Vertical");

//        // 평면 입력 벡터 (y=0)
//        Vector3 inputDir = new Vector3(x, 0f, z);

//        // Shift 달리기
//        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

//        // 플레이어 기준 방향으로 변환 (회전 반영)
//        Vector3 move = transform.TransformDirection(inputDir);
//        move.y = 0f; // 혹시 모를 y 성분 제거

//        cc.Move(move * speed * Time.deltaTime);

//        // 점프
//        if (Input.GetButtonDown("Jump") && isGrounded)
//            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

//        // 중력
//        velocity.y += gravity * Time.deltaTime;
//        cc.Move(velocity * Time.deltaTime);
//    }
//}
