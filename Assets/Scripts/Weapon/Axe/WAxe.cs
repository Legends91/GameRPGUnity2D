using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WAxe : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] WeaponHold weaponhold;
    [SerializeField] Animator anyAxe;
    [SerializeField] public GameObject weapon;


    // Start is called before the first frame update
    void Start()
    {
        weaponhold = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponHold>();
        weaponhold.CheckActiveWeapon();
        weaponhold.Axe = true;

    }
    private void Update()
    {

        if (weaponhold.xInputP != 0 || weaponhold.yInputP != 0)
        {
            anyAxe.SetFloat("X", weaponhold.xInputP);
            anyAxe.SetFloat("Y", weaponhold.yInputP);
        }

        AxeChop();
        anyAxe.SetBool("Chop", weaponhold.Chop);
    }

    public void AxeChop()
    {
        if (weaponhold.Chop == true)
        {
            weapon.SetActive(true);
        }
    }

}
