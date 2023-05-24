using System.Collections.Generic;
using UnityEngine;

namespace VR_Horror
{
    public class InteractiveCollectiblesController : MonoBehaviour
    {
        [field: SerializeField]
        private List<InteractiveCollectibles> InteractiveCollectiblesCollection { get; set; }

        public void CollectObject( InteractiveCollectibles interactiveCollectibles)
        {
            interactiveCollectibles.HandleCollection();
        }
    }
}