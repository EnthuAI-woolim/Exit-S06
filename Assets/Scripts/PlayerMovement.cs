using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    Vector2 moveInput = Vector2.zero;  // WASD / 조이스틱 입력 벡터
    float jumpInput = 0f;

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // 이 메소드 이름은 Input Actions의 Action 이름 “Move” 에 대응해야 함
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // 이 메소드 이름은 Action 이름 “Jump” 에 대응
    void OnJump(InputValue value)
    {
        // performed 일 때만 처리하고 싶으면 context를 사용한 방식도 가능
        if (value.isPressed && controller.isGrounded)
        {
            // 점프 입력
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        // 수평 이동
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // 중력 & 하강 처리
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // 바닥 고정
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
