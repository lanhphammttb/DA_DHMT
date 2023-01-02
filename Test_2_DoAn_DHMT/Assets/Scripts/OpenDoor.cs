using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] protected GameObject DoorObject;
    public GameObject textPickKey;
    private bool isKey;
    private bool isDoor;
    [SerializeField] protected GameObject textInfo;
    [SerializeField] protected Button buttonYes;
    [SerializeField] protected Button buttonNo;
    [SerializeField] protected GameObject key;
    //[SerializeField] protected bool Pick;
    //[SerializeField] protected Animator animator;

    //protected virtual void Reset()
    //{
    //    this.LoadComponents();
    //}

    //protected virtual void Awake()
    //{
    //    this.LoadComponents();
    //}

    //protected void LoadComponents()
    //{
    //    this.LoadAnimator();
    //}

    private void Start()
    {
        isKey = false;
        isDoor = false;
        textPickKey.SetActive(false);
        textInfo.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        //key.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Key"))
        {
                Debug.Log("Starting see key:    " + Time.time);
                isKey = true;
                textPickKey.SetActive(true);
                textInfo.SetActive(false);
                buttonYes.gameObject.SetActive(false);
                buttonNo.gameObject.SetActive(false);
                StartCoroutine(AfterSeeKey(3.0f));
        }

        if (other.gameObject.CompareTag("isNPC"))
        {
            textInfo.SetActive(true);
            buttonYes.gameObject.SetActive(true);
            buttonNo.gameObject.SetActive(true);
            Debug.Log("Starting meet NPC:    " + Time.time);
            StartCoroutine(AfterMeetNPC(5.0f));
        }

        IEnumerator AfterMeetNPC(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            textInfo.SetActive(false);
            buttonYes.gameObject.SetActive(false);
            buttonNo.gameObject.SetActive(false);          
            Debug.Log("Done " + Time.time);
        }
        IEnumerator AfterSeeKey(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            textPickKey.SetActive(false);
            key.gameObject.SetActive(false);
            Debug.Log("Done " + Time.time);
        }

    }
    //protected virtual void LoadAnimator()
    //{
    //    if (this.animator != null) return;
    //    this.animator = GetComponent<Animator>();
    //    Debug.Log(transform.name + ": LoadAnimator", gameObject);
    //}
    //protected virtual void Animating()
    //{
    //    this.animator.SetBool("Pick", this.Pick);
    //}
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            isDoor = true;
        }        
    }

    private void Update()
    {
        //this.Animating();
        if (isKey && isDoor)
        {
            DoorObject.transform.Translate(new Vector3(-0.7f, 0.0f, -1.2f));

            DoorObject.transform.Rotate(0, -124.879f, 0);
            isKey= false;
            isDoor= false;
        }
    }
}

