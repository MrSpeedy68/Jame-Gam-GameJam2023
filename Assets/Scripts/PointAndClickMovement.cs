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
    private bool _bAttacking = false;
    private Interactable _interactable;
    private Enemy _enemy;
    private Player _player;
    private Animator _animator;
    private AnimatorStateInfo _animationStateInfo;
    private Outline _outlinedObject;
    
    private void Start()
    {
        _camera = Camera.main;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _player = GetComponent<Player>();
        _animator = GetComponentInChildren<Animator>();
        
        _navMeshAgent.speed = _player.GetSpeed();
        _currentGoToPos = transform.position;
    }

    private void Update()
    {
        _animationStateInfo = _animator.GetCurrentAnimatorStateInfo(0);

        _animator.SetFloat("DistanceToTarget", Vector3.Distance(transform.position, _currentGoToPos));
        
        // Animation State Machine
        if (_animationStateInfo.IsName("Walking"))
        {
            _navMeshAgent.isStopped = false;
            MoveToLocation();
        }
        
        if (_animationStateInfo.IsName("Idle"))
        {
            _navMeshAgent.isStopped = true;
        }
        
        if (_animationStateInfo.IsName("Attack"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_enemy)
                {
                    _animator.SetTrigger("Attack");
                    _enemy.TakeDamage(_player.GetDamage());
                    _navMeshAgent.isStopped = true;
                    _bInteracting = false;
                }
                else
                {
                    _animator.SetBool("isAttack", false);
                }
            }
        }
        

        if (Vector3.Distance(transform.position, _currentGoToPos) < 2f && _interactable && _bInteracting)
        {
            _navMeshAgent.isStopped = true;
            _interactable.Interact();
            _bInteracting = false;
        }
        
        //Outlining Objects
        
        Ray rayOutline = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitOutline;
        if (Physics.Raycast(rayOutline, out hitOutline, maxDistance))
        {
            if (hitOutline.transform.CompareTag("Interactable"))
            {
                _outlinedObject = hitOutline.transform.gameObject.GetComponent<Outline>();
                _outlinedObject.enabled = true;
            }
            else if (hitOutline.transform.CompareTag("Enemy"))
            {
                _outlinedObject = hitOutline.transform.gameObject.GetComponent<Outline>();
                _outlinedObject.enabled = true;
            }
            else if (_outlinedObject)
            {
                _outlinedObject.enabled = false;
                _outlinedObject = null;
            }
        }
        
        
        // Mouse Input
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
                    _animator.SetBool("isInteract", true);
                    _bInteracting = true;
                    
                    FindGroundPoint(hit, ray);
                }
                else if (hit.transform.CompareTag("Ground"))
                {
                    _currentGoToPos = hit.point;
                    //_animator.SetTrigger("Walking");
                    DrawLocation();
                    MoveToLocation();
                }
                else if (hit.transform.CompareTag("Enemy"))
                {
                    _enemy = hit.transform.gameObject.GetComponent<Enemy>();
                    _animator.SetBool("isAttack", true);

                    FindGroundPoint(hit, ray);
                }
            }
        }
    }

    private void FindGroundPoint(RaycastHit hit, Ray ray)
    {
        RaycastHit newHit;

        if (Physics.Raycast(hit.point + ray.direction * 0.01f, ray.direction, out newHit, maxDistance,
                groundLayer))
        {
            _currentGoToPos = newHit.point;
            DrawLocation();
            MoveToLocation();
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
