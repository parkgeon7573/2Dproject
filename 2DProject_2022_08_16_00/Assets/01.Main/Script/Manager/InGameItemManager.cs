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
    
    int[] m_itemTable = { 51, 3, 2, 1, 41, 2 };
    float[] m_itemTable2 = { 91.0f, 3f, 2.5f, 0.8f, 0.5f, 2.2f };
    public void RemoveItem(InGameItemController item)
    {
        m_itemPool.Set(item);
        item.gameObject.SetActive(false);
    }
    public void CreateItem(Vector3 pos)
    {
        var type = (ItemType)Util.GetPriority(m_itemTable);//(ItemType)Random.Range((int)ItemType.Coin, (int)ItemType.Max);

        var item = m_itemPool.Get();
        item.gameObject.SetActive(true);
        item.SetItem(pos, m_hero, type, m_iconSprites[(int)type]);
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
