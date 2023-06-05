using System;
using System.Collections;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Sirenix.OdinInspector;
using UnityEngine;

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

        //private bool wasConnectedInfoShown = false;
        //private bool wasDisconnectedInfoShown = false;
        //private bool isReconnectInitialized = false;
        private bool isConnectToTcpServerRunning = false;
        [HideInInspector] public bool justSentMessage = false;

        #endregion

        // Use this for initialization 	
        void Start()
        {
            StartCoroutine(ConnectToTcpServer());
            //StartCoroutine("CheckConnectionStatus", 3.0f);
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

        IEnumerator CheckConnectionStatus(float delay)
        {
            while (true)
            {
                if (socketConnection != null)
                {
                    if (socketConnection.Connected)
                    {
                        //STATUS CONNECTED
                        Debug.Log("<color=green>Socket connection active.<color>");
                    }

                    if (!socketConnection.Connected)
                    {
                        Debug.LogError("<color=red>Socket disconnected. Trying to reconnect.</color>");
                        //STATUS DISCONNECTED
                        //StopCoroutine("ConnectToTcpServer");
                        if (!isConnectToTcpServerRunning)
                        {
                            //	StartCoroutine("ConnectToTcpServer");
                        }
                    }
                }

                yield return new WaitForSeconds(delay);
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
        public void SendMessage()
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

        bool SocketConnected(Socket s)
        {
            bool part1 = s.Poll(1000, SelectMode.SelectRead);
            bool part2 = (s.Available == 0);
            if (part1 && part2)
                return false;
            else
                return true;
        }
        /*
         It works like this:
    
        s.Poll returns true if
            connection is closed, reset, terminated or pending (meaning no active connection)
            connection is active and there is data available for reading
        s.Available returns number of bytes available for reading
        if both are true:
            there is no data available to read so connection is not active
    
         */
    }
}