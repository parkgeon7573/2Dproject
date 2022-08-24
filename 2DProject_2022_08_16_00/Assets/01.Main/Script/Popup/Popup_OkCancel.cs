using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Popup_OkCancel : MonoBehaviour
{
    [SerializeField]
    UILabel m_title;
    [SerializeField]
    UILabel m_body;
    [SerializeField]
    UILabel m_okBtnLabel;
    [SerializeField]
    UILabel m_cancelBtnLabel;
    Action m_okDel;
    Action m_cancelDel;
    // Start is called before the first frame update
    public void SetUI(string title, string body, Action OkDel = null, Action cancelDel = null, string okBtnText = "Ok", string cancelBtnText = "Cancel")
    {
        m_title.text = title;
        m_body.text = body;
        m_okBtnLabel.text = okBtnText;
        m_cancelBtnLabel.text = cancelBtnText;
        m_okDel = OkDel;
        m_cancelDel = cancelDel;
        
    }
    public void OnPressOk()
    {
        if (m_okDel != null)
            m_okDel();
        else
            PopupManager.Instance.Close_Popup();
    }
    public void OnPressCancel()
    {
        if(m_cancelDel != null)        
            m_cancelDel();
        else
            PopupManager.Instance.Close_Popup();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
   
}
