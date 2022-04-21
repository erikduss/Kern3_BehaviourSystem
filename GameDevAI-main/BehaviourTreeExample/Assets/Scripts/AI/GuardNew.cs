using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class GuardNew : MonoBehaviour
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
    private Player playerScript;
    private float maxDetectionRange = 15;
    private float enemyFov = 45;

    private VariableFloat hasWeapon;
    private VariableGameObject lastKnownPlayerPosition;

    private bool isChasing = false;
    private bool isOnGuard = false;
    private bool playerInSight = false;
    private bool playerAlive = true;

    [SerializeField] private TextMesh stateText;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Start()
    {
        target = new VariableGameObject { Value = new GameObject("tempobj") };
        hasWeapon = new VariableFloat { Value = 0f };
        animator.SetBool("IsGuard", true);

        //Create your Behaviour Tree here!
        tree = new BTSequence(
                new BTPickNewWanderPoint(waypointList, target, agent, stateText),
                new BTAnimate(animator, "Rifle Walk", stateText),
                new BTMoveToTarget(agent, walkSpeed, target, stoppingDistance, stateText)
            );
    }

    private void FixedUpdate()
    {
        tree?.Run();
    }

    private void RevertBackToWander()
    {
        playerInSight = false;
        isChasing = false;
        isOnGuard = false;
        playerScript.isBeingChased = false;

        target.Value = waypointList[1].gameObject;

        tree = new BTSequence(
                new BTPickNewWanderPoint(waypointList, target, agent, stateText),
                new BTAnimate(animator, "Rifle Walk", stateText),
                new BTMoveToTarget(agent, walkSpeed, target, stoppingDistance, stateText)
            );
    }

    private IEnumerator GiveUpChase(int delay)
    {
        yield return new WaitForSeconds(delay);
        if (!playerInSight)
        {
            RevertBackToWander();
            Debug.Log("Giving up");
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
