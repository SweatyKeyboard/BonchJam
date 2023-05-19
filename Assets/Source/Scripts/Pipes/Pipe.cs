using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _spawnMenu;
    [SerializeField] private Image _icon;
    public float Condition { get; private set; }

    private bool _isOpened;
    private bool _isSpawningNow;

    public void Spawn(ForPipeData element)
    {
        if (_isSpawningNow)
            return;

        StartCoroutine(DelayedSpawn(element));
    }

    private IEnumerator DelayedSpawn(ForPipeData element)
    {
        _isSpawningNow = true;
        OpenCloseSpawnMenu();
        float elapsedTime = 0;
        _icon.sprite = element.Sprite;
        do
        {
            _icon.fillAmount = elapsedTime / element.SpawnTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        } while (elapsedTime < element.SpawnTime);

        Instantiate(element.ObjectPrefab, _spawnPosition.position, Quaternion.identity);
        _icon.fillAmount = 0;
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
