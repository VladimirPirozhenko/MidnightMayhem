using FishNet.Object;
using FishNet.Connection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : NetworkBehaviour
{
    #region Serialized
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float xAxisSensibility;
    [SerializeField] private float yAxisSensibility;
    #endregion

    #region Private
    private Joystick joystick;
    
    private Camera cam;
    private Transform camTransform;
    private Vector3 previousPosition;
    #endregion

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        //joystick = FindObjectOfType<FixedJoystick>();
        cam = Camera.main;
        camTransform = cam.transform;   
        cam.gameObject.SetActive(true);
    }
    private void LateUpdate()
    {
        if (!IsOwner)
            return;
        //float yLowerBorder = joystick.transform.position.y + 480/2;
        //float yHigherBorder = joystick.transform.position.y - 480 / 2;
        //float xLowerBorder = joystick.transform.position.x - 480 / 2;
        //float xHigherBorder = joystick.transform.position.x + 480 / 2;
        Vector2 mousePosition = Input.mousePosition;
        //if (!(mousePosition.y < yLowerBorder && mousePosition.y > yHigherBorder
        //&& mousePosition.x  > xLowerBorder && mousePosition.x < xHigherBorder))
        {
            if (Input.GetMouseButtonDown(0))
            {
                previousPosition = cam.ScreenToViewportPoint(mousePosition);
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 newPosition = cam.ScreenToViewportPoint(Input.mousePosition);
                Vector3 direction = previousPosition - newPosition;
                float rotationAroundYAxis = -direction.x * xAxisSensibility;
                float rotationAroundXAxis = direction.y * yAxisSensibility;
                cam.transform.position = target.position;
                cam.transform.Rotate(Vector3.right, rotationAroundXAxis);
                cam.transform.Rotate(Vector3.up, rotationAroundYAxis,Space.World);
                cam.transform.Translate(offset);
                previousPosition = newPosition;
            }
            else
            {
                camTransform.position = target.position;
                cam.transform.Translate(offset);
            }
        }
        //else
        //{
        //    camTransform.position = target.position;
        //    cam.transform.Translate(offset);
        //}
    }
}