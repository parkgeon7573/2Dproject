using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    GameObjectPool<ProjectileController> m_projectilePool;
    [SerializeField]
    GameObject m_fxMagnetObj;
    [SerializeField]
    GameObject m_projectilePrefab;
    [SerializeField]
    Transform m_firepos;
    [SerializeField]
    Camera m_mainCamera;
    Rigidbody2D m_rigidbody;
    Vector2 m_dir;
    [SerializeField]
    float m_speed = 2f;
    bool m_isDrag;
    Vector3 m_startPos;
    Vector3 m_prevPos;

    public void SetMagnetEffect(bool active)
    {
        m_fxMagnetObj.SetActive(active);
    }
    public void RemoveProjectile(ProjectileController projectile)
    {
        projectile.gameObject.SetActive(false);
        m_projectilePool.Set(projectile);
    }
    // Start is called before the first frame update
    void CreateProjectile()
    {
        var projectile = m_projectilePool.Get();
        projectile.gameObject.SetActive(true);
        projectile.transform.position = m_firepos.position;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("item"))
        {
            var item = collision.GetComponent<InGameItemController>();
            item.SetItemEffect(this);
            InGameItemManager.Instance.RemoveItem(item);
        }
    }
    void Start()
    {
        m_mainCamera = Camera.main;
        m_rigidbody = GetComponent<Rigidbody2D>();
        InvokeRepeating("CreateProjectile", 1f, 0.1f);
        m_projectilePool = new GameObjectPool<ProjectileController>(9, () =>
        {
            var obj = Instantiate(m_projectilePrefab);
            obj.SetActive(false);
            var projectile = obj.GetComponent<ProjectileController>();
            projectile.InitProjectile(this);
            return projectile;
        });
        SetMagnetEffect(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_isDrag = true;
            m_startPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_isDrag = false;
        }
        if (m_isDrag)
        {
            var endPos = m_mainCamera.ScreenToWorldPoint(Input.mousePosition);
            m_dir = endPos - m_startPos;
            m_dir.y = 0f;
            m_prevPos = transform.position;
            transform.position += (Vector3)m_dir;
            var dir = transform.position - m_prevPos;
            var hit = Physics2D.Raycast(m_prevPos, dir.normalized, Mathf.Abs(dir.x), 1 << LayerMask.NameToLayer("Background"));
            if(hit.collider != null)
            {
                if(dir.x < 0f && !hit.collider.CompareTag("Background_Right")||
                    dir.x > 0f && !hit.collider.CompareTag("Background_Left"))
                transform.position = hit.point;
            }
            m_startPos = endPos;

            /*var viewPoint = m_mainCamera.WorldToViewportPoint(transform.position);
            if (viewPoint.x < 0)
            {
                viewPoint.x = 0;
                transform.position = m_mainCamera.ViewportToWorldPoint(viewPoint);
            }
            else if (viewPoint.x > 1f)
            {
                viewPoint.x = 1;
                transform.position = m_mainCamera.ViewportToWorldPoint(viewPoint);
            }*/           

        }
        
        /*m_dir = new Vector2(Input.GetAxis("Horizontal"), 0f);
        transform.position += (Vector3)m_dir * m_speed * Time.deltaTime;*/
    }
}
