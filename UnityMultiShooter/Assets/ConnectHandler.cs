using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectHandler : MonoBehaviour
{
    [SerializeField] private bool isServer = true;
    [SerializeField] private GameObject Server;
    [SerializeField] private GameObject Client;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Player2;
    // Start is called before the first frame update
    void Start()
    {
        GameObject handler;
        if (isServer) {
            handler = Instantiate(Server);
            handler.GetComponent<UDPServer>().player = Player;
            handler.GetComponent<UDPServer>().player2 = Player2;
        } else {
            handler = Instantiate(Client);
            handler.GetComponent<UDPClient>().player = Player;
            handler.GetComponent<UDPClient>().player2 = Player2;
        }
        handler.transform.parent = transform;
    }
}
