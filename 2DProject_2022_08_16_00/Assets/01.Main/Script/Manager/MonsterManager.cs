using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    public enum MonsterType
    {
        White,
        Yellow,
        Pink,
        Bomb,
        Max
    }
    [SerializeField]
    GameObject[] m_monsterPrefabs;
    List<MonsterController> m_monsterList = new List<MonsterController>();
    Dictionary<MonsterType,GameObjectPool<MonsterController>> m_monsterPool = new Dictionary<MonsterType, GameObjectPool<MonsterController>>();
    Vector2 m_startPos = new Vector2(-2.7f, 6f);
    float m_posGap = 1.35f;
    public void RemoveMonster(MonsterController mon)
    { 
        if (m_monsterList.Remove(mon)) 
        {
            m_monsterPool[mon.Type].Set(mon);
        }
        mon.gameObject.SetActive(false);
    }
    
    void CreateMonsters()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateMonster(m_startPos + Vector2.right * i * m_posGap);
        }
    }
    void CreateMonster(Vector3 pos)
    {
        var mon = m_monsterPool[(MonsterType)Random.Range((int)MonsterType.White, (int)MonsterType.Max)].Get();
        mon.transform.position = pos;
        mon.gameObject.SetActive(true);
        m_monsterList.Add(mon);
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_monsterPrefabs = Resources.LoadAll<GameObject>("Prefab/Monster");
        
        foreach (var prefab in m_monsterPrefabs)
        {
            var results = prefab.name.Split('.');
            MonsterType type = (MonsterType)(int.Parse(results[0]) - 1);
            GameObjectPool<MonsterController> pool = new GameObjectPool<MonsterController>(2, () =>
            {
                var obj = Instantiate(prefab);
                obj.gameObject.SetActive(false);
                obj.transform.SetParent(transform);
                var mon = obj.GetComponent<MonsterController>();
                mon.Type = type;
                return mon;
            });
            m_monsterPool.Add(type, pool);
        }
        /*m_monsterPool = new GameObjectPool<MonsterController>(10, () =>
        {            
            var obj = Instantiate(m_monsterPrefabs);
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(transform);
            var mon = obj.GetComponent<MonsterController>();
            return mon; 
        });*/
        InvokeRepeating("CreateMonsters", 2f, 3f);
    }
    

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i< m_monsterList.Count; i++)
        {
            m_monsterList[i].Move();
        }
    }
}
