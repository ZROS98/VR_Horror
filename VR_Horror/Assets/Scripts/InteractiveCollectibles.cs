using UnityEngine;

namespace VR_Horror
{
    public class InteractiveCollectibles : MonoBehaviour
    {
        public void HandleCollection()
        {
            Debug.Log($"{gameObject.name} was collected");
            gameObject.SetActive(false);
        }
    }
}