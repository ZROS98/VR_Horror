using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class FloorPhysics : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<Rigidbody> FloorRigidbodiesCollection { get; set; }

        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();
                StartCoroutine(CollapseFloreProcess());
            }
        }

        [Button]
        private void CollapseFloreRandomly ()
        {
            StartCoroutine(CollapseFloreRandomlyProcess());
        }

        private IEnumerator CollapseFloreRandomlyProcess ()
        {
            for (int i = 0; i < FloorRigidbodiesCollection.Count; i++)
            {
                yield return new WaitForSeconds(0.1f);

                int randomIndex = Random.Range(0, FloorRigidbodiesCollection.Count);

                if (FloorRigidbodiesCollection[randomIndex].useGravity == false)
                {
                    FloorRigidbodiesCollection[randomIndex].useGravity = true;
                }
            }
        }

        [Button]
        private void CollapseFlore ()
        {
            StartCoroutine(CollapseFloreProcess());
        }

        private IEnumerator CollapseFloreProcess ()
        {
            foreach (Rigidbody currentRigidbody in FloorRigidbodiesCollection)
            {
                yield return new WaitForSeconds(0.1f);

                currentRigidbody.useGravity = true;
                currentRigidbody.isKinematic = false;
            }
        }
    }
}