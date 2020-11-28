using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patroller : MonoBehaviour
{
    public Transform[] Target;
    public int speed;
    public Transform player;
    private Rigidbody rb;
    IEnumerator behaviour;
    public float waitTime;
    // private Vector3 movement;

    private int targetIndex;
    private float dist;
    bool flag;
    public bool playerinSightRange;

    public float sightRange;
    public LayerMask whatIsPlayer;
    // Start is called before the first frame update


    void Start()
    {
        targetIndex = 0;
        transform.LookAt(Target[targetIndex].position);
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        //direction.Normalize();
        dist = Vector3.Distance(transform.position, Target[targetIndex].position);
        if (behaviour != null)
        {
            StopCoroutine(behaviour);
        }
        playerinSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (!playerinSightRange)
        {
            StopCoroutine(behaviour);
            behaviour = Patrol();
            StartCoroutine(behaviour);
        }
        if (playerinSightRange)
        {
            StopCoroutine(behaviour);
            behaviour = Chase(direction);
            StartCoroutine(behaviour);
        }
    }

    IEnumerator Patrol()
    {
        var wait = new WaitForSeconds(Random.Range(0, waitTime));
        var waitFrame = new WaitForFixedUpdate();
        if (dist >= 1f)
        {
            IncreaseIndex();
            yield return waitFrame;
        }
        yield return wait;
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }  

        void IncreaseIndex()
        {
            targetIndex++;
            if (targetIndex >= Target.Length)
            {
                targetIndex = 0;
            }
            transform.LookAt(Target[targetIndex].position);
        }
    IEnumerator Chase(Vector3 direction)
    {
        var waitFrame = new WaitForFixedUpdate();
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
        yield return waitFrame;
    }
}
