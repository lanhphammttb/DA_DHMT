using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorkerMovement : SaiBehaviour
{
    [SerializeField] protected Transform target;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isWaving = true;
    [SerializeField] protected bool isWalking = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadAgent();
        this.LoadAnimator();
    }


    protected override void FixedUpdate()
    {
        this.FindHouse();
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
        if (!navMeshAgent)
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

    protected virtual void FindHouse()
    {
        Transform house = BuildingManager.instance.FindBuilding(transform);
        if (house == null) return;
        this.target = house;
    }
}
