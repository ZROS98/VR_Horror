using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace VR_Horror.InteractiveTriggers
{
    public class MannequinInFireController : InteractiveTriggerController
    {
        [field: SerializeField]
        private List<ParticleSystem> FireParticleSystemCollection { get; set; }

        private bool WasActivated { get; set; } = false;

        public override void ActivateInteractiveObject ()
        {
            if (WasActivated == false)
            {
                base.ActivateInteractiveObject();

                ActivateParticleSystem();

                WasActivated = true;
                OnTriggerActivated(CurrentInteractiveTriggerType);
            }
        }

        [Button]
        private void ActivateParticleSystem ()
        {
            StartCoroutine(ActivateParticleSystemProcess());
        }

        private IEnumerator ActivateParticleSystemProcess ()
        {
            List<int> indexCollection = new List<int>();

            while (indexCollection.Count < FireParticleSystemCollection.Count)
            {
                int randomIndex = Random.Range(0, FireParticleSystemCollection.Count);

                if (indexCollection.Contains(randomIndex) == false)
                {
                    indexCollection.Add(randomIndex);
                }
            }

            for (int i = 0; i < FireParticleSystemCollection.Count; i++)
            {
                yield return new WaitForSeconds(0.2f);

                if (FireParticleSystemCollection[indexCollection[i]].isPlaying == false)
                {
                    FireParticleSystemCollection[indexCollection[i]].Play();
                }
            }
        }
    }
}