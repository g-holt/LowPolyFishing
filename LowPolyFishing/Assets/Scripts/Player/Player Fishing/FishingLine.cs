using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    [SerializeField] Transform[] gearPoints;
    
    LineRenderer lineRenderer;


    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = gearPoints.Length;
    }

    
    void Update()
    {
        DrawFishingLine();
    }


    void DrawFishingLine()
    {
        for(int i = 0; i < gearPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, gearPoints[i].position);
        }
    }
    
}
