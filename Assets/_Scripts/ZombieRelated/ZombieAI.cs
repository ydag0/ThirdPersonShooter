using UnityEngine;
using UnityEngine.AI;
public class ZombieAI : Zombie
{
    public float zombieDamage { get; private set; } = 5;
    
    NavMeshAgent navMeshAgent;
    Transform target;

    float stopDistance = 1;
    float rayDistance { get { return stopDistance; } }
    int layerMask;
    void Start()
    {
        zombieDamage = GameManager.Instance.gameSettings.zombieDamage;
        target = GameManager.Instance.player.transform;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        
        layerMask = LayerMask.GetMask("Player");
    }

    void SetDestination()
    {
        transform.LookAt(target.position);
        Vector3 distance = target.position - transform.position;//distance between zombie and target

        if (distance.magnitude <= stopDistance)// IF distance equal or smaller than stopdistance return 
            return;

        distance = target.position - (distance.normalized * stopDistance);//distance with stop difference
        navMeshAgent.SetDestination(distance);                    
    }
    void Update()
    {
        HandleState();
    }
    private void FixedUpdate()
    {
        if (state == ZombieState.walking)
            SetDestination();
    }
    void HandleState()
    {
        //if player is dead
        if (GameManager.Instance.player.dead)
        {
            state = ZombieState.walking;
            return;
        }
            
        //If walking and If it is in the attack range switch attacking state
        if (state == ZombieState.walking)
        {
            if (InAttackRange() && state != ZombieState.attacking)
            {
                state = ZombieState.attacking;
            }
        }
        //If attacking and If it is NOT in the attack range switch running state
        else if (state == ZombieState.attacking)
        {
            if (!InAttackRange() && state != ZombieState.walking)
            {
                state = ZombieState.walking;
            }
        }

        if (health <= 0)
        {
            //if it is not already dead.
            if (state != ZombieState.dead)
            {
                state = ZombieState.dead;
                ZombieMode.Instance.InvokeDeath();
            }
        }
        else if (state == ZombieState.dead)
        {
            state = ZombieState.walking;
        }
    }

    bool InAttackRange()
    {
        //Shoots A Ray With Layermask
        bool inRange = false;

        Ray ray = new(transform.position + Vector3.up *.3f, transform.forward);
        if(Physics.Raycast(ray ,rayDistance ,layerMask))
        {
            inRange = true;
        }
        return inRange;
    }
}
