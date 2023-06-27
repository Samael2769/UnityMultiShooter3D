using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPClient : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint serverEndPoint;
    public GameObject player;
    public GameObject player2;
    private Vector3 player2Position;
    private Vector3 playerPosition;

    void Start()
    {
        udpClient = new UdpClient();

        serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9055);
        
        string welcome = "Check";
        Debug.Log("Sending message to " + serverEndPoint + ": " + welcome);
        byte[] welcomeData = Encoding.ASCII.GetBytes(welcome);

        udpClient.Send(welcomeData, welcomeData.Length, serverEndPoint);

        udpClient.BeginReceive(ReceiveCallback, null);
        player2Position = new Vector3(0, 0, 0);
        playerPosition = new Vector3(-1, -1, -1);
    }

    void Update()
    {

        string message = "pos" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z;
        Debug.Log("Sending message to " + serverEndPoint + ": " + message);
        byte[] data = Encoding.ASCII.GetBytes(message);
        udpClient.Send(data, data.Length, serverEndPoint);



        player2.transform.position = Vector3.MoveTowards(player2.transform.position, player2Position, 0.01f);
        // Calculate distance between current position and target position
        float distance = Vector3.Distance(player2.transform.position, player2Position);
        // Set animator parameter based on distance
        player2.gameObject.GetComponent<PlayerStats>().animator.SetFloat("Speed", distance);
        if (playerPosition.x != -1 && playerPosition.y != -1 && playerPosition.z != -1) {
            Debug.Log("Player position received: " + playerPosition);
            player.transform.position = playerPosition;
            playerPosition = new Vector3(-1, -1, -1);
        }
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref serverEndPoint);

        string receivedMessage = Encoding.ASCII.GetString(receivedData);
        Debug.Log("Message received from " + serverEndPoint.ToString() + ": " + receivedMessage);
        if (receivedMessage.StartsWith("pos"))
        {
            receivedMessage = receivedMessage.Substring(3);
            string[] positionData = receivedMessage.Split(',');
            player2Position = new Vector3(float.Parse(positionData[0]), float.Parse(positionData[1]), float.Parse(positionData[2]));
        } else if (receivedMessage.StartsWith("Bpos")) {
            receivedMessage = receivedMessage.Substring(4);
            string[] positionData = receivedMessage.Split(',');
            playerPosition = new Vector3(float.Parse(positionData[0]), float.Parse(positionData[1]), float.Parse(positionData[2]));
        }

        // Start listening for the next message
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}