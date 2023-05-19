using UnityEngine;

[CreateAssetMenu(menuName = "PipeElement")]
public class ForPipeData : ScriptableObject
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private a_ForPipe _objectPrefab;
    [SerializeField] private Sprite _sprite;

    public float SpawnTime => _spawnTime;
    public a_ForPipe ObjectPrefab => _objectPrefab;
    public Sprite Sprite => _sprite;
}