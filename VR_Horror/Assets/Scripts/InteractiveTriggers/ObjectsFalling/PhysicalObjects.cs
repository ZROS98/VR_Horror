using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VR_Horror.ObjectsFalling
{
    public class PhysicalObjects : MonoBehaviour
    {
        [field: SerializeField]
        private float MinImpulseForce { get; set; }
        [field: SerializeField]
        private float MaxImpulseForce { get; set; }
        [field: SerializeField]
        private Rigidbody CurrentRigidbody { get; set; }

        private WaitForSeconds CurrentWaitForSeconds { get; set; }
        private float TimeToPushObject { get; set; } = 3.0f;

        protected virtual void Start ()
        {
            Initialize();
            //StartCoroutine(TimerToPushObjectsProcess());
        }

        [Button]
        public void PushObject ()
        {
            CurrentRigidbody.AddForce((transform.forward * -1.0f) * RandomizeImpulseForce(), ForceMode.Impulse);
        }

        private float RandomizeImpulseForce ()
        {
            return Random.Range(MinImpulseForce, MaxImpulseForce);
        }

        private void Initialize ()
        {
            CurrentWaitForSeconds = new WaitForSeconds(TimeToPushObject);
        }

        private IEnumerator TimerToPushObjectsProcess ()
        {
            yield return CurrentWaitForSeconds;

            PushObject();
        }
    }
}