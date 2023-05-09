using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class StatueRotation : MonoBehaviour
    {
        [field: SerializeField]
        private Transform PlayerTransform { get; set; }
        [field: SerializeField]
        private Transform StatueTransform { get; set; }

        [field: SerializeField]
        private float RotationDelay { get; set; }

        protected void Start ()
        {
            StartCoroutine(StatueRotationProcess());
        }

        private IEnumerator StatueRotationProcess ()
        {
            while (true)
            {
                yield return new WaitForSeconds(RotationDelay);

                StatueTransform.DOLookAt(PlayerTransform.position * -1.0f, RotationDelay, AxisConstraint.Y);
            }
        }
    }
}