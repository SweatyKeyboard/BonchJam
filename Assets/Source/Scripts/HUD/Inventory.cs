using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private InventorySlot[] _slots;
    public InventorySlot this[int i] => _slots[i];
    public InventorySlot this[ForPipeData i] => _slots.Where(x => x.Element == i).First();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateTommorows(Supply tommorowSupply)
    {
        foreach (SupplyItem supply in tommorowSupply)
        {
            this[supply.Element].SetTommorow(supply.Count);
        }
    }
}