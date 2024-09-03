using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(UniqueID))]
public class ItemPickUp : MonoBehaviour, IDataPersistence
{
    public Transform player;

    [SerializeField] private float pickupDistance = 1.5f;
    [SerializeField] private float tamNhatx = 1f;
    [SerializeField] private float tamNhaty = 1f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private InventoryItemData ItemData;
    [SerializeField] private TextMeshProUGUI itemCount;

    private CapsuleCollider2D myCollider;
    private bool collected;
    private bool canBePickedUp;

    public InventorySlot AssignedInventorySlot;
    [SerializeField] private ItemPickUpSaveData itemSaveData;

    private string id;
    public int soluong = 1;

    private void Awake()
    {
        QuanliLuutru.instance.LoadGame();
        id = GetComponent<UniqueID>().ID;
        itemSaveData = new ItemPickUpSaveData(ItemData, transform.position, transform.rotation);

        myCollider = GetComponent<CapsuleCollider2D>();
        myCollider.isTrigger = true;
        myCollider.size = new Vector2(tamNhatx, tamNhaty);
        StartCoroutine(EnablePickupAfterDelay(1f));
    }

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("Không tìm được Player");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canBePickedUp) return; // Nếu không thể nhặt vật phẩm thì thoát khỏi hàm

        var inventory = collision.transform.GetComponent<PlayerInventoryHolder>();

        if (inventory == null) return; // Nếu không tìm thấy kho đồ của người chơi thì thoát

        if (inventory.AddToInventory(ItemData, soluong))
        {
            ItemCollected();
        }
    }

    private void Update()
    {
        if (!canBePickedUp) return; // Nếu không thể nhặt vật phẩm thì thoát khỏi hàm

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > pickupDistance) return;

        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    public void LoadData(GameData data)
    {
        if (data.collectedItems.Contains(id))
        {
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        var gameData = QuanliLuutru.instance.GetGameData();
        gameData.AddCollectedItem(id);
        gameData.RemoveCollectedItem(id);
        QuanliLuutru.instance.SaveGame();
    }
    public void SaveData(GameData data)
    {
       // data.activeItems[id] = itemSaveData;
    }

    private void ItemCollected()
    { 
        Destroy(gameObject);
    }

    public void Initialize(InventoryItemData data, int amount)
    {
        ItemData = data;
        soluong = amount;
        StartCoroutine(EnablePickupAfterDelay(2f)); // Đặt thời gian chờ 2 giây
    }

    private IEnumerator EnablePickupAfterDelay(float delay)
    {
        canBePickedUp = false; // Vô hiệu hóa khả năng nhặt vật phẩm
        myCollider.enabled = false; // Vô hiệu hóa collider
        yield return new WaitForSeconds(delay);
        canBePickedUp = true; // Kích hoạt lại khả năng nhặt vật phẩm
        myCollider.enabled = true; // Kích hoạt lại collider
    }
}

[System.Serializable]
public struct ItemPickUpSaveData
{
    public InventoryItemData ItemData;
    public Vector3 Position;
    public Quaternion Rotation;

    public ItemPickUpSaveData(InventoryItemData _data, Vector3 _position, Quaternion _rotation)
    {
        ItemData = _data;
        Position = _position;
        Rotation = _rotation;
    }
}
