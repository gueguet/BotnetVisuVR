using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{

    private LineRenderer lineRenderer;
    public GameObject destination;

    public float lineDrawSpeed = 4f;
    public float lineWidth;
    private float distTot, distDraw;
    private float x = 0;


    void Start()
    {
        // lineRenderer settings
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, destination.transform.position);
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // totale distance between the two nodes
        distTot = Vector3.Distance(destination.transform.position, transform.position);
    }
        

    void Update()
    {
        // evolution of the draw
        x += lineDrawSpeed * Time.deltaTime;

        // define the direction of the draw
        Vector3 pointA = transform.position;
        Vector3 pointB = destination.transform.position;      
        Vector3 pointAlongLine = pointB + (x * Vector3.Normalize(pointA - pointB));

        // draw until we reach the endPoint
        var distPoint = Vector3.Distance(pointAlongLine, pointB);
        if (distPoint < distTot)
        {
            pointAlongLine = pointB + (x * Vector3.Normalize(pointA - pointB));
            lineRenderer.SetPosition(0, destination.transform.position);
            lineRenderer.SetPosition(1, pointAlongLine);
        }

        else
        {
            lineRenderer.SetPosition(0, destination.transform.position);
            lineRenderer.SetPosition(1, transform.position);
        }

    }
}
