using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private TextMesh textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor = textMesh.color;
        disappearTimer = 1f;
        moveVector = new Vector3(0, 1, 0) * 2f;
    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 8f * Time.deltaTime;

        if (disappearTimer > 0)
        {
            disappearTimer -= Time.deltaTime;
            if (disappearTimer <= 0)
            {
                // Start disappearing
                textColor.a -= 3f * Time.deltaTime;
                textMesh.color = textColor;
                if (textColor.a <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
