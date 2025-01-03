using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryWorldItem : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Refresh(ItemData itemData)
    {
        _itemData = itemData;
        _sr.sprite = _itemData.icon;
        gameObject.name = _itemData.name;
    }

    public void Pop()
    {
        float randomXVelocity = Random.Range(-5, 5);
        float randomYVelocity = Random.Range(5, 10);
        _rb.velocity = new Vector2(randomXVelocity, randomYVelocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>())
        {
            Inventory.Instance.AddItem(_itemData);
            DataManager.Instance.InventoryData.AddItemData(_itemData);
            DataManager.Instance.SaveData(E_DataModel.Inventory);
            Destroy(gameObject);
        }
    }
}