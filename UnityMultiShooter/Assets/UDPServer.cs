using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class UDPServer : MonoBehaviour
{
    private UdpClient udpClient;
    private IPEndPoint endPoint;
    public GameObject player;
    public GameObject player2;
    private Vector3 playerPosition;
    private Vector3 player2Position;
    
    void Start()
    {
        udpClient = new UdpClient(9055);
        endPoint = new IPEndPoint(IPAddress.Any, 0);
        playerPosition = player.transform.position;
        player2Position = player2.transform.position;

        Debug.Log("Waiting for a client...");
        
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        byte[] receivedData = udpClient.EndReceive(result, ref endPoint);

        string receivedMessage = Encoding.ASCII.GetString(receivedData);
        Debug.Log("Message received from " + endPoint.ToString() + ": " + receivedMessage);
        Vector3 currentPosition;
        string positionMessage;
        if (receivedMessage == "Check")
        {
            currentPosition = player2Position;
            positionMessage = "Bpos" + currentPosition.x + "," + currentPosition.y + "," + currentPosition.z;
        } else {
            currentPosition = playerPosition;
            positionMessage = "pos" + currentPosition.x + "," + currentPosition.y + "," + currentPosition.z;
        }

        if (receivedMessage.StartsWith("pos"))
        {
            string[] position = receivedMessage.Substring(3).Split(',');
            currentPosition = new Vector3(float.Parse(position[0]), float.Parse(position[1]), float.Parse(position[2]));
            player2Position = currentPosition;
        }

        // Convert the position string to bytes
        byte[] positionData = Encoding.ASCII.GetBytes(positionMessage);

        // Send the player's position to the sender
        udpClient.Send(positionData, positionData.Length, endPoint);

        // Start listening for the next message
        udpClient.BeginReceive(ReceiveCallback, null);
    }

    void Update()
    {
        playerPosition = player.transform.position;

        player2.transform.position = Vector3.MoveTowards(player2.transform.position, player2Position, 0.01f);
        // Calculate distance between current position and target position
        float distance = Vector3.Distance(player2.transform.position, player2Position);
        // Set animator parameter based on distance
        player2.gameObject.GetComponent<PlayerStats>().animator.SetFloat("Speed", distance);
    }

    private void sendPlayerPosition()
    {
        string message = "pos" + player.transform.position.x + "," + player.transform.position.y + "," + player.transform.position.z;
        byte[] data = Encoding.ASCII.GetBytes(message);
        udpClient.Send(data, data.Length, endPoint);
    }

    void OnApplicationQuit()
    {
        udpClient.Close();
    }
}