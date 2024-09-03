using TMPro;
using UnityEngine;

public class FPSGioihan : MonoBehaviour
{
    public int mucFPS;

    public void ChonFPS(int index)
    {
        switch (index)
        {
            case 0:
                Application.targetFrameRate = 0;
                break;
            case 1:
                Application.targetFrameRate = 30;
                break;
            case 2:
                Application.targetFrameRate = 60;
                break;
            case 3:
                Application.targetFrameRate = 120;
                break;
        }
    }
}