using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PopupManager : SingletonDontDestroy<PopupManager>
{
    [SerializeField]
    GameObject m_popupOkCancelPrefab;
    [SerializeField]
    GameObject m_popupOkPrefab;
    const int m_startDepth = 1000;
    const int m_popupDepthGap = 10;
    List<GameObject> m_popupList = new List<GameObject>();
    public bool IsOpenPopup { get { return m_popupList.Count > 0; } }

    public void Open_PopupOkCancel(string title, string body, Action okDel = null, Action cancelDel = null, string okBtnText = "Ok", string cancelBtnText = "Cancel")
    {
        var obj = Instantiate(m_popupOkCancelPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_startDepth + m_popupList.Count * m_popupDepthGap + i;
        }
        var popup = obj.GetComponent<Popup_OkCancel>();
        popup.SetUI(title, body, okDel, cancelDel, okBtnText, cancelBtnText);
        m_popupList.Add(obj);
    }
    public void Open_PopupOk(string title, string body, Action okDel = null, Action cancelDel = null, string okBtnText = "Ok")
    {
        var obj = Instantiate(m_popupOkPrefab);
        obj.transform.SetParent(transform);
        obj.transform.localPosition = Vector3.zero;
        var panels = obj.GetComponentsInChildren<UIPanel>();
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].depth = m_startDepth + m_popupList.Count * m_popupDepthGap + i;
        }
        var popup = obj.GetComponent<Popup_Ok>();
        popup.SetUI(title, body, okDel, okBtnText);
        m_popupList.Add(obj);
    }
    public void Close_Popup()
    {
        if(m_popupList.Count > 0)
        {
            Destroy(m_popupList[m_popupList.Count - 1].gameObject);
            m_popupList.RemoveAt(m_popupList.Count - 1);
        }
    }
    // Start is called before the first frame update
    protected override void Onstart()
    {
        m_popupOkCancelPrefab = Resources.Load<GameObject>("Popup/Popup_OkCancel");
        m_popupOkPrefab = Resources.Load<GameObject>("Popup/Popup_Ok");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (UnityEngine.Random.Range(1, 101) % 2 == 0)
                Open_PopupOkCancel("Notice", "안녕하세요. 수업을 종료하시겠습니까?", null, null, "예", "아니오");
            else
                Open_PopupOk("안내", "금일은 갑작스러운 폭우로인해 수업이 늦게 시작되었습니다");
        }
    }
}
