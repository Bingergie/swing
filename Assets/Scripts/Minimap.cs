using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;

    private void LateUpdate()
    {
        Vector3 oldPosition = transform.position;
        Vector3 newPosition = player.position;

        newPosition.y = oldPosition.y;
        newPosition.z = oldPosition.z;

        transform.position = newPosition;
    }
}
