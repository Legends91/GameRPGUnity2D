using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public TextMeshProUGUI FpsText;
    private float tansuatcapnhat = 1f;
    private float thoigian;
    private float soluongkhunghinh;
    void Update()
    {
        thoigian += Time.deltaTime;
        soluongkhunghinh++;
        if(thoigian >= tansuatcapnhat)
        {
            int tocdokhunghinh = Mathf.RoundToInt(soluongkhunghinh / thoigian); //Lam tron so
            FpsText.text = tocdokhunghinh.ToString() + " " + "FPS"; //Hien thi
            thoigian -= tansuatcapnhat; //dat lai
            soluongkhunghinh = 0;
        }
    }
}
