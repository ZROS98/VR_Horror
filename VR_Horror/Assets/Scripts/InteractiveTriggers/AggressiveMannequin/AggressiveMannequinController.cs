using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class AggressiveMannequinController : InteractiveTriggerController
    {
        [field: SerializeField]
        private Animator CurrentAnimator { get; set; }
        
        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                CurrentAnimator.enabled = true;

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }
    }
}