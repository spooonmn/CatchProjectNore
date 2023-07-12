using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Camera))]
public class MultiTargetCameraScript : MonoBehaviour
{
    public List<Transform> targets; // list of players

    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint(); // gets center point of all players
        Vector3 newPosition = centerPoint + offset; // adds offset
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime); // smoothing
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        float greatestDistance = Mathf.Max(bounds.size.x, bounds.size.y * 2); // get the maximum of width and height
        return greatestDistance;
    }


    Vector3 GetCenterPoint()
    {
        // if only one player camera center is player
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        // creates a rectangle around the players
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        // returns the center of the rectangle 
        return bounds.center;
    }
}