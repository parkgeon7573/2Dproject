using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGController : MonoBehaviour
{
    [SerializeField]
    float m_speed = 0.1f;
    SpriteRenderer m_sprRenderer;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayBGM(SoundManager.BgmList.dragon_flight);
        m_sprRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        m_sprRenderer.material.mainTextureOffset += Vector2.up * m_speed * Time.deltaTime;
        GameUIManager.Instance.SetDistScore(m_sprRenderer.material.mainTextureOffset.y);
    }
}
