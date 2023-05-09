using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AI_Behaviour : MonoBehaviour
{
    public float _sphereTargetDistance = 20;
    public float _speed = 6;
    public float _timeToRecalculatePath = 3;
    private float _recalculateTimer = 0;

    private NavMeshAgent _agent;
    private Animator _animator; 
    private BearStats _stats; 
    public LayerMask _terrain;
    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _stats = GetComponent<BearStats>(); 
        _animator.SetBool("Running", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(_stats._dead) this.enabled = false;
        _recalculateTimer += Time.deltaTime;

        if(_recalculateTimer > _timeToRecalculatePath)
        {
            //RecalculatePath();
        }

        transform.position += transform.forward * _speed * Time.deltaTime;
        transform.Rotate(Vector3.up * _speed * 3 * Random.Range(-1,1) * Time.deltaTime);
    }

    private void RecalculatePath()
    {
        Vector3 newPos = Random.insideUnitSphere * _sphereTargetDistance;

        newPos += transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(newPos, out hit, _sphereTargetDistance, _terrain);
        Vector3 finalPosition = hit.position;

        _agent.SetDestination(finalPosition);
        _recalculateTimer = 0;
    }
}
