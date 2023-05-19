using UnityEngine;

public class Fish : a_Fish
{
    private new void Awake()
    {
        base.Awake();
        Moving = new ComplexMove(transform, _verticalSpeed, _verticalAmplitude);
    }
}