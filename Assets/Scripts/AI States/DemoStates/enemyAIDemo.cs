using UnityEngine;
using System.Collections;

public class enemyAIDemo : MonoBehaviour
{
    [HideInInspector]
    public IdleStateDemo attackState;
    [HideInInspector]
    public IdleStateDemo patrolState;
    [HideInInspector]
    public AlertStateDemo alertState;
    [HideInInspector]
    public IEnemyStateDemo currentState;

    [HideInInspector]
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    public Light myLight;
    public float life = 100;
    public float timeBetweenShots = 1.0f;
    public float damageForce = 10;
    public float rotationTime = 3.0f;
    public float shotHeight = 0.5f;
    public Transform[] waypPints;

    void Start()
    {
        // AI States.
        alertState = new AlertStateDemo(this);
        attackState = new AttackStateDemo(this);
        patrolState = new PatrolStateDemo(this);

        // Start patrolling
        currentState = patrolState;

        //Keep a NavMesh reference
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {

        // Since our states don't inherit from
        // MonoBehaviour, its update is not called
        // automatically, and we'll take care of it
        // us to call it every frame.
        currentState.UpdateState();

        if (life < 0) Destroy(this.GameObject);

    }

    public void Hit(float damage)
    {
        life -= damage;
        currentState.Impact();
        Debug.Log("Enemy hit:" + life);
    }

    // Since our states don't inherit from
    // MonoBehaviour, we'll have to let them know
    // when something enters, stays,  or leaves our
    // trigger.
    void OnTriggerEnter(Collider col)
    {
        currentState.OnTriggerEnter(col);
    }

    void OnTriggerStay(Collider col)
    {
        currentState.OnTriggerStay(col);
    }

    void OnTriggerExit(Collider col)
    {
        currentState.OnTriggerExit(col);
    }
}
