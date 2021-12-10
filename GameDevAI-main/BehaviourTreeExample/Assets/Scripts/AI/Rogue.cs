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
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        canThrowBomb = new VariableFloat { Value = 1f };
        //moveSpeed = Instantiate(moveSpeed);


        tree =
            new BTSequence
            (
                new BTFollow(animator, agent, stateText, playerScript),
                new BTIdle(animator, agent, stateText, playerScript),
                new BTSequence(
                        new BTSearchCover(animator, agent, stateText, playerScript, coverSpots),
                        new BTThrowSmoke(agent, stateText, playerScript, animator, this, canThrowBomb)
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

        StartCoroutine(SmokeBombDelay());
    }

    private IEnumerator SmokeBombDelay()
    {
        canThrowBomb.Value = 0;
        yield return new WaitForSeconds(10);
        Destroy(activeSmoke);
        canThrowBomb.Value = 1;
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
