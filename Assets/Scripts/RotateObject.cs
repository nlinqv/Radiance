﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public Transform mirror;
    public float rotateSpeed;
    public float baseAngle;
    private void OnMouseDown()
    {
        Vector3 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        baseAngle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg;
        baseAngle -= mirror.eulerAngles.y;
    }

    private void OnMouseDrag()
    {
        Vector3 pos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(pos.x, pos.y) * Mathf.Rad2Deg - baseAngle;
        mirror.rotation = Quaternion.AngleAxis(angle, Vector3.up * rotateSpeed);

    }
}