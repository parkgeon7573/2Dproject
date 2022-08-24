using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class LoadSceneManager : SingletonDontDestroy<LoadSceneManager>
{
    public enum SceneState
    {
        None = -1,
        Title,
        Lobby,
        Game,
        Max
    }
   
    [SerializeField]
    GameObject m_loadingObj;
    UIProgressBar m_loadingBar;
    UILabel m_loadingPregress;
    AsyncOperation m_loadingInfo;
    StringBuilder m_sb = new StringBuilder();
    SceneState m_state;
    SceneState m_loadState = SceneState.None;
    float m_time;

    public void LoadSceneAsync(SceneState state)
    {
        m_loadingObj.SetActive(true);
        if (m_loadState != SceneState.None) return;
        m_loadState = state;
        m_loadingInfo = SceneManager.LoadSceneAsync((int)m_loadState);

    }

    void CloseUI()
    {
        m_loadingObj.SetActive(false);
    }    
    // Start is called before the first frame update
    protected override void Onstart()
    {
        m_loadingObj.SetActive(false);
        m_loadingBar = m_loadingObj.GetComponentInChildren<UIProgressBar>();
        m_loadingPregress = m_loadingObj.GetComponentInChildren<UILabel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PopupManager.Instance.IsOpenPopup)
            {
                PopupManager.Instance.Close_Popup();
            }
            else
            {
                switch (m_state)
                {
                    case SceneState.Title:
                        PopupManager.Instance.Open_PopupOkCancel("[000000]Notice[-]", "[000000]게임을 종료하시겠습니까?[-]", () =>
                        {
#if UNITY_EDITOR
                            EditorApplication.isPlaying = false;
#else
                             Application.Quit();
#endif
                        }, null, "예", "아니오");
                        break;
                    case SceneState.Lobby:
                        PopupManager.Instance.Open_PopupOkCancel("[000000]Notice[-]", "[000000]타이틀 화면으로 돌아가시겠습니까?[-]", () =>
                        {
                            LoadSceneAsync(SceneState.Title);
                            PopupManager.Instance.Close_Popup();
                        }, null, "예", "아니오");
                        break;
                    case SceneState.Game:
                        PopupManager.Instance.Open_PopupOkCancel("[000000]Notice[-]", "[000000]현재 게임을 종료하고 로비로 돌아가시겠습니까? /r/n저장되지 않은 내용은 모두 사라집니다.[-]", () => 
                        {
                            LoadSceneAsync(SceneState.Lobby);
                            PopupManager.Instance.Close_Popup();
                        }, null, "예", "아니오");
                        break;
                }
            }
        }
        if (m_loadingInfo != null)
        {
            if (m_loadingInfo.isDone)
            {
                m_loadingInfo = null;
                m_state = m_loadState;
                m_loadState = SceneState.None;
                m_loadingBar.value = 1;
                m_sb.Append("100%");
                m_loadingPregress.text = m_sb.ToString();
                m_sb.Clear();
                Invoke("CloseUI", 1f);
            }
            else
            {
                m_loadingBar.value = m_loadingInfo.progress;
                m_sb.Append(Mathf.CeilToInt(m_loadingInfo.progress * 100.0f));
                m_sb.Append('%');
                m_loadingPregress.text = m_sb.ToString();
                m_sb.Clear();
                
            }
        }
    }
}
