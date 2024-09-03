using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truonghop : MonoBehaviour
{
    [SerializeField]
    private bool showGizmos = true;

    
    float[] interestGizmo = new float[0];
    Vector2 resultDirection = Vector2.zero;
    private float rayLength = 2;

    private void Start()
    {
        interestGizmo = new float[8];
    }

    public Vector2 GetDirectionToMove(List<DieukhienHanhvi> behaviours, AIData aiData)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

       
        foreach (DieukhienHanhvi behaviour in behaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aiData);
        }

        
        for (int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        interestGizmo = interest;

      
        Vector2 outputDirection = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outputDirection += PhuongHuong.Huong[i] * interest[i];
        }

        outputDirection.Normalize();

        resultDirection = outputDirection;

        
        return resultDirection;
    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying && showGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, resultDirection * rayLength);
        }
    }
}
