using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Normal,
    Invinvible,
    Result,
    Max
}
public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{
    [SerializeField]
    BGController m_bgCtr;
    [SerializeField]
    HeroController m_hero;
    GameState m_state;
    public void SetState(GameState state)
    {
        m_state = state;
        switch (state)
        {
            case GameState.Normal:
                 break;
            case GameState.Invinvible:
                m_bgCtr.SetSpeed(5f);
                m_hero.SetInvincibleEffect(true);
                MonsterManager.Instance.ResetCreateMonster(5f);
                break;
            case GameState.Result:
                break;
        }
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
