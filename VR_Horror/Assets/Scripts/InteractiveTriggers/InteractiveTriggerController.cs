using System;
using System.Collections;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class InteractiveTriggerController : MonoBehaviour
    {
        [field: SerializeField]
        public InteractiveTriggerType CurrentInteractiveTriggerType { get; set; }

        [field: SerializeField]
        private float TriggerDelay { get; set; } = 20.0f;

        private bool IsReady { get; set; } = true;
        private WaitForSeconds CurrentWaitForSeconds { get; set; }
        private Coroutine TriggerTimerCoroutine { get; set; }

        public static Action<InteractiveTriggerType> OnTriggerActivated = delegate { };

        public virtual void ActivateInteractiveObject ()
        {
            if (IsReady == false)
            {
                return;
            }

            TriggerTimerCoroutine = StartCoroutine(TriggerTimerProcess());
        }

        protected virtual void Awake ()
        {
            Initialize();
        }

        protected virtual void Initialize ()
        {
            CurrentWaitForSeconds = new WaitForSeconds(TriggerDelay);
        }

        private IEnumerator TriggerTimerProcess ()
        {
            IsReady = false;

            yield return CurrentWaitForSeconds;

            IsReady = true;
            StopCoroutine(TriggerTimerCoroutine);
        }
    }
}