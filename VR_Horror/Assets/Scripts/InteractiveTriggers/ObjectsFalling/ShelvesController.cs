using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class ShelvesController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<ShelfPhysics> ShelfPhysicsCollection { get; set; }

        [field: SerializeField]
        private float ShelfPushDelay { get; set; }

        private bool WasActivated { get; set; }

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                PushShelves();

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }

        private void PushShelves ()
        {
            ShelfPhysicsCollection[0].PushShelf();
            StartCoroutine(PushShelfProcess(ShelfPhysicsCollection[1]));
        }

        private IEnumerator PushShelfProcess (ShelfPhysics shelfPhysics)
        {
            yield return new WaitForSeconds(ShelfPushDelay);

            shelfPhysics.PushShelf();
        }
    }
}