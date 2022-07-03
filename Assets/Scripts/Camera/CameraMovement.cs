using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    // controls how quickly the camera moves towards the target; lower speed --> smoother movement
    // technically the interpolation ratio; not speed
    public float cameraSpeed;

    // can be set so camera doesn't move past boundaries of scene
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void LateUpdate()
    {
        if (transform.position.Equals(target.position))
        {
            return;
        }

        Vector3 transformPos = transform.position;
        // use own z-position so camera doesn't fall below 2d scene
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, transformPos.z);

        targetPos = GetTargetPosWithinBounds(targetPos);

        if (transformPos.Equals(targetPos))
        {
            return;
        }

        transform.position = Vector3.Lerp(transformPos, targetPos, cameraSpeed);
    }

    private Vector3 GetTargetPosWithinBounds(Vector3 targetPos)
    {
        if (!HasBoundariesSet())
        {
            return targetPos;
        }

        float xPos = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        float yPos = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
        Vector3 targetPosWithinBounds = new Vector3(xPos, yPos, targetPos.z);
        return targetPosWithinBounds;
    }

    private bool HasBoundariesSet()
    {
        return maxPosition != Vector2.zero || minPosition != Vector2.zero;
    }
}
