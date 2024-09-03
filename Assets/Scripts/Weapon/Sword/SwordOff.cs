using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOff : MonoBehaviour
{
    [SerializeField] WeaponHold weaponhold;
    // Start is called before the first frame update

    private void Start()
    {
        weaponhold = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponHold>();
    }
    public void OutState()
    {
        weaponhold.Swing = false;
        gameObject.SetActive(false);
    }

}
