using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Rigidbody compRb;
    private Animator compAni;
    private Transform transCamera = null;

    private Vector3 velocity;
    private Vector3 rotation;

    private float cameraRotationX;
    private float currentCameraRotationX;
    private float cameraRotationLimit = 85f;

    private const string WALK = "isWalk";

    private void Awake() {
        compRb = GetComponent<Rigidbody>();
        compAni = GetComponent<Animator>();
    }

    public void PosCamera(Transform _pos) {
        transCamera = _pos;
    }

    public void Move(Vector3 Mov) {
        this.velocity = Mov;
    }

    public void Rotate(Vector3 yRot) {
        this.rotation = yRot;
    }

    public void RotateCamera(float zRot) {
        this.cameraRotationX = zRot;
    }

    private void FixedUpdate() {
        if(PanelManager.state != ePanelState.withoutPanel)
            return;

        PerformMovement();
        PerformRotation();
    }

    private void PerformMovement() {
        bool val = velocity != Vector3.zero;

        if(val)
            compRb.MovePosition(compRb.position + velocity * Time.fixedDeltaTime);

        compAni.SetBool(WALK, val);
    }

    private void PerformRotation() {
        compRb.MoveRotation(compRb.rotation * Quaternion.Euler(rotation));

        if(transCamera) {
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            transCamera.localEulerAngles = Vector3.right * currentCameraRotationX;
        }
    }
}