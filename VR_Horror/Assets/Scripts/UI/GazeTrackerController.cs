using UnityEngine;

namespace VR_Horror.UI
{
    public class GazeTrackerController : MonoBehaviour
    {
        [field: SerializeField] 
        private MeshRenderer RaycastHitMarker { get; set; }
        [field: SerializeField] 
        private MeshRenderer RawGazeDirectionHitMarker { get; set; }

        public void HandleMeshRenderVisibility(bool isEnable)
        {
            RaycastHitMarker.enabled = isEnable;
            RawGazeDirectionHitMarker.enabled = isEnable;
        }
    }
}