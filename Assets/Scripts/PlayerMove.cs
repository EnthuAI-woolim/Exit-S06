using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 4.5f;
    public float sprintSpeed = 7.0f;
    public float jumpHeight = 1.0f;
    public float gravity = -9.81f;

    CharacterController cc;
    Vector3 velocity;
    bool isGrounded;

    void Awake() => cc = GetComponent<CharacterController>();

    void Update()
    {
        // 땅 체크 (CharacterController의 isGrounded 사용)
        isGrounded = cc.isGrounded;
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // WASD 입력 (Old Input Manager: Horizontal/Vertical)
        float x = Input.GetAxis("Horizontal");   // A/D
        float z = Input.GetAxis("Vertical");     // W/S

        // 달리기(Shift)
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);

        // 점프(Space)
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        // 중력
        velocity.y += gravity * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
