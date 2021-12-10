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
            playerInSight = true;
            playerScript.isBeingChased = true;
            if (!isOnGuard)
            {
                Debug.Log("Player has been seen");

                lastKnownPlayerPosition = new VariableGameObject { Value = new GameObject("lastknownpos") };
                lastKnownPlayerPosition.Value.transform.position = player.transform.position;
                
                tree = new BTSequence(
                    new BTAnimate(animator, "Run", stateText),
                    new BTLookForWeapon(weaponList, target, agent, hasWeapon, lastKnownPlayerPosition, isChasing, stateText),
                    new BTChasePlayer(playerInSight, hasWeapon, target, stateText),
                    new BTMoveToTarget(agent, runSpeed, target, stoppingDistance, stateText)
                );

                isOnGuard = true;
                StartCoroutine(GiveUpChase(10));
            }
            else if (hasWeapon.Value > 0)
            {
                if (agent.remainingDistance < 1f)
                {
                    playerAlive = false;
                    tree = null;
                    animator.Play("Kick");

                    IDamageable playerdamageable = player.GetComponent<Player>();
                    playerdamageable.TakeDamage(this.gameObject,1000);

                    StartCoroutine(GiveUpChase(2));
                }
                lastKnownPlayerPosition.Value.transform.position = player.transform.position;
                isChasing = true;
            }
            else
            {
                lastKnownPlayerPosition.Value.transform.position = player.transform.position;
                isChasing = false;
            }
        }
        else
        {
            /*lastKnownPlayerPosition = new VariableGameObject { Value = new GameObject("lastknownpos") };
            lastKnownPlayerPosition.Value.transform.position = player.transform.position;

            target.Value = lastKnownPlayerPosition.Value;*/

            Debug.Log("Cant see the player");
            playerInSight = false;
            isChasing = false;
        }
    }

    private void FixedUpdate()
    {
        tree?.Run();

        if(IsPlayerNear() && IsPlayerInEnemyFOV() && playerAlive)
        {
            IsPlayerSeen();
        }
        else
        {
            playerInSight = false;
            isChasing = false;
        }
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
