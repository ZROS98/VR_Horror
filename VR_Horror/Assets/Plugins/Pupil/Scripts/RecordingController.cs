﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PupilLabs
{
    [HelpURL("https://github.com/pupil-labs/hmd-eyes/blob/master/docs/Developer.md#recording-data")]
    public class RecordingController : MonoBehaviour
    {
        public RequestController requestCtrl;

        [Header("Recording Path")]
        public bool useCustomPath;

        [Tooltip("Please enter the session participant code in order to have the session folder named with it")]
        public string sessionParticipantCode = "";

        public GameObject recordingIndicator;
        [SerializeField]
        private string customPath;

        [Header("Controls")]
        [SerializeField]
        private bool recordEyeFrames = true;
        [SerializeField]
        public bool startRecording;
        [SerializeField]
        public bool stopRecording;
        
        public string PlayerName { get; set; } 
        public bool IsReadyToRecord { get; set; }

        public bool IsRecording { get; private set; }

        void OnEnable ()
        {
            if (requestCtrl == null)
            {
                Debug.LogError(
                    "RecordingController is missing the required RequestController reference. Please connect the reference, or the component won't work correctly.");
                enabled = false;
                return;
            }
        }

        void OnDisable ()
        {
            if (IsRecording)
            {
                StopRecording();
            }
        }

        void Update ()
        {
            if (IsReadyToRecord == true && Input.GetKeyDown(KeyCode.R))
            {
                if (IsRecording)
                {
                    stopRecording = true;
                    Debug.Log("<color=red>trying to STOP recording</color>");
                    recordingIndicator.SetActive(false);
                }
                else
                {
                    startRecording = true;
                    Debug.Log("<color=green>trying to START recording</color>");
                    recordingIndicator.SetActive(true);
                }
            }

            if (startRecording)
            {
                startRecording = false;
                StartRecording();
            }

            if (stopRecording)
            {
                stopRecording = false;
                StopRecording();
            }
        }

        public void StartRecording ()
        {
            if (!enabled)
            {
                Debug.LogWarning("Component not enabled");
                return;
            }

            if (!requestCtrl.IsConnected)
            {
                Debug.LogWarning("Not connected");
                return;
            }

            if (IsRecording)
            {
                Debug.Log("Recording is already running.");
                return;
            }

            var path = GetRecordingPath();

            requestCtrl.Send(new Dictionary<string, object>
            {
                {"subject", "recording.should_start"}, {"session_name", path}, {"record_eye", recordEyeFrames}
            });
            IsRecording = true;

            //abort process on disconnecting
            requestCtrl.OnDisconnecting += StopRecording;
        }

        public void StopRecording ()
        {
            if (!IsRecording)
            {
                Debug.Log("Recording is not running, nothing to stop.");
                return;
            }

            requestCtrl.Send(new Dictionary<string, object>
            {
                {"subject", "recording.should_stop"}
            });

            IsRecording = false;

            requestCtrl.OnDisconnecting -= StopRecording;
        }

        public void SetCustomPath (string path)
        {
            useCustomPath = true;
            customPath = path;
        }

        private string GetRecordingPath ()
        {
            string path = "";

            if (useCustomPath)
            {
                path = customPath;
            }
            else
            {
                string date = System.DateTime.Now.ToString("yyyy_MM_dd_HH-mm");

                if (sessionParticipantCode.Length > 0)
                {
                    path = $"{Application.dataPath}/{PlayerName + date + " " + sessionParticipantCode}";
                }
                else
                    path = $"{Application.dataPath}/{PlayerName + date}";

                path = path.Replace("Assets/", ""); //go one folder up
            }

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}