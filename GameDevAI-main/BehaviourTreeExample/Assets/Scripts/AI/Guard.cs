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

    private float stoppingDistance = 0.1f;

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
        lastKnownPlayerPosition = new VariableGameObject { Value = new GameObject("tempobj") };
        lastKnownPlayerPosition.Value.transform.position = new Vector3(1,1,1);
        hasWeapon = new VariableFloat { Value = 0f };
        animator.SetBool("IsGuard", true);

        //Create your Behaviour Tree here!
        tree = new BTSelector(
                new BTSequence //Chase Sequence
                (
                    new BTCanSeeObject(player, this.gameObject, maxDetectionRange, enemyFov, stateText),
                    new BTSetLastKnownPlayerPosition(lastKnownPlayerPosition, player, stateText),
                    new BTSetCombatState(true, stateText),
                    new BTSelector(
                        new BTSequence //The enemy has a weapon
                        (
                            new BTCanSeeObject(player, this.gameObject, maxDetectionRange, enemyFov, stateText),
                            new BTHasWeapon(hasWeapon, stateText),
                            new BTSetPlayerAsTarget(target, agent, player, stateText),
                            new BTAnimate(animator, "Run", stateText),
                            new BTMoveToTarget(agent, runSpeed, target, stoppingDistance + 1.2f, stateText),
                            new BTAnimate(animator, "Kick", stateText),
                            new BTDamageTarget(target, agent.gameObject, 5000, stateText)
                        ),
                        new BTSequence //The enemy does not have a weapon yet
                        (
                            new BTLookForWeapon(weaponList, target, agent, stateText),
                            new BTAnimate(animator, "Run", stateText),
                            new BTMoveToTarget(agent, runSpeed, target, stoppingDistance, stateText),
                            new BTPickUpWeapon(hasWeapon, stateText),
                            new BTMoveToTarget(agent, runSpeed, lastKnownPlayerPosition, stoppingDistance, stateText),
                            new BTAnimate(animator, "Idle", stateText),
                            new BTWait(3f, stateText),
                            new BTSetCombatState(false, stateText),
                            new BTPickNewWanderPoint(waypointList, target, agent, stateText),
                            new BTAnimate(animator, "Rifle Walk", stateText),
                            new BTMoveToTarget(agent, walkSpeed, target, stoppingDistance, stateText),
                            new BTAnimate(animator, "Idle", stateText),
                            new BTWait(3f, stateText)
                        )
                    )
                ),
                new BTSequence //Look for the player if combat state is active
                (
                    new BTIsAware(stateText),
                    new BTHasWeapon(hasWeapon, stateText),

                    new BTSelector
                    (
                        new BTSequence //back to attack cycle if the player has been spotted again
                        (
                            new BTCanSeeObject(player, this.gameObject, maxDetectionRange, enemyFov, stateText),
                            new BTSetLastKnownPlayerPosition(lastKnownPlayerPosition, player, stateText),

                            new BTSequence //back to attack cycle if the player has been spotted again
                            (
                                new BTCanSeeObject(player, this.gameObject, maxDetectionRange, enemyFov, stateText),
                                new BTSetPlayerAsTarget(target, agent, player, stateText),
                                new BTAnimate(animator, "Run", stateText),
                                new BTMoveToTarget(agent, runSpeed, target, stoppingDistance + 1.2f, stateText),
                                new BTAnimate(animator, "Kick", stateText),
                                new BTDamageTarget(target, agent.gameObject, 5000, stateText)
                            )
                        ),
                        new BTSequence
                        (
                            new BTAnimate(animator, "Run", stateText),
                            new BTMoveToTarget(agent, runSpeed, lastKnownPlayerPosition, stoppingDistance, stateText),
                            new BTAnimate(animator, "Idle", stateText),
                            new BTWait(3f, stateText),
                            new BTSetCombatState(false, stateText)
                        )
                    )
                    
                ),
                new BTSequence //Wander Sequence
                (
                    new BTPickNewWanderPoint(waypointList, target, agent, stateText),
                    new BTAnimate(animator, "Rifle Walk", stateText),
                    new BTMoveToTarget(agent, walkSpeed, target, stoppingDistance, stateText),
                    new BTAnimate(animator, "Idle", stateText),
                    new BTWait(3f, stateText)
                )
            );
    }

    private void FixedUpdate()
    {
        tree?.Run();
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
