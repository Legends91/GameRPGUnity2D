using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeOff : MonoBehaviour
{
    [SerializeField] WeaponHold weaponhold;
    // Start is called before the first frame update

    private void Start()
    {
        weaponhold = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponHold>();
    }
    public void OutState()
    {
        gameObject.SetActive(false);
        weaponhold.Chop = false;
    }
}
