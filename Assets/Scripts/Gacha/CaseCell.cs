using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaseCell : MonoBehaviour
{
    [System.Serializable]

    private class ListOfSprites
    {
        public List<Sprite> Sprites;
    }

    [SerializeField]
    private List<ListOfSprites> sprites;
    [SerializeField]
    private int[] chances;
    [SerializeField]
    private Color[] colors;

    public void Setup()
    {
        var index = Randomize();
        GetComponent<Image>().sprite = sprites[index].Sprites[Random.Range(0, sprites[index].Sprites.Count)];
        transform.parent.GetComponent<Image>().color = colors[index];
    }

    private int Randomize()
    {
        int selectedIndex = 0; 

        int rand = Random.Range(0, 100);

        for (int i = 0; i < chances.Length; i++)
        {
            if (rand < chances[i])
            {
                selectedIndex = i;
                break;
            }
        }

        return selectedIndex; 

    }

}
