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
    [SerializeField]
    CameraShake m_cameraShake;
    GameState m_state;
    public void SetState(GameState state)
    {
        m_state = state;
        switch (state)
        {
            case GameState.Normal:
                m_bgCtr.SetSpeed(1f);
                m_hero.SetInvincibleEffect(false);
                MonsterManager.Instance.ResetCreateMonster(1f);
                break;
            case GameState.Invinvible:
                m_cameraShake.Shake();
                m_bgCtr.SetSpeed(6f);
                m_hero.SetInvincibleEffect(true);
                MonsterManager.Instance.ResetCreateMonster(5f);
                break;
            case GameState.Result:
                break;
        }
    }

    public GameState GetState()
    {
        return m_state;
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
