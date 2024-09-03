using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvUIManager : MonoBehaviour
{
    [SerializeField] private CraftingDisplay _cratingWindowParent;
    private void OnEnable()
    {
        CraftingBench.OnCraftingDisplayRequest += DisplayCraftingWindow;
    }
    private void OnDisable()
    {
        CraftingBench.OnCraftingDisplayRequest -= DisplayCraftingWindow;
    }
    private void DisplayCraftingWindow(CraftingBench craftingBench)
    {

    }
}
