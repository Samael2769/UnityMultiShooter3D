using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector3 bulletSpawnOffset;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shoot");
            GameObject bullet = Instantiate(bulletPrefab, transform.position + bulletSpawnOffset, transform.rotation);
            bullet.transform.Rotate(transform.rotation.x, transform.rotation.y + 90, 90);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 1000f);
        }
    }
}
