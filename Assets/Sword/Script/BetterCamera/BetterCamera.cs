using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterCamera : MonoBehaviour
{
    [SerializeField] private GameObject followObject;
    [SerializeField] private float yOffset = 2;
    [SerializeField] [Range(20, 100)] private float movementSpeed = 20;
    
    [SerializeField] private bool predictedMovementOffset = true;
    [SerializeField] [Range(0.1f, 10)] private float zoomSpeed = 0.5f; 
    [SerializeField] [Range(1, 5)] private float maxExtraZoom = 2.2f;

    private Transform followObjectTransform;

    private Vector3 followObjectCurrentPos;
    private Vector3 followObjectPastPos;

    private Vector3 followPrediction;

    private Camera _camera;
    private float _defaultCamSize;
    
    // Start is called before the first frame update
    void Start()
    {
        followObjectTransform =  followObject.GetComponent<Transform>();
        followObjectCurrentPos = followObjectPastPos = followObjectTransform.transform.position;

        _camera = GetComponent<Camera>();
        _defaultCamSize = _camera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        followObjectCurrentPos = followObjectTransform.transform.position;
        followPrediction =  followObjectCurrentPos - followObjectPastPos;

        if (predictedMovementOffset)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize,
                _defaultCamSize + (followPrediction.normalized.magnitude * maxExtraZoom), zoomSpeed * Time.deltaTime);
        }
        else
        {
            _camera.orthographicSize = _defaultCamSize;
        }
        
        transform.position = Vector3.Lerp(transform.position, new Vector3(followObjectCurrentPos.x, 
                followObjectCurrentPos.y + yOffset, transform.position.z), movementSpeed * Time.deltaTime);
        
        followObjectPastPos = followObjectCurrentPos;
    }
}
