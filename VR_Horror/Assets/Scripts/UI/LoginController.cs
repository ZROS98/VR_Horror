using PupilLabs;
using TMPro;
using UnityEngine;

namespace VR_Horror.UI
{
    public class LoginController : MonoBehaviour
    {
        [field: SerializeField]
        private TMP_InputField CurrentInputField { get; set; }
        [field: SerializeField]
        private RecordingController CurrentRecordingController { get; set; }
        [field: SerializeField]
        private GameObject LoginPanel { get; set; }

        private string PlayerName { get; set; }

        public void SetPlayerName ()
        {
            PlayerName = CurrentInputField.text;
            CurrentRecordingController.PlayerName = PlayerName;
            AddPermissionToRecord();
            
            DeactivateLoginPanel();
        }

        protected void Start ()
        {
            CurrentInputField.ActivateInputField();
        }

        private void DeactivateLoginPanel ()
        {
            LoginPanel.SetActive(false);
        }

        private void AddPermissionToRecord ()
        {
            CurrentRecordingController.IsReadyToRecord = true;
        }
    }
}