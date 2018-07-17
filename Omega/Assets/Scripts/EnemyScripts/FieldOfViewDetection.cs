﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewDetection : MonoBehaviour
{
    public static event System.Action PlayerSpotted;
    public Transform player;
    public float maxAngle;
    public float maxRadius;

    private bool isInFOV = false;

    //public Light spotLight;
    //Color spotlightOriginalColor;
    float playerCaughtTimer;
    float timeToSpotPlayer = .1f;

    private void Start()
    {
       // spotlightOriginalColor = spotLight.color;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,maxRadius);

        Vector3 fovLine1 = Quaternion.AngleAxis(maxAngle, transform.up) * transform.forward * maxRadius;
        Vector3 fovLine2 = Quaternion.AngleAxis(-maxAngle, transform.up) * transform.forward * maxRadius;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, fovLine1);
        Gizmos.DrawRay(transform.position, fovLine2);

        if (!isInFOV)
            Gizmos.color = Color.red;
        else
            Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position,(player.position - transform.position).normalized *maxRadius);

        Gizmos.color = Color.black;
        Gizmos.DrawRay(transform.position,transform.forward * maxRadius);

    }

    public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
    {
        Collider[] overlaps = new Collider[10];
        int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

        for (int i = 0; i < count; i++)
        {
            if (overlaps[i] != null)
            {
                if (overlaps[i].transform == target)
                {
                    Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                    directionBetween.y *= 0;

                    float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                    if (angle <= maxAngle)
                    {
                        Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                        RaycastHit hit;

                        if (Physics.Raycast(ray,out hit, maxRadius))
                        {
                            if (hit.transform == target)
                                return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    private void Update()
    {
        isInFOV = inFOV(transform, player, maxAngle, maxRadius);
        if (isInFOV == true)
        {
            playerCaughtTimer += Time.deltaTime;
        }
        else
        {
            playerCaughtTimer -= Time.deltaTime;
        }
        playerCaughtTimer = Mathf.Clamp(playerCaughtTimer, 0, timeToSpotPlayer);
        //spotLight.color = Color.Lerp(spotlightOriginalColor, Color.red, playerCaughtTimer / timeToSpotPlayer);

        if (playerCaughtTimer >= timeToSpotPlayer)
        {
            if (PlayerSpotted != null)
            {
                PlayerSpotted();
            }
        }
    }
}
