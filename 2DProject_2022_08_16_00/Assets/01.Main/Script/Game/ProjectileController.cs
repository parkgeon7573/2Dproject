using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;
    HeroController m_hero;
    int m_power = 1;
    
    public void InitProjectile(HeroController hero)
    {
        m_hero = hero;
    }
    void RemoveProjectile()
    {
        m_hero.RemoveProjectile(this);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster"))
        {
            RemoveProjectile();
            var mon = collision.gameObject.GetComponent<MonsterController>();
            mon.SetDamage(m_power);

        }
    }
    void OnEnable()
    {
        Invoke("RemoveProjectile", 1f);
    }
    void OnDisable()
    {
        if(IsInvoking("RemoveProjectile"))
        {
            CancelInvoke("RemoveProjectile");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * m_speed * Time.deltaTime;
    }
}