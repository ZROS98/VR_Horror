using System.Collections.Generic;
using UnityEngine;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror.ObjectsFalling
{
    public class ObjectsFallingController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<PhysicalObject> PhysicalObjectsCollection { get; set; }

        public override void ActivateInteractiveObject ()
        {
            base.ActivateInteractiveObject();

            foreach (PhysicalObject physicalObjects in PhysicalObjectsCollection)
            {
                physicalObjects.PushObject();
            }
        }
    }
}