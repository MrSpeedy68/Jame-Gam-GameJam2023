using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PointAndClickMovement : MonoBehaviour
{
    public LayerMask groundLayer;
    public float maxDistance = 100.0f; //Max distance to perform raycast

    [SerializeField] private GameObject drawObject;
    
    private Camera _camera;
    private Vector3 _currentGoToPos;
    private NavMeshAgent _navMeshAgent;
    private void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance, groundLayer))
            {
                _currentGoToPos = hit.point;
                DrawLocation();
                MoveToLocation();
            }
        }
    }

    private void MoveToLocation()
    {
        _navMeshAgent.SetDestination(_currentGoToPos);
    }
    private void DrawLocation()
    {
        Destroy(Instantiate(drawObject, _currentGoToPos, Quaternion.identity), 10.0f);
    }
}
