using UnityEngine;

public class CameraTransform : MonoBehaviour
{
    [field: SerializeField] private Camera VRCamera { get; set; }
    [field: SerializeField] private Camera ScreenCamera { get; set; }

    void Update()
    {
        ScreenCamera.transform.SetPositionAndRotation(VRCamera.transform.position, VRCamera.transform.rotation);
    }
}