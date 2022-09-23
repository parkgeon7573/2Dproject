using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 0.1f;
    float m_scale = 1f;
    SpriteRenderer m_sprRenderer;

    public void SetSpeed(float scale)
    {
        m_scale = scale;
    }
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BgmList.dragon_flight);
        m_sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_sprRenderer.material.mainTextureOffset += Vector2.up * m_speed * m_scale * Time.deltaTime;
        GameUIManager.Instance.SetDistScore(m_sprRenderer.material.mainTextureOffset.y);
    }
}
