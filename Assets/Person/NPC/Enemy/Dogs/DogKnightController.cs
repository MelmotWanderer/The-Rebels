using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class DogKnightController : MonoBehaviour
{
    [SerializeField] Transform[] _PatrolPoints;
    [SerializeField] Player _player;
    private NavMeshAgent agent;
    private Transform targetWayPoint;
    private Animator _anim;
    public bool _foundEnemyTarget = false;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        targetWayPoint = _PatrolPoints[0];
        _anim = GetComponent<Animator>();
    }

    
    void Update()

    {
        if (!_foundEnemyTarget)
        {
            Walk();
        }else
        {
            RunToEnemy();
        }
        
    }
   
        
       
    
    private void Walk()
    {
        _anim.SetBool("isRun", false);
        _anim.SetBool("isWalk", true);
        agent.speed = 1.5f;
        if (Vector3.Distance(transform.position, targetWayPoint.position) <= 1f)
        {

            targetWayPoint = _PatrolPoints[Random.Range(0, _PatrolPoints.Length)];

        }
        else
        {
            agent.SetDestination(targetWayPoint.position);

        }
        
    }
    private void RunToEnemy()
    {
        _anim.SetBool("isWalk", false);
        
        agent.speed = 1.5f;
        targetWayPoint = _player.transform;
        if (Vector3.Distance(transform.position, targetWayPoint.position) <= 2f)
        {
            agent.ResetPath();
            Attack();
            

        }
        else
        {
            _anim.SetBool("isRun", true);
            agent.SetDestination(targetWayPoint.position);
        }
        
    }
    public void Attack()
    {
        _anim.SetTrigger("Attack");
    }
    public void EnemyTargetFound()
    {
        _foundEnemyTarget = true;
        
    }
}
