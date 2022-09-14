 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] m_eyeSprites;
    Animator m_animator;
    Vector3 m_dir = Vector3.down;
    MonsterManager.MonsterType m_type;
    [SerializeField]
    float m_speed = 1.5f;
    int m_hp;
    public MonsterManager.MonsterType Type { get { return m_type; } set { m_type = value; } }
    public void Move()
    {
        transform.position += m_dir * m_speed * Time.deltaTime;
    }
    public void SetDamage(int damage)
    {
        m_animator.Play("Hit", 0, 0f);
        SetAngryEyes();
        if (IsInvoking("SetDefaultEyes"))
            CancelInvoke("SetAngryEyes");   
        Invoke("SetDefaultEyes", 1f);
        m_hp -= damage;
        if(m_hp <= 0)
        {
            EffectPool.Instance.CreateEffect(transform.position);
            InGameItemManager.Instance.CreateItem(transform.position);
            MonsterManager.Instance.RemoveMonster(this);
        }
    }
    void SetDefaultEyes()
    {
        m_eyeSprites[0].enabled = true;
        m_eyeSprites[1].enabled = true;
        m_eyeSprites[2].enabled = false;
        m_eyeSprites[3].enabled = false;
    }
    void SetAngryEyes()
    {
        m_eyeSprites[0].enabled = false;
        m_eyeSprites[1].enabled = false;
        m_eyeSprites[2].enabled = true;
        m_eyeSprites[3].enabled = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Background_Bottom"))
        {
            MonsterManager.Instance.RemoveMonster(this);
        }
    }
    void OnEnable()
    {
        m_hp = (int)(m_type + 1) + 1;
        SetDefaultEyes();
    }
    // Start is called before the first frame update
    void Start()
    {        
        m_animator = GetComponent<Animator>();
    }
}
