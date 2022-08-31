using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : SingletonMonoBehaviour<MonsterManager>
{
    [SerializeField]
    GameObject m_monsterPrefab;
    GameObjectPool<MonsterController> m_monsterPool;
    // Start is called before the first frame update
    protected override void Onstart()
    {
        m_monsterPool = new GameObjectPool<MonsterController>(5, () =>
        {
            var obj = Instantiate(m_monsterPrefab);
            obj.gameObject.SetActive(false);
            var mon = obj.GetComponent<MonsterController>();
            return mon; 
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
