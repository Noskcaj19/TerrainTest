using UnityEngine;

public class PlayerMotion : MonoBehaviour {
    public float AirSpeed = 1.0f;
    public float Gravity = 20.0f;
    public float JumpSpeed = 9.0f;
    public float Speed = 6.0f;
//    public GameObject Target;

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

//        CameraRaycast();

        // DEBUG
        if (Input.GetKey(KeyCode.LeftCommand)) transform.position = origin;
    }

//    private void CameraRaycast() {
//        RaycastHit hit;
////        var layerMask = 0b011111011;
//        var dir = mainCamera.transform.position - gameObject.transform.position;
//
//        if (Physics.Raycast(transform.position, dir, out hit, 10f)) {
//            Debug.DrawRay(transform.position, dir * hit.distance,
//                Color.red);
//            if (hit.collider.gameObject.name != "Sphere") {
//                Target.transform.position = hit.point;
//            }
//            Debug.Log("Did Hit " + hit.collider.gameObject.name);
//        } else {
//            Debug.DrawRay(transform.position, dir * 1000, Color.white);
//            Debug.Log("Did not Hit");
//        }
//    }
}