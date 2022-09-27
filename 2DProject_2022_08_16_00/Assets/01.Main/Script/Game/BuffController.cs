using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    None = -1,
    Invincible,
    Magnet,
    Max
}
public class BuffController : MonoBehaviour
{
    [SerializeField]
    HeroController m_hero;
    Dictionary<BuffType, float> m_buffTable = new Dictionary<BuffType, float>();
    Dictionary<BuffType, float> m_buffList = new Dictionary<BuffType, float>();
    public void SetBuff(BuffType buff)
    {
        if (m_buffList.ContainsKey(buff))
        {
            m_buffList[buff] = 0f;
        }
        else
        {
            m_buffList.Add(buff, 0f);
            StartCoroutine(Coroutine_BuffProcess(buff));
        }
    }
    IEnumerator Coroutine_BuffProcess(BuffType buff)
    {
        switch (buff)
        {
            case BuffType.Invincible:
                GameStateManager.Instance.SetState(GameState.Invinvible);
                break;
            case BuffType.Magnet:
                m_hero.SetMagnetEffect(true);
                break;
        }
        while (true)
        {
            yield return null;
            m_buffList[buff] += Time.deltaTime;            
            if (m_buffList[buff] > m_buffTable[buff])
            {                
                switch (buff)
                {
                    case BuffType.Invincible:
                        GameStateManager.Instance.SetState(GameState.Normal);
                        break;
                    case BuffType.Magnet:
                        m_hero.SetMagnetEffect(false);
                        break;
                }
                m_buffList.Remove(buff);
                yield break;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        m_buffTable.Add(BuffType.Invincible, 3f);
        m_buffTable.Add(BuffType.Magnet, 5f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
