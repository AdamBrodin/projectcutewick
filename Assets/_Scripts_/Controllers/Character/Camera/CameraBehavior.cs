using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* - Bogdan Chernikov - SU17A -  2019-2020© -*/

public class CameraBehavior : MonoBehaviour
{
    #region Variables
    public Transform target;

    public float cameraDistance = 3.0f;
    public float camerHeight = 3.0f;
    public float damping = 5.0f;
    public float rotationDamping = 10.0f;

    public bool smoothRotation = true;
    public bool followBehind = true;
    #endregion

    void FixedUpdate()
    {
        if (target != null)
        {
            Vector3 wantedPosition;

            if (followBehind)
                wantedPosition = target.TransformPoint(0, camerHeight, -cameraDistance);
            else
                wantedPosition = target.TransformPoint(0, camerHeight, cameraDistance);

            transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * damping);

            if (smoothRotation)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
            }

            else transform.LookAt(target, target.up);
        }
    }

}
