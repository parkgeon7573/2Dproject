using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxController : MonoBehaviour
{
    ParticleSystem[] m_particles;    
    IEnumerator Coroutine_CheckPlaying()
    {
        bool isPlaying = false;
        while (true)
        {
            yield return null;
            isPlaying = true;
            for (int i = 0; i < m_particles.Length; i++)
            {
                if (m_particles[i].isPlaying)
                {
                    isPlaying = true;
                    break;
                }
            }
            if (!isPlaying)
            {
                gameObject.SetActive(false);
                yield break;
            }

        }
    }
    bool m_isFirst;
    // Start is called before the first frame update
 
   
    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine("Coroutine_CheckPlaying");
    }
    private void OnDisable()
    {
        if (m_isFirst)
        { 
            m_isFirst = false;
            return;
        }
        EffectPool.Instance.RemoveEffect(this);
    }
    void Awake()
    { 
        m_isFirst = true;
        m_particles = GetComponentsInChildren<ParticleSystem>();
    }
   
}
