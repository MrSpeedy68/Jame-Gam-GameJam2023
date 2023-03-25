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
    private bool _bInteracting = false;
    private Interactable _interactable;
    private void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _navMeshAgent.isStopped = false;
            
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.transform.CompareTag("Interactable"))
                {
                    _interactable = hit.transform.gameObject.GetComponent<Interactable>();
                    _bInteracting = true;
                    
                    RaycastHit newHit;
                    
                    if (Physics.Raycast(hit.point + ray.direction * 0.01f, ray.direction, out newHit, maxDistance, groundLayer))
                    {
                        _currentGoToPos = newHit.point;
                        DrawLocation();
                        MoveToLocation();
                    }
                }
                else if (hit.transform.CompareTag("Ground"))
                {
                    _currentGoToPos = hit.point;
                    DrawLocation();
                    MoveToLocation();
                }
            }
        }

        if (_bInteracting)
        {
            //Debug.Log(Vector3.Distance(transform.position, _currentGoToPos));
            if (Vector3.Distance(transform.position, _currentGoToPos) < 2f && _interactable)
            {
                _navMeshAgent.isStopped = true;
                _interactable.Interact();
                _bInteracting = false;
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
