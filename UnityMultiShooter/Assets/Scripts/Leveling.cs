using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leveling : MonoBehaviour
{
    [SerializeField] private GameObject BaseLevel;
    [SerializeField] private List<GameObject> obstacles = new List<GameObject>();
    [SerializeField] private int level = 1;
    [SerializeField] private int maxLevel = 10;
    [SerializeField] private int maxObstacles = 10;
    private int levelHeight = 45;
    private int levelWidth = 45;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelObject = Instantiate(BaseLevel, transform.position, Quaternion.identity);
        levelObject.transform.parent = transform;
        levelObject.transform.localScale = new Vector3(1, 1, 1);
        levelObject.transform.localPosition = new Vector3(0, 0, 0);
        levelObject.transform.localRotation = Quaternion.identity;
        for (int i = 0; i < maxObstacles; i++)
        {
            int obs = Random.Range(0, obstacles.Count);
            GameObject obstacle = Instantiate(obstacles[obs], transform.position, Quaternion.identity);
            obstacle.transform.parent = transform;
            obstacle.transform.localPosition = new Vector3(Random.Range(-levelWidth/2, levelWidth/2), 2, Random.Range(-levelHeight/2, levelHeight/2));
            obstacle.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            if (obs == 0)
            {
                obstacle.transform.localPosition = new Vector3(obstacle.transform.localPosition.x, 2, obstacle.transform.localPosition.z);
            }
            else
            {
                obstacle.transform.localPosition = new Vector3(obstacle.transform.localPosition.x, 1.2f, obstacle.transform.localPosition.z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
