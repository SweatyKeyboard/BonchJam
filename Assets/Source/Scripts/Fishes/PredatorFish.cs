using System.Collections;
using UnityEngine;

public class PredatorFish : a_Fish
{
    [SerializeField] private float _eatDistance;
    [SerializeField] private float _eatDuration;

    private bool _isEating;

    private new void Awake()
    {
        base.Awake();
        Moving = new ComplexMove(transform, _verticalSpeed, _verticalAmplitude);
        ((ComplexMove)Moving).FindNextVictim<Fish>();
    }
    private new void Update()
    {
        base.Update();
        CheckEating();
    }

    private void CheckEating()
    {
        if (_isEating)
            return;

        if (((ComplexMove)Moving).Target == null)
            return;

        if (((ComplexMove)Moving).Distance < _eatDistance)
        {
            StartCoroutine(Eating());
        }
    }

    private IEnumerator Eating()
    {
        _isEating = true;
        yield return new WaitForSeconds(_eatDuration);
        
        ((ComplexMove)Moving).Target.GetComponent<Fish>().Die();
        ((ComplexMove)Moving).FindNextVictim<Fish>();
        _isEating = false;
    }
}