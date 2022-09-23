using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GameUIManager : SingletonMonoBehaviour<GameUIManager>
{
    [SerializeField]
    UILabel m_distScoreLabel;
    [SerializeField]
    UILabel m_huntScoreLabel;
    [SerializeField]
    UILabel m_coinCountLabel;
    uint m_distScore;
    uint m_huntScore;
    uint m_coinCount;
    StringBuilder m_sb = new StringBuilder();
    public void SetDistScore(float dist)
    {        
        m_distScore = (uint)Mathf.CeilToInt(dist * 1000f);
        m_distScoreLabel.text = m_sb.AppendFormat("{0:n0}", m_distScore).ToString();
        m_sb.Clear();
    }
    public void SetHuntScore(int score)
    {
        m_huntScore += (uint)score;
        m_huntScoreLabel.text = m_sb.AppendFormat("{0:n0}", m_huntScore).ToString();
        m_sb.Clear();
    }
    public void SetCoinCont(int count)
    {
        m_coinCount += (uint)count;
        m_coinCountLabel.text = m_sb.AppendFormat("{0:n0}", m_coinCount).ToString();
        m_sb.Clear();
    }

    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_distScore = 0;
        SetDistScore(0f);
        m_huntScore = 0;
        SetHuntScore(0);
        m_coinCount = 0;
        SetCoinCont(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
