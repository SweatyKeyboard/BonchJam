using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _spawnMenu;
    [SerializeField] private Image _iconFilling;
    [SerializeField] private GameObject _icon
        ;
    [SerializeField] private float _startConditionValue;

    private float _condition;
    public float Condition {
        get => _condition;
        private set
        {
            _condition = value;
            ConditionChanged?.Invoke();
        }
    }
    public event System.Action ConditionChanged;

    private bool _isOpened;
    private bool _isSpawningNow;

    private void Awake()
    {
        _condition = _startConditionValue;
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
        _iconFilling.sprite = element.Sprite;
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
    }

    private void OnMouseDown()
    {
        OpenCloseSpawnMenu();
    }

    public void OpenCloseSpawnMenu()
    {
        _isOpened = !_isOpened;
        _spawnMenu.SetActive(_isOpened);
    }
}
