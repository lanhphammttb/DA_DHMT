using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class NPC1 : MonoBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isWaving = true;
    [SerializeField] protected bool isWalking = false;
    private bool isAction;

    protected virtual void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void Awake()
    {
        this.LoadComponents();
    }

    protected void LoadComponents()
    {
        this.LoadAgent();
        this.LoadAnimator();
    }

    private void Start()
    {
        isAction = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isPlayer"))
        {
            Debug.Log("Starting " + Time.time);
            StartCoroutine(After(3.0f));
        }

        if (other.gameObject.CompareTag("isTarget"))
        {
            isAction = false;
        }

        IEnumerator After(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            isAction = true;
            Debug.Log("Done " + Time.time);
        }
    }
    void FixedUpdate()
    {
        this.Moving();
        this.Animating();
    }

    protected virtual void LoadAgent()
    {
        if (this.navMeshAgent != null) return;
        this.navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log(transform.name + ": LoadAgent", gameObject);
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    public virtual void SetTarget(Transform trans)
    {
        this.target = trans;
    }
    protected virtual void Moving()
    {
        if (!isAction)
        {
            this.navMeshAgent.isStopped = true;
            this.isWalking = false;
            return;
        }
        this.isWalking = true;
        this.navMeshAgent.isStopped = false;
        this.navMeshAgent.SetDestination(this.target.position);
    }

    protected virtual void Animating()
    {
        this.animator.SetBool("isWaving", this.isWaving);
        this.animator.SetBool("isWalking", this.isWalking);
    }
}
