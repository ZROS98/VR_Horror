using Sirenix.OdinInspector;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class ShelfPhysics : MonoBehaviour
    {
        [field: SerializeField]
        private Rigidbody CurrentRigidbody { get; set; }

        [field: SerializeField]
        private float ImpulseForce { get; set; }

        [Button]
        public void PushShelf ()
        {
            CurrentRigidbody.AddForce(transform.forward * -1.0f * ImpulseForce);
        }
    }
}