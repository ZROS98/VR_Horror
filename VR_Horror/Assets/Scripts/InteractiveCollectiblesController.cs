using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VR_Horror
{
    public class InteractiveCollectiblesController : MonoBehaviour
    {
        [field: SerializeField]
        private List<InteractiveCollectibles> InteractiveCollectiblesCollection { get; set; }
        [field: SerializeField]
        private TMP_Text CounterText { get; set; }

        private int CardCount { get; set; } = 0;
        
        public void CollectObject( InteractiveCollectibles interactiveCollectibles)
        {
            interactiveCollectibles.HandleCollection();
            UpdateCounter();
        }

        private void UpdateCounter()
        {
            CardCount++;
            string counterText = $"Cards collected: {CardCount}/{InteractiveCollectiblesCollection.Count -1}";
            CounterText.text = counterText;
        }
    }
}