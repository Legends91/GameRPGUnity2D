using Unity.VisualScripting;
using UnityEngine;

public class WSword : MonoBehaviour
{
    [Header("------ Khai báo ------")]
    [SerializeField] WeaponHold weaponhold;
    [SerializeField] Animator anySword;
    [SerializeField] public GameObject weapon;

    
    // Start is called before the first frame update
    void Start()
    {
        weaponhold = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponHold>();
        weaponhold.CheckActiveWeapon();
        weaponhold.Sword = true;
        
    }
    private void Update()
    {

        if (weaponhold.xInputP != 0 || weaponhold.yInputP != 0)
        {
            anySword.SetFloat("X", weaponhold.xInputP);
            anySword.SetFloat("Y", weaponhold.yInputP);
        }
        
        SwordSwing();
        anySword.SetBool("Swing", weaponhold.Swing);
    }

    public void SwordSwing()
    {
        if (weaponhold.Swing == true)
        {
            weapon.SetActive(true);
        }
    }
}
