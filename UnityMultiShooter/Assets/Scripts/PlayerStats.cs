using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int health = 100;
    public int armor = 0;
    [SerializeField] private Animator animator;

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Life", health);
        if ((health <= 0) && (armor <= 0))
        {
            Destroy(gameObject);
        }
    }
}
