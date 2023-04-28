using System.Collections.Generic;
using UnityEngine;
using VR_Horror.ObjectsFalling;

namespace VR_Horror.InteractiveTriggers
{
    public class ObjectsFallingController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<PhysicalObject> PhysicalObjectsCollection { get; set; }

        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                foreach (PhysicalObject physicalObjects in PhysicalObjectsCollection)
                {
                    physicalObjects.PushObject();
                }

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }
    }
}