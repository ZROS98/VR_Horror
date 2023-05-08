using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class ToiletDoorTriggerController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<DOTweenAnimation> DOTAnimationCollection { get; set; }

        private bool WasActivated { get; set; }

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                ActivateToiletDoorAnimation();

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }

        private void ActivateToiletDoorAnimation ()
        {
            foreach (DOTweenAnimation doTweenAnimation in DOTAnimationCollection)
            {
                doTweenAnimation.DOPlay();
            }
        }
    }
}