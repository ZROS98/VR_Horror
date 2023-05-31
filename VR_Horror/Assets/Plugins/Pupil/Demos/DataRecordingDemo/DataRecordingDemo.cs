using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace PupilLabs.Demos
{
    public class DataRecordingDemo : MonoBehaviour
    {
        public Text text;
        public RecordingController recorder;

        [Header("Annotations")]
        public AnnotationPublisher annotationPub;
        public Transform head;
        public bool sendHeadAsAnnotation = false;

        void Update()
        {
            bool connected = recorder.requestCtrl.IsConnected;

            UpdateText(connected);

            if (connected && Input.GetKeyDown(KeyCode.A))
            {
                SendSimpleAnnotation("Key A Pressed");
            }

            if (connected && sendHeadAsAnnotation)
            {
                if (Time.frameCount % 10 == 0)
                {
                    SendHeadPosAnnotations(); //limit annotation rate
                }
            }
        }

        void UpdateText(bool connected)
        {
            text.text = connected ? "Connected" : "Not connected";

            if (connected)
            {
                text.text += "\n\nPress R to Start/Stop the recording and A to send an annotation.";

                var status = recorder.IsRecording ? "recording" : "not recording";
                text.text += $"\n\nStatus: {status}";
            }
        }

        void SendHeadPosAnnotations()
        {
            Dictionary<string, string> headData = new Dictionary<string, string>();

            headData["head_world_x"] = head.position.x.ToString(CultureInfo.InvariantCulture);
            headData["head_world_y"] = head.position.y.ToString();
            headData["head_world_z"] = head.position.z.ToString();

            annotationPub.SendAnnotation(label: "head pos", customData: headData);
        }

        void SendSimpleAnnotation(string label)
        {
            annotationPub.SendAnnotation(label: label);
        }
    }
}
