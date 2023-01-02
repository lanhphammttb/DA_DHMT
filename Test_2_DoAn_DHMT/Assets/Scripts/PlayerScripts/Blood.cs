using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class Blood : MonoBehaviour
{
    public GameObject TextObject;
    public Image healthBar;
    float health = 100.0f;
    float healthMax = 100.0f;
    public GameObject FireExplosion;
    // Start is called before the first frame update
    private void Awake()
    {
        FireExplosion.SetActive(false);        
    }
    void Start()
    {
        Hit(0.0f);
        TextObject.SetActive(false);

        
    }

    public void Hit(float _damage)
    {
        health = health - _damage;
        healthBar.fillAmount = health / healthMax;
    }

    private void OnTriggerStay(Collider other) //orther collider c?a ð?i tý?ng mà player va ch?m
    {
        if (other.gameObject.CompareTag("isObstacle"))
        {
            Hit(0.3f);
        }

    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("isFire"))
        {
            Hit(100.0f);
            FireExplosion.SetActive(true);
        }        
    }
    // Update is called once per frame
    void Update()
    {
        if (healthBar.fillAmount == 0.0f)
        {
            TextObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1.0f;
        }
    }
}
