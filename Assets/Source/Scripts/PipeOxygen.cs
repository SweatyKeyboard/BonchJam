using UnityEngine;

public class PipeOxygen : a_ForPipe
{
    [SerializeField] private float _destroyTimer;
    [SerializeField] private float _barIncreaseValue;

    private void Start()
    {
        Bar.Instance.AddOxygen(_barIncreaseValue);
    }

    private void Update()
    {
        _destroyTimer -= Time.deltaTime;

        if (_destroyTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
