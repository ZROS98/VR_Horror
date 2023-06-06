using System.Collections.Generic;
using UnityEngine;
using PupilLabs;
using Sirenix.OdinInspector;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror
{
    public class PupilEyetrackerTriggersSender : MonoBehaviour
    {
        [Tooltip("Monobehaviour performing sending triggers to the eyetracker")]
        public AnnotationPublisher annotationPublisher;
        public string labelForTrigger;

        [Button]
        public void SendTriggerAnnotation(string triggerValue)
        {
            Dictionary<string, string> _customDataTrigger = new Dictionary<string, string>();
            _customDataTrigger[labelForTrigger + "trigger"] = triggerValue;
            
            annotationPublisher.SendAnnotation(labelForTrigger, annotationPublisher.timeSync.ConvertToPupilTime(Time.realtimeSinceStartup), 0.0f, _customDataTrigger);
            
            Debug.Log(triggerValue);
        }
        
        protected virtual void OnEnable ()
        {
            AttachEvent();
        }

        protected virtual void OnDisable ()
        {
            DetachEvents();
        }
        
        private void OnTriggerActivated (InteractiveTriggerType triggerType)
        {
            int triggerTypeValue = (int)triggerType;
            SendTriggerAnnotation(triggerTypeValue.ToString());
        }
        
        private void AttachEvent ()
        {
            InteractiveTriggerController.OnTriggerActivated += OnTriggerActivated;
        }

        private void DetachEvents ()
        {
            InteractiveTriggerController.OnTriggerActivated -= OnTriggerActivated;
        }
    }
}