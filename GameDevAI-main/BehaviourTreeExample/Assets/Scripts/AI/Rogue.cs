using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private Player playerScript;

    private GameObject playerObject;

    private VariableFloat canThrowBomb;

    [SerializeField] private TextMesh stateText;
    [SerializeField] private List<Transform> coverSpots = new List<Transform>();

    [SerializeField] private GameObject smokeBomb;

    private GameObject activeSmoke;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here
        animator.SetBool("IsGuard", false);
        agent.stoppingDistance = 5f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerScript = playerObject.GetComponent<Player>();
        //moveSpeed = Instantiate(moveSpeed);


        tree =
            new BTSelector
            (
                new BTSequence
                (
                    new BTIsSpotted(playerScript, stateText),
                    new BTSequence
                    (
                        new BTSearchCover(agent, stateText, coverSpots),
                        new BTAnimate(animator, "Throw", stateText),
                        new BTThrowSmoke(animator, this, stateText),
                        new BTAnimate(animator, "Crouch Idle", stateText),
                        new BTWait(10f, stateText)
                    )
                ),
                new BTSequence
                (
                    new BTAnimate(animator, "Walk Crouch", stateText),
                    new BTFollow(agent, playerObject, stateText),
                    new BTAnimate(animator, "Crouch Idle", stateText),
                    new BTIdle(agent, playerObject, stateText)
                )
            );
    }

    private void FixedUpdate()
    {
        tree?.Run();
    }

    public void SpawnSmokeBomb()
    {
        Transform playerObject = playerScript.gameObject.transform;

        float offsetX = Random.Range(0,5);
        float offsetZ = Random.Range(0, 5);

        activeSmoke = Instantiate(smokeBomb, new Vector3(playerObject.position.x + offsetX, playerObject.position.y, playerObject.position.z + offsetZ), Quaternion.identity);
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
