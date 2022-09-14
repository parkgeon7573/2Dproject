using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemController : MonoBehaviour
{
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
    public InGameItemManager.ItemType Type { get { return m_type; } private set { m_type = value; } }
    public void SetItem(Vector3 pos, Vector3 target, InGameItemManager.ItemType type, Sprite icon)
    {
        var dir = (target - pos);
        m_from = pos;
        m_to = m_from + dir.normalized * 1.5f;
        m_to.y = m_targetPosY;
        m_time = 0f;
        m_type = type;
        m_duration = m_maxDuration * ((m_to - pos).magnitude / m_maxDist);
        m_sprRenderer.sprite =  icon;
        transform.localRotation = Quaternion.identity;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_time += Time.deltaTime / m_duration;
        var valueX = m_xCurve.Evaluate(m_time);
        var valueY = m_yCurve.Evaluate(m_time);
        transform.position = m_from * (1f - valueX) + m_to * valueX + Vector3.up * (valueY * m_height);
        if(m_time > 1f)
        {
            InGameItemManager.Instance.RemoveItem(this);
        }
        if(Type >= InGameItemManager.ItemType.Gem_Red && Type <= InGameItemManager.ItemType.Gem_Blue)
        {
            transform.eulerAngles = new Vector3(0f, 0f, m_angle += 0.5f);
        }
    }
}
