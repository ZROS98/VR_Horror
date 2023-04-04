using UnityEngine;

public class DEBUG_CharacterController : MonoBehaviour
{
    [field: SerializeField]
    private CharacterController CurrentCharacterController { get; set; }
    [field: SerializeField]
    private Transform PlayerCamera { get; set; }
    [field: SerializeField]
    private Transform Player { get; set; }
    [field: SerializeField]
    private float MoveSpeed { get; set; } = 5.0f;
    [field: SerializeField]
    private float MouseSensitivity { get; set; } = 100.0f;
    
    private float RotationAxisX { get; set; } = 0.0f;
    private float MinAngleValue { get; set; } = -90.0f;
    private float MaxAngleValue { get; set; } = 90.0f;

    protected virtual void Start ()
    {
        LockCursor();
    }

    protected virtual void Update ()
    {
        HandleMovement();
        HandleRotation();
    }

    private void LockCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void HandleMovement ()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = Player.rotation * moveDirection;
        moveDirection *= MoveSpeed;

        CurrentCharacterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation ()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;
        
        RotationAxisX -= mouseY;
        RotationAxisX = Mathf.Clamp(RotationAxisX, MinAngleValue, MaxAngleValue);

        PlayerCamera.localRotation = Quaternion.Euler(RotationAxisX, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX, Space.World);
    }
}