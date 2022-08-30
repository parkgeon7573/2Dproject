using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 10f;
    HeroController m_hero;
    public void InitProjectile(HeroController hero)
    {
        m_hero = hero;
    }
    void RemoveProjectile()
    {
        m_hero.RemoveProjectile(this);
    }
    void OnEnable()
    {
        Invoke("RemoveProjectile", 1f);
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * m_speed * Time.deltaTime;
    }
}