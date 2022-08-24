using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Popup_Ok : MonoBehaviour
{
    [SerializeField]
    UILabel m_title;
    [SerializeField]
    UILabel m_body;
    [SerializeField]
    UILabel m_okBtnLabel;
    Action m_okDel;
    
    // Start is called before the first frame update
    public void SetUI(string title, string body, Action okDel, string okBtnText = "Ok")
    {
        m_title.text = title;
        m_body.text = body;
        m_okBtnLabel.text = okBtnText;
        m_okDel = okDel;
    }
    public void OnPressOk()
    {
        if (m_okDel != null)
            m_okDel();
        else
            PopupManager.Instance.Close_Popup();
    }
    void Start()
    {

    }
    void Update()
    {

    }
}
