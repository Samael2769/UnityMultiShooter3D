using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint serverEndPoint;

    void Start()
    {
        udpClient = new UdpClient();

        serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        
        string welcome = "Hello, are you there?";
        Debug.Log("Sending message to " + serverEndPoint + ": " + welcome);
        byte[] welcomeData = Encoding.ASCII.GetBytes(welcome);

        udpClient.Send(welcomeData, welcomeData.Length, serverEndPoint);

        udpClient.BeginReceive(ReceiveCallback, null);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string message = "Hello, are you there?";
            Debug.Log("Sending message to " + serverEndPoint + ": " + message);
            byte[] data = Encoding.ASCII.GetBytes(message);

            udpClient.Send(data, data.Length, serverEndPoint);
        }
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref serverEndPoint);

        string receivedMessage = Encoding.ASCII.GetString(receivedData);
        Debug.Log("Message received from " + serverEndPoint.ToString() + ": " + receivedMessage);

        // Start listening for the next message
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}