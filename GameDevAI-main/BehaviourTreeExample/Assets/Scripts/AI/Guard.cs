using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;

    private float stoppingDistance;

    [SerializeField] private Transform[] waypointList;
    [SerializeField] private Transform[] weaponList;
    public VariableGameObject target;

    private float walkSpeed = 1;
    private float runSpeed = 2;

    private GameObject player;
    private float maxDetectionRange = 15;
    private float enemyFov = 45;

    private bool hasWeapon = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        target = new VariableGameObject { Value = new GameObject("tempobj") };
        animator.SetBool("IsGuard", true);

        //Create your Behaviour Tree here!
        tree = new BTSequence(
                new BTPickNewWanderPoint(waypointList, target, agent),
                new BTAnimate(animator, "Rifle Walk"),
                new BTMoveToTarget(agent, walkSpeed, target, stoppingDistance)
            );
    }

    // check to see if a player is within viewing range
    private bool IsPlayerNear()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < maxDetectionRange) // if true, an Enemy is within range
            return true;
        else return false;
    }

    // check to see if Enemy is in npc's current fov
    private bool IsPlayerInEnemyFOV()
    {
        Vector3 targetDir = player.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        if (angle < enemyFov)
            return true;
        else return false;
    }

    // can the Enemy see the Player
    private void IsPlayerSeen()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;

        RaycastHit hit;

        if ((Physics.Raycast(agent.transform.position, direction, out hit, maxDetectionRange) && hit.collider.tag == "Player"))
        {
            Debug.Log("Player has been seen");
            tree = new BTSequence(
                new BTLookForWeapon(weaponList, target, agent, hasWeapon),
                new BTAnimate(animator, "Run"),
                new BTMoveToTarget(agent, runSpeed, target, stoppingDistance)
            );
        }
    }

    private void FixedUpdate()
    {
        tree?.Run();

        if(IsPlayerNear() && IsPlayerInEnemyFOV())
        {
            IsPlayerSeen();
        }
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Handles.color = Color.yellow;
    //    Vector3 endPointLeft = viewTransform.position + (Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;
    //    Vector3 endPointRight = viewTransform.position + (Quaternion.Euler(0, ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward).normalized * SightRange.Value;

    //    Handles.DrawWireArc(viewTransform.position, Vector3.up, Quaternion.Euler(0, -ViewAngleInDegrees.Value, 0) * viewTransform.transform.forward, ViewAngleInDegrees.Value * 2, SightRange.Value);
    //    Gizmos.DrawLine(viewTransform.position, endPointLeft);
    //    Gizmos.DrawLine(viewTransform.position, endPointRight);

    //}
}
