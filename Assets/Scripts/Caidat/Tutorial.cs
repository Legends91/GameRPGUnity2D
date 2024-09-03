using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public GameObject PanelMove;
    public GameObject PanelInteract;
    public GameObject PanelAttack;
    public GameObject PanelNPC;
    public void SkipPanelMove()
    {
        PanelMove.SetActive(false);
    }
    public void SkipPanelInt()
    {
        // PanelInteract.SetActive(false);

        SceneManager.LoadScene(3);
    }
    public void SkipPanelAtk()
    {
         PanelAttack.SetActive(false);

        //SceneManager.LoadScene(3);
    }
    public void SkipPanelNPC()
    {
        PanelNPC.SetActive(false);

        //SceneManager.LoadScene(3);
    }
}
