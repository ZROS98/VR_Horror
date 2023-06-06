using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;
using VR_Horror.InteractiveTriggers;

namespace VR_Horror
{
    public class TCPTriggerSender : MonoBehaviour
    {
        public string LabViewServerIP = "192.168.50.172"; //100.1.1.1
        public int LabViewServerPort = 5000; //5000
        public string triggerMessageToBeSent = ""; //15555

        #region private members

        private TcpClient socketConnection;

        private Thread clientReceiveThread;
        private bool isConnectToTcpServerRunning = false;
        [HideInInspector] public bool justSentMessage = false;

        #endregion

        // Use this for initialization 	
        void Start()
        {
            StartCoroutine(ConnectToTcpServer());
        }

        /// <summary> 	
        /// Setup socket connection. 	
        /// </summary> 	
        IEnumerator ConnectToTcpServer()
        {
            while (socketConnection == null || (socketConnection != null && !socketConnection.Connected))
            {
                isConnectToTcpServerRunning = true;
                if (socketConnection == null)
                {
                    try
                    {
                        clientReceiveThread = new Thread(new ThreadStart(ListenForData));
                        clientReceiveThread.IsBackground = true;
                        clientReceiveThread.Start();
                    }
                    catch (Exception e)
                    {
                        Debug.Log("On client connect exception " + e);
                    }
                }

                if (socketConnection != null && !socketConnection.Connected)
                {
                    Debug.Log("TRYING TO RECONNECT");
                    socketConnection.Connect(LabViewServerIP, LabViewServerPort);
                }

                if (socketConnection != null && socketConnection.Connected)
                {
                    Debug.Log("<color=green>Socket connection with LabView established.<color>");
                    isConnectToTcpServerRunning = false;

                    StopCoroutine("ConnectToTcpServer");
                }

                yield return new WaitForSeconds(0.3f);
            }
        }

        /// <summary> 	
        /// Runs in background clientReceiveThread; Listens for incomming data. 	
        /// </summary>     
        private void ListenForData()
        {
            try
            {
                socketConnection = new TcpClient(LabViewServerIP, LabViewServerPort); //("localhost", 8052);
                bool wasConnectionErrorShown = false;
                Byte[] bytes = new Byte[1024];
                while (true)
                {
                    // Get a stream object for reading 		
                    try
                    {
                        using (NetworkStream stream = socketConnection.GetStream())
                        {
                            int length;
                            // Read incomming stream into byte arrary. 					
                            while ((length = stream.Read(bytes, 0, bytes.Length)) != 0)
                            {
                                var incommingData = new byte[length];
                                Array.Copy(bytes, 0, incommingData, 0, length);
                                // Convert byte array to string message. 						
                                string serverMessage = Encoding.ASCII.GetString(incommingData);
                                Debug.Log("server message received as: " + serverMessage);
                                wasConnectionErrorShown = false;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        if (!wasConnectionErrorShown)
                        {
                            Debug.LogError(
                                $"<color=red>No LabView connection. </color>Is LabView runtime running? If yes - for each Unity runtime session the LabView runtime should be reinitialized. Click here for the error details: \n{e}, {e.StackTrace}");
                            wasConnectionErrorShown = true;
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                Debug.LogError(
                    $"<color=red>No LabView connection. </color>Is LabView runtime running? If yes - for each Unity runtime session the LabView runtime should be reinitialized. Click here for the error details: \n{socketException}, {socketException.StackTrace}");
            }
        }


        /// <summary> 	
        /// Send message to server using socket connection. 	
        /// </summary>
        [Button]
        public void SendMessage(string triggerValue)
        {
            justSentMessage = false;
            if (socketConnection == null)
            {
                return;
            }

            try
            {
                // Get a stream object for writing. 			
                NetworkStream stream = socketConnection.GetStream();

                if (stream.CanWrite)
                {
                    triggerMessageToBeSent = triggerValue;
                    string clientMessage = triggerMessageToBeSent; //"This is a message from one of your clients."; //43
                    // Convert string message to byte array.                 
                    byte[] clientMessageAsByteArray = Encoding.ASCII.GetBytes(clientMessage);
                    // Write byte array to socketConnection stream.                 
                    stream.Write(clientMessageAsByteArray, 0, clientMessageAsByteArray.Length);
                    Debug.Log("<b><color=#46CA3A>SUCCESS:</color></b><color=green> trigger sent to LabView.</color>");
                    justSentMessage = true;
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
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
            SendMessage(triggerTypeValue.ToString());
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