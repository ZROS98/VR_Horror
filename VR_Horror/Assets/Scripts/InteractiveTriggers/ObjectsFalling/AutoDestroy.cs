using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class AutoDestroy : MonoBehaviour
    {
        [field: SerializeField]
        private Rigidbody CurrentRigidbody { get; set; }
        
        [field: SerializeField]
        private int TargetLayer { get; set; }
        private void OnCollisionEnter (Collision other)
        {
            if (other.gameObject.layer == TargetLayer)
            {
                Destroy(gameObject);
            }
        }
    }
}