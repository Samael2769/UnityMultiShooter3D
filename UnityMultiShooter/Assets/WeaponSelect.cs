using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] private GameObject weapon;
    [SerializeField] private int weaponIndex = 0;
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<Vector3> weaponOffsets = new List<Vector3>();
    [SerializeField] private List<Vector3> weaponScale = new List<Vector3>();
    private List<Tuple<GameObject, Vector3>> trueWeapons = new List<Tuple<GameObject, Vector3>>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            trueWeapons.Add(new Tuple<GameObject, Vector3>(weapons[i], weaponOffsets[i]));
        }
        weapon = Instantiate(trueWeapons[weaponIndex].Item1, transform.position + trueWeapons[weaponIndex].Item2, transform.rotation);
        weapon.transform.parent = transform;
        weapon.transform.Rotate(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
        weapon.transform.localScale = weaponScale[weaponIndex];
    }

    // Update is called once per frame
    void Update()
    {
        //when scroll wheel is moved down or up it will change the weapon
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            weaponIndex++;
            if (weaponIndex >= trueWeapons.Count)
            {
                weaponIndex = 0;
            }
            Destroy(weapon);
            weapon = Instantiate(trueWeapons[weaponIndex].Item1, transform.position + trueWeapons[weaponIndex].Item2, transform.rotation);
            weapon.transform.parent = transform;
            weapon.transform.Rotate(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
            weapon.transform.localScale = weaponScale[weaponIndex];
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            weaponIndex--;
            if (weaponIndex < 0)
            {
                weaponIndex = trueWeapons.Count - 1;
            }
            Destroy(weapon);
            weapon = Instantiate(trueWeapons[weaponIndex].Item1, transform.position + trueWeapons[weaponIndex].Item2, transform.rotation);
            weapon.transform.parent = transform;
            weapon.transform.Rotate(transform.rotation.x, transform.rotation.y - 90, transform.rotation.z);
            weapon.transform.localScale = weaponScale[weaponIndex];
        }
    }
}
