using System;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {
    public bool IsControllable = true;

    public Transform Target;

    public float Distance = 8.0f;
    public float XSpeed = 900.0f;
    public float YSpeed = 900.0f;
    public float YMax = 80;
    public float YMin = -80;

    private float rotationXAxis;
    private float rotationYAxis;

    public void SetControllable(bool value) {
        IsControllable = value;
    }

    private void Start() {
        var angles = transform.eulerAngles;
        rotationYAxis = Math.Abs(rotationYAxis) < 0.1 ? angles.y : rotationYAxis;
        rotationXAxis = angles.x;
    }

    private void LateUpdate() {
        var velocityX = 0f;
        var velocityY = 0f;

        if (IsControllable) {
            velocityX = XSpeed * Input.GetAxis("Mouse X") * 0.02f;
            velocityY = YSpeed * Input.GetAxis("Mouse Y") * 0.02f;
        }

        rotationYAxis += velocityX;
        rotationXAxis -= velocityY;

        rotationXAxis = ClampAngle(rotationXAxis, YMin, YMax);

        var toRotation = Quaternion.Euler(rotationXAxis, rotationYAxis, 0);
        var rotation = toRotation;

        var negDistance = new Vector3(0.0f, 0.0f, -Distance);
        var position = rotation * negDistance + Target.position;

        var castPoint = RaycastCamera(position);

        transform.position = castPoint;
        transform.rotation = rotation;
    }

    private static float ClampAngle(float angle, float min, float max) {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    // Updates camera position to avoid clipping through the ground
    private Vector3 RaycastCamera(Vector3 currentPosition) {
        var dir = currentPosition - Target.gameObject.transform.position;
        return Physics.Raycast(Target.transform.position, dir, out var hit, 10f) ? hit.point : currentPosition;
    }
}