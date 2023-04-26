using System.Collections.Generic;
using UnityEngine;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror.ObjectsFalling
{
    public class ObjectsFallingController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<PhysicalObjects> PhysicalObjectsCollection { get; set; }

        public override void ActivateInteractiveObject ()
        {
            base.ActivateInteractiveObject();

            foreach (PhysicalObjects physicalObjects in PhysicalObjectsCollection)
            {
                physicalObjects.PushObject();
            }
        }
    }
}