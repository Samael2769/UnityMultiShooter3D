using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPServer : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    
    void Start()
    {
        udpClient = new UdpClient(9050);
        endPoint = new IPEndPoint(IPAddress.Any, 0);

        Debug.Log("Waiting for a client...");
        
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref endPoint);

        string receivedMessage = Encoding.ASCII.GetString(receivedData);
        Debug.Log("Message received from " + endPoint.ToString() + ": " + receivedMessage);

        string welcome = "Welcome to my test server";
        byte[] welcomeData = Encoding.ASCII.GetBytes(welcome);
        udpClient.Send(welcomeData, welcomeData.Length, endPoint);

        // Start listening for the next message
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}