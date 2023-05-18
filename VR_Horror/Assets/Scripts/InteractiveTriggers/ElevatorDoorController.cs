using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class ElevatorDoorController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<DOTweenAnimation> ElevatorAnimationCollection { get; set; }
        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();
                OpenElevatorDoors();
                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }

        [Button]
        private void OpenElevatorDoors ()
        {
            foreach (DOTweenAnimation animation in ElevatorAnimationCollection)
            {
                animation.DOPlay();
            }
        }
    }
}