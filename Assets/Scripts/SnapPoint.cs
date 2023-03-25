using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnapPoint : MonoBehaviour
{
    public bool isAttached = false;

    public Transform snapTransform;
    void Start()
    {
        snapTransform = transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position,0.1f);
        DrawArrow.ForGizmo(transform.position, transform.forward, Color.red);
    }

    public void AttachSnapPoint()
    {
        isAttached = true;
    }
}
