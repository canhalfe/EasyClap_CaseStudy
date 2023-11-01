using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowNew : MonoBehaviour
{
    public Transform PlayerTransform;
    private Vector3 _cameraOffset;

    [Range(0.01f, 5.0f)]
    public float SmootFactor = 0.5f;

    public bool LookatPlayer = false;
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    void FixedUpdate()
    {
        //Vector3 newPos = PlayerTransform.position + _cameraOffset;

        Vector3 newPos = new Vector3(transform.position.x, transform.position.y, PlayerTransform.position.z + _cameraOffset.z);

        transform.position = Vector3.Slerp(transform.position, newPos, SmootFactor * Time.fixedDeltaTime);

        if (LookatPlayer)
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
