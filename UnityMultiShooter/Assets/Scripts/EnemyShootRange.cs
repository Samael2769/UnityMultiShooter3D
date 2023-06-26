using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootRange : MonoBehaviour
{
    public int range = 10;
    public int damage = 1;
    public float fireRate = 1f;
    public float nextFire = 0f;
    public GameObject bulletPrefab;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFire && Vector3.Distance(transform.position, player.position) < range)
        {
            nextFire = Time.time + fireRate;
            RotateTowards(player);
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 10;
        bullet.transform.Rotate(0, transform.rotation.y + 90, 90);
        Destroy(bullet, 2f);
    }

    void RotateTowards(Transform player)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 100f);
    }
}
