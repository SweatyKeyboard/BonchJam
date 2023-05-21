using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private InventorySlot[] _slots;   
    [SerializeField] private GameObject _bgDarker;
    public InventorySlot this[int i] => _slots[i];
    public InventorySlot this[ForPipeData i] => _slots.Where(x => x.Element == i).First();

    private bool _isSpawnMenuOpened;
    private Pipe _selectedPipe;

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

    public void OpenCloseSpanwMenu(Pipe pipe)
    {
        if (!_isSpawnMenuOpened)
        {
            _isSpawnMenuOpened = true;
            _selectedPipe = pipe;
            _selectedPipe.Select();
        }
        else
        {
            if (pipe == _selectedPipe)
            {
                _isSpawnMenuOpened = false;
                _selectedPipe.Deselect();
            }
            else
            {
                _selectedPipe.Deselect();
                _selectedPipe = pipe;
                _selectedPipe.Select();
            }
        }

        SetButtonsActivity(_isSpawnMenuOpened);
        _bgDarker.SetActive(_isSpawnMenuOpened);
    }

    private void SetDarker(bool isDarker)
    {
        _bgDarker.SetActive(isDarker);
    }

    public void Spawn(ForPipeData element)
    {
        if (!_isSpawnMenuOpened)
            return;

        _selectedPipe.Spawn(element);

        _isSpawnMenuOpened = false;
        SetButtonsActivity(false);
        _bgDarker.SetActive(false);
    }

    private void SetButtonsActivity(bool isActive)
    {
        foreach (InventorySlot slot in _slots)
        {
            slot.ButtonActive = isActive;
        }
    }
}