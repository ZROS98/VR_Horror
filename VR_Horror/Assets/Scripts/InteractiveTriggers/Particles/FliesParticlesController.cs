using System.Collections;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers.Particles
{
    public class FliesParticlesController : InteractiveTriggerController
    {
        [field: SerializeField]
        private ParticleSystem CurrentParticleSystem { get; set; }

        [field: SerializeField]
        private float ParticleSystemDuration { get; set; }

        private WaitForSeconds ParticleSystemWaitForSeconds { get; set; }

        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                StartCoroutine(ParticleSystemProcess());

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }

        protected override void Initialize ()
        {
            ParticleSystemWaitForSeconds = new WaitForSeconds(ParticleSystemDuration);
        }

        private IEnumerator ParticleSystemProcess ()
        {
            CurrentParticleSystem.Play();

            yield return ParticleSystemWaitForSeconds;

            CurrentParticleSystem.Stop();
        }
    }
}