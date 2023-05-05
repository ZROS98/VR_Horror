using UnityEngine;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror
{
    public class RaycastShooter : MonoBehaviour
    {
        [field: SerializeField]
        private float Duration { get; set; } = 5.0f;
        [field: SerializeField]
        private float RayDistance { get; set; } = 2.0f;
        [field: SerializeField]
        private float Timer { get; set; } = 0.0f;
        
        private bool IsActivated { get; set; } = false;

        protected virtual void Update ()
        {
            HandleRaycast();
        }

        private void HandleRaycast ()
        {
            RaycastHit hit;

            Vector3 forward = transform.TransformDirection(Vector3.forward) * RayDistance;
            Debug.DrawRay(transform.position, forward, Color.green);

            if (Physics.Raycast(transform.position, forward, out hit, RayDistance))
            {
                if (hit.collider.gameObject.TryGetComponent(out InteractiveTriggerController interactiveTriggerController))
                {
                    Timer += Time.deltaTime;

                    if (Timer >= Duration && IsActivated == false)
                    {
                        IsActivated = true;
                        interactiveTriggerController.ActivateInteractiveObject();
                    }
                }
                else
                {
                    ResetTimer();
                }
            }
            else
            {
                ResetTimer();
            }
        }

        private void ResetTimer ()
        {
            Timer = 0.0f;
            IsActivated = false;
        }
    }
}