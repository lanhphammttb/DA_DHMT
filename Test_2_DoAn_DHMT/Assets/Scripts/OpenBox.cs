using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class OpenBox: MonoBehaviour
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected bool isOpen = false;
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
        this.LoadAnimator();
    }

    private void Start()
    {
        isAction = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isHand"))
        {
            isAction = true;
            Debug.Log("Starting " + Time.time);
            StartCoroutine(After(2.0f));
        }

        IEnumerator After(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            isAction = false;
            Debug.Log("Done " + Time.time);
        }
    }
    void FixedUpdate()
    {
        this.Moving();
        this.Animating();
    }

    protected virtual void LoadAnimator()
    {
        if (this.animator != null) return;
        this.animator = GetComponent<Animator>();
        Debug.Log(transform.name + ": LoadAnimator", gameObject);
    }

    protected virtual void Moving()
    {
        if (isAction)
        {
            this.isOpen = true;
        }
        else
        {
            this.isOpen = false;
        }

    }

    protected virtual void Animating()
    {
        this.animator.SetBool("isOpen", this.isOpen);
    }
}
