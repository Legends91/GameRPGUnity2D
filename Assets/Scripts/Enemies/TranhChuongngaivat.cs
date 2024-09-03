using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranhChuongngaivat : DieukhienHanhvi
{
    [SerializeField]
    private float bankinh = 2f, agentColliderSize = 0.6f;

    [SerializeField]
    private bool showGizmo = true;

  
    float[] dangersResultTemp = null;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (Collider2D chuongngaivatCollider in aiData.chuongngaivat)
        {
            Vector2 directionToObstacle
                = chuongngaivatCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

           
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (bankinh - distanceToObstacle) / bankinh;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            
            for (int i = 0; i < PhuongHuong.Huong.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, PhuongHuong.Huong[i]);

                float valueToPutIn = result * weight;

                
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (showGizmo == false)
            return;

        if (Application.isPlaying && dangersResultTemp != null)
        {
            if (dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(
                        transform.position,
                        PhuongHuong.Huong[i] * dangersResultTemp[i] * 2
                        );
                }
            }
        }

    }
}

public static class PhuongHuong
{
    public static List<Vector2> Huong = new List<Vector2>{
            new Vector2(0,1).normalized,
            new Vector2(1,1).normalized,
            new Vector2(1,0).normalized,
            new Vector2(1,-1).normalized,
            new Vector2(0,-1).normalized,
            new Vector2(-1,-1).normalized,
            new Vector2(-1,0).normalized,
            new Vector2(-1,1).normalized
        };
}
