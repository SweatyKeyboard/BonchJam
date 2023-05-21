using UnityEngine;

[CreateAssetMenu(menuName = "PipeElement")]
public class ForPipeData : ScriptableObject
{
    [SerializeField] private float _spawnTime;
    [SerializeField] private float _pipeBrakingValue;
    [SerializeField] private a_ForPipe _objectPrefab;
    [SerializeField] private Sprite _sprite;

    public float SpawnTime => _spawnTime;
    public float PipeBrakingValue => _pipeBrakingValue;
    public a_ForPipe ObjectPrefab => _objectPrefab;
    public Sprite Sprite => _sprite;
}