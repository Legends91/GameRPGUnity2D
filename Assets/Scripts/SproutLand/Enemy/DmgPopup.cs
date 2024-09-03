using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgPopup : MonoBehaviour
{
    public GameObject damagePopupPrefab;
    int damageAmount;

    private void Update()
    {
        TakeDamage();
    }

    public void TakeDamage()
    {

        ShowDamagePopup();
    }

    private void ShowDamagePopup()
    {
        Vector3 popupPosition = transform.position + new Vector3(0, 2, 0); // Adjust the position as needed
        GameObject popup = Instantiate(damagePopupPrefab, popupPosition, Quaternion.identity);
        popup.GetComponent<EnemyHealth>().Setup(damageAmount);
    }
}
