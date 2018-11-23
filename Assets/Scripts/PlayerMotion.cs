using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    public float AirSpeed = 1.0f;
    public float Gravity = 20.0f;
    public float JumpSpeed = 9.0f;
    public float Speed = 6.0f;

    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    private Camera mainCamera;

    private Vector3 origin;

    private void Start() {
        mainCamera = Camera.main;
        controller = GetComponent<CharacterController>();
        origin = transform.position;
    }

    private void Update() {
        // Allow normal control on the ground
        if (controller.isGrounded) {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * Speed;

            moveDirection = mainCamera.transform.TransformDirection(moveDirection);
            // Apply jump force
            if (Input.GetButton("Jump")) moveDirection.y = JumpSpeed;
        }

        // Apply gravity
        moveDirection.y = moveDirection.y - Gravity * Time.deltaTime;
        // Move the controller
        controller.Move(moveDirection * Time.deltaTime);

        // DEBUG
        if (Input.GetKey(KeyCode.LeftCommand)) transform.position = origin;
    }
}