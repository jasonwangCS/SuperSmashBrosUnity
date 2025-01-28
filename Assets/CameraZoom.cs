using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    // Start is called before the first frame update
    private float zoom;
    private float zoomMultiplier = 22f;
    private float minZoom = 30;
    private float maxZoom = 67.69531f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    [SerializeField] private CameraZoom cam;

    void Start()
    {
        zoom = cam.GetComponent<Camera>().orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMultiplier; 
        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cam.GetComponent<Camera>().orthographicSize = Mathf.SmoothDamp(cam.GetComponent<Camera>().orthographicSize, zoom, ref velocity, smoothTime);
    }
}
