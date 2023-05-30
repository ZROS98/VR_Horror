using System.Collections.Generic;
using UnityEngine;
using PupilLabs;

namespace VR_Horror
{
    public class PupilEyetrackerTriggersSender : MonoBehaviour
    {
        [Tooltip("Monobehaviour performing sending triggers to the eyetracker")]
        public AnnotationPublisher annotationPublisher;
        public string labelForTrigger;

        public void SendTriggerAnnotation(string _triggerValue)
        {
            Dictionary<string, string> _customDataTrigger = new Dictionary<string, string>();
            _customDataTrigger[labelForTrigger + "trigger"] = _triggerValue;
            //Debug.Log("<color=yellow>_customDataTrigger[labelForTrigger + 'trigger']: "+ _customDataTrigger[labelForTrigger + "trigger"] + "</color>");
            annotationPublisher.SendAnnotation(labelForTrigger, annotationPublisher.timeSync.ConvertToPupilTime(Time.realtimeSinceStartup), 0.0f, _customDataTrigger);
        }
        public void SendTriggerAnnotationWithDuration(string _triggerValue, float duration)
        {
            Dictionary<string, string> _customDataTrigger = new Dictionary<string, string>();
            _customDataTrigger[labelForTrigger + "trigger"] = _triggerValue;
            Debug.Log("<color=yellow>_customDataTrigger[labelForTrigger + 'trigger']: " + _customDataTrigger[labelForTrigger + "trigger"] + "</color>");
            annotationPublisher.SendAnnotation(labelForTrigger, annotationPublisher.timeSync.ConvertToPupilTime(Time.realtimeSinceStartup), duration, _customDataTrigger);
        }

    }
}