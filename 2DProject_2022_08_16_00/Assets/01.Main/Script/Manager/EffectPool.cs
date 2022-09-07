using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : SingletonMonoBehaviour<EffectPool>
{
    [SerializeField]
    GameObject m_fxEcpolosionPrefab;
    GameObjectPool<VfxController> m_effectPool;
    // Start is called before the first frame update
    public void CreateEffect(Vector3 pos)
    {
        var effect = m_effectPool.Get();
        effect.transform.position = pos;
        effect.gameObject.SetActive(true);
    }
    public void RemoveEffect(VfxController effect)
    {
        m_effectPool.Set(effect);
    }
    protected override void OnStart()
    {
        m_effectPool = new GameObjectPool<VfxController>(5, () =>
        {
            var obj = Instantiate(m_fxEcpolosionPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var effect = obj.GetComponent<VfxController>();
            return effect;
        });
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
