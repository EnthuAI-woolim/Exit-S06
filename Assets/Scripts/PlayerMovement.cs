using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    Vector2 moveInput = Vector2.zero;  // WASD / ���̽�ƽ �Է� ����
    float jumpInput = 0f;

    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 1.5f;

    private Vector3 velocity;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    // �� �޼ҵ� �̸��� Input Actions�� Action �̸� ��Move�� �� �����ؾ� ��
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // �� �޼ҵ� �̸��� Action �̸� ��Jump�� �� ����
    void OnJump(InputValue value)
    {
        // performed �� ���� ó���ϰ� ������ context�� ����� ��ĵ� ����
        if (value.isPressed && controller.isGrounded)
        {
            // ���� �Է�
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void Update()
    {
        // ���� �̵�
        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // �߷� & �ϰ� ó��
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // �ٴ� ����
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
