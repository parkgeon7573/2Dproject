using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemController : MonoBehaviour
{   
    HeroController m_hero;
    [SerializeField]
    InGameItemManager.ItemType m_type;
    [SerializeField]
    SpriteRenderer m_sprRenderer;
    [SerializeField]
    TweenRotation m_rotTween;
    [SerializeField]
    AnimationCurve m_xCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
    [SerializeField]
    AnimationCurve m_yCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);    
    [SerializeField]
    Vector3 m_from;
    [SerializeField]
    Vector3 m_to;
    [SerializeField]
    float m_duration = 1f;
    float m_time;
    static float m_targetPosY = -8.5f;
    static float m_height = 3f;
    static float m_maxDuration = 2f;
    static float m_maxDist = 14.5f;
    float m_angle;
    bool m_isMagnet;
    float m_magnetPower = 12f;
    public InGameItemManager.ItemType Type { get { return m_type; } private set { m_type = value; } }
    public void SetItem(Vector3 pos, HeroController target, InGameItemManager.ItemType type, Sprite icon)
    {
        var dir = (target.transform.position - pos);
        m_hero = target;
        m_isMagnet = false;
        m_from = pos;
        m_to = m_from + dir.normalized * 1.5f;
        m_to.y = m_targetPosY;
        m_time = 0f;
        m_type = type;
        m_duration = m_maxDuration * ((m_to - pos).magnitude / m_maxDist);
        m_sprRenderer.sprite =  icon;
        transform.localRotation = Quaternion.identity;
    }
    public void SetItemEffect(HeroController hero)
    {
        switch (m_type)
        {
            case InGameItemManager.ItemType.Coin:
                SoundManager.Instance.PlaySFX(SoundManager.SfxList.get_coin);
                GameUIManager.Instance.SetCoinCont(1);
                break;
            case InGameItemManager.ItemType.Gem_Blue:
            case InGameItemManager.ItemType.Gem_Red:
            case InGameItemManager.ItemType.Gem_Green:
                SoundManager.Instance.PlaySFX(SoundManager.SfxList.get_gem);
                GameUIManager.Instance.SetCoinCont((int)m_type * 10);
                break;
            case InGameItemManager.ItemType.Invincible:
                SoundManager.Instance.PlaySFX(SoundManager.SfxList.get_invincible);
                hero.SetBuff(BuffType.Invincible);
                break;
            case InGameItemManager.ItemType.Magnet:
                SoundManager.Instance.PlaySFX(SoundManager.SfxList.get_item);
                hero.SetBuff(BuffType.Magnet);
                break;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Magnet"))
        {
            m_isMagnet = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isMagnet)
        {
            var valueX = m_xCurve.Evaluate(m_time);
            var valueY = m_yCurve.Evaluate(m_time);
            transform.position = m_from * (1f - valueX) + m_to * valueX + Vector3.up * (valueY * m_height);
            m_time += Time.deltaTime / m_duration;
            if (m_time > 1f)
            {
                InGameItemManager.Instance.RemoveItem(this);
            }
        }
        else
        {
            transform.position += (m_hero.transform.position - transform.position).normalized * m_magnetPower * Time.deltaTime;
        }
        if(Type >= InGameItemManager.ItemType.Gem_Red && Type <= InGameItemManager.ItemType.Gem_Blue)
        {
            transform.eulerAngles = new Vector3(0f, 0f, m_angle += 0.5f);
        }
    }
}
