using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool<T> where T : MonoBehaviour
{
    Queue<T> m_pool = new Queue<T>();
    int m_count;
    Func<T> m_CreateFunc;
    public GameObjectPool(int count, Func<T> createDel)
    {
        m_count = count;
        m_CreateFunc = createDel;
    }
    void Allocation()
    {
        for (int i = 0; i < m_count; i++)
        {
            m_pool.Enqueue(m_CreateFunc());
        }
    }
    public T Get()
    {
        if (m_pool.Count > 0)
            return m_pool.Dequeue();
        return m_CreateFunc();
    }
    public void Set(T obj)
    {
        m_pool.Enqueue(obj);
    }
}