using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraResolution : MonoBehaviour
{
    public enum ClearMode
    {
        None,
        Clear
    }
    [SerializeField]
    ClearMode m_mode;
    Camera m_camera;
    [SerializeField]
    Vector2 m_referenceResolution = new Vector2(960f, 640f);

    void OnPreCull()
    {
        if (m_mode == ClearMode.None) return;
        GL.Clear(true, true, Color.black);    
    }

    // Start is called before the first frame update
    void Start()
    {
        m_camera = GetComponent<Camera>();
        Rect viewRect = new Rect(0f, 0f, 1f, 1f);
        float curAspectRatio = (float)Screen.width / (float)Screen.height;
        float fixedAsprecRatio = m_referenceResolution.x / m_referenceResolution.y;

        if (curAspectRatio == fixedAsprecRatio)
        {
            m_camera.rect = new Rect(0f, 0f, 1f, 1f);
        }
        else if (curAspectRatio > fixedAsprecRatio)
        {
            float w = fixedAsprecRatio / curAspectRatio;
            float x = (1 - w) / 2;
            m_camera.rect = new Rect(x, 0f, w, 1f);
        }        
        else if(curAspectRatio < fixedAsprecRatio)
        {
            float h = curAspectRatio / fixedAsprecRatio;
            float y = (1 - h) / 2;
            m_camera.rect = new Rect(0f, y, 1f, h);
        }
    }

}
