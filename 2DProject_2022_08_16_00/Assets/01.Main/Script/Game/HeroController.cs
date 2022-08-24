using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    GameObject m_projectilePrefab;
    [SerializeField]
    Transform m_firepos;
    Vector2 m_dir;
    [SerializeField]
    float m_speed = 2f;
    int frame;
    // Start is called before the first frame update
    void CreateProjectile()
    {
        var obj = Instantiate(m_projectilePrefab);
        obj.transform.position = m_firepos.position;
    }
    void Start()
    {
        StartCoroutine(Coroutine_Fire(0.2f));
    }
    IEnumerator Coroutine_Fire(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            CreateProjectile();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
        m_dir = new Vector2(Input.GetAxis("Horizontal"), 0f);
        transform.position += (Vector3)m_dir * m_speed * Time.deltaTime;
    }
}
