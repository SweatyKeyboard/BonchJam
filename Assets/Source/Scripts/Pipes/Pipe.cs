using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class Pipe : MonoBehaviour, ISelectable
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _spawnMenu;
    [SerializeField] private Image _iconFilling;
    [SerializeField] private GameObject _icon;
    [SerializeField] private float _startConditionValue;
    [SerializeField] private int _index;

    [SerializeField] private float _brokenFrom = 40;
    [SerializeField] private float _cantUseFrom = 20;

    [Header("Sprites")]
    [SerializeField] private Sprite _normalSprite;
    [SerializeField] private Sprite _brokenSprite;
    [SerializeField] private Sprite _normalSelected;
    [SerializeField] private Sprite _brokenSelected;

    private SpriteRenderer _spriteRenderer;

    private float _condition;
    public float Condition
    {
        get => _condition;
        set
        {
            if (value > 0 && value < 100)
                _condition = value;

            if (value > 100)
                _condition = 100;

            if (value < 0)
                value = 0;

            _spriteRenderer.sprite = _condition > _brokenFrom ? _normalSprite : _brokenSprite;

            ConditionChanged?.Invoke();
        }
    }

    private bool _isSelected;
    public bool IsSelected { get => _isSelected; set => _isSelected = value; }

    public event System.Action ConditionChanged;
    private Action<Transform> _clicked;
    public Action<Transform> Clicked
    {
        get => _clicked;
        set => _clicked = value;
    }

    private bool _isOpened;
    private bool _isSpawningNow;

    private void Awake()
    {
        _condition = _startConditionValue;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Spawn(ForPipeData element)
    {
        if (_isSpawningNow)
            return;

        if (Inventory.Instance[element].Count == 0)
            return;

        StartCoroutine(DelayedSpawn(element));
    }

    private IEnumerator DelayedSpawn(ForPipeData element)
    {
        Inventory.Instance[element].Count--;
        _icon.SetActive(true);

        _isSpawningNow = true;
        OpenCloseSpawnMenu();
        float elapsedTime = 0;
        do
        {
            _iconFilling.fillAmount = elapsedTime / element.SpawnTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        } while (elapsedTime < element.SpawnTime);

        a_ForPipe spawned = Instantiate(element.ObjectPrefab, _spawnPosition.position, Quaternion.identity);

        if (spawned.TryGetComponent(out a_Fish fish))
        {
            LiveCollection.Instance.Livers.Add(fish);
            LiveCollection.Instance.FindFoodAgain();
        }

        _iconFilling.fillAmount = 0;
        Condition -= element.PipeBrakingValue;
        _icon.SetActive(false);

        _isSpawningNow = false;


        Goals.Instance.CheckFishes();
    }

    private void OnMouseDown()
    {
        if (IsSelected && Condition > _cantUseFrom)
        {
            Clicked?.Invoke(transform);
            return; 
        }

        OpenCloseSpawnMenu();
    }

    public void OpenCloseSpawnMenu()
    {
        Inventory.Instance.OpenCloseSpanwMenu(this);
    }


    public void Select()
    {
        _spriteRenderer.sprite = _condition > _brokenFrom ? _normalSelected : _brokenSelected;

    }

    public void Deselect()
    {
        _spriteRenderer.sprite = _condition > _brokenFrom ? _normalSprite : _brokenSprite;
    }
}
