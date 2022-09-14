using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameItemManager : SingletonMonoBehaviour<InGameItemManager>
{
    public enum ItemType
    {
        Coin,
        Gem_Red,
        Gem_Green,
        Gem_Blue,
        Invincible,
        Magnet,
        Max
    }
    [SerializeField]
    HeroController m_hero;
    [SerializeField]
    GameObject m_gameItemPrefab;
    GameObjectPool<InGameItemController> m_itemPool;
    [SerializeField]
    List<Sprite> m_iconSprites;
    public void RemoveItem(InGameItemController item)
    {
        m_itemPool.Set(item);
        item.gameObject.SetActive(false);
    }
    public void CreateItem(Vector3 pos)
    {
        var type = (ItemType)Random.Range((int)ItemType.Coin, (int)ItemType.Max);

        var item = m_itemPool.Get();
        item.gameObject.SetActive(true);
        item.SetItem(pos, m_hero.transform.position, type, m_iconSprites[(int)type]);
    }
    // Start is called before the first frame update
    protected override void OnStart()
    {
        m_gameItemPrefab = Resources.Load<GameObject>("Prefab/InGameItem/Item");
        m_itemPool = new GameObjectPool<InGameItemController>(10, () =>
        {
            var obj = Instantiate(m_gameItemPrefab);
            obj.SetActive(false);
            obj.transform.SetParent(transform);
            var item = obj.GetComponent<InGameItemController>();
            return item;
        });
    }
    
}
