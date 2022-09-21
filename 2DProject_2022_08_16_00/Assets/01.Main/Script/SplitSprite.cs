using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SplitSprite : MonoBehaviour
{
    [SerializeField]
    UITexture m_texture;
    Sprite[] m_sprties;
    // Start is called before the first frame update
    void Start()
    {
        m_sprties = Resources.LoadAll<Sprite>("Fonts/text_01");
        Debug.Log(m_sprties.Length);
        for(int i = 0; i < m_sprties.Length; i++)
        {
            var spr = m_sprties[i];
            Texture2D texture = new Texture2D((int)spr.rect.width, (int)spr.rect.height, TextureFormat.ARGB32, false);
            for (int y = 0; i < texture.height; y++)
            {
                for(int x = 0; x < texture.width; x++)
                {
                    texture.SetPixel(x, y, spr.texture.GetPixel((int)spr.rect.x + x, (int)spr.rect.y + y));
                }
            }
            texture.Apply();
            var image = texture.EncodeToPNG();
            string dataPath = null;
#if UNITY_EDITOR
            dataPath = Application.dataPath;
#else
            dataPath = Application.persistentDataPath;
#endif
            string path = string.Format(@"{0}\FontImage\image_{1:00}.png", dataPath, i + 1);
            File.WriteAllBytes(path, image);
            m_texture.mainTexture = texture;
            m_texture.MakePixelPerfect();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
