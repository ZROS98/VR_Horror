using System;
using System.Globalization;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror.Logs
{
    public class DataCollection : MonoBehaviour
    {
        private string FilenameTemplate { get; set; } = "{0}_{1}.csv";
        private string DateFormat { get; set; } = "yyyy.MM.dd_HH.mm.ss";
        private string SavePath { get; set; }
        private string ExaminationDate { get; set; }

        private const char CSV_SEPARATOR = ';';

        protected virtual void Awake ()
        {
            Initialize();
        }

        protected virtual void OnEnable ()
        {
            AttachEvent();
        }

        protected virtual void OnDisable ()
        {
            DetachEvents();
        }

        private void Initialize ()
        {
            ExaminationDate = System.DateTime.Now.ToString(DateFormat);
            SavePath = $"{Directory.GetCurrentDirectory()}/ {GetFilename()}";
        }

        private string GetFilename ()
        {
            return string.Format(FilenameTemplate, Application.productName, ExaminationDate);
        }

        private void OnTriggerActivated (InteractiveTriggerType triggerType)
        {
            UpdateLogs(triggerType);
        }

        [Button]
        private void UpdateLogs (InteractiveTriggerType triggerType = InteractiveTriggerType.NONE)
        {
            string currentTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);

            using (StreamWriter writer = File.Exists(SavePath) ? File.AppendText(SavePath) : File.CreateText(SavePath))
            {
                writer.Write(triggerType);
                writer.Write(CSV_SEPARATOR);
                writer.Write(currentTime);
                writer.Write(Environment.NewLine);
            }
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