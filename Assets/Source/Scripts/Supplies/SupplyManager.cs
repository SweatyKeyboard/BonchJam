using UnityEngine;

public class SupplyManager : MonoBehaviour
{
    [SerializeField] private ForPipeData[] _forPipeElements;
    [SerializeField] private Inventory _inventory;

    private Supply _tommorowSupply;
    public Supply TommorowSupply => _tommorowSupply;

    private void Start()
    {
        GetSupply();
    }


    public void GetSupply()
    {
        if (_tommorowSupply == null)
        {
            _tommorowSupply = GenerateSupply();
        }
        GetSupply(_tommorowSupply);

        _tommorowSupply = GenerateSupply();
        _inventory.UpdateTommorows(_tommorowSupply);
    }

    private Supply GenerateSupply()
    {
        SupplyItem[] items = new SupplyItem[_forPipeElements.Length];
        for (int i = 0; i < items.Length; i++)
        {
            items[i] = new SupplyItem(_forPipeElements[i], Random.Range(3, 6));
        }

        return new Supply(items);
    }

    public void GetSupply(Supply supply)
    {
        foreach (SupplyItem item in supply)
        {
            _inventory[item.Element].Count += item.Count;
        }
    }
}
