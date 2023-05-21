using UnityEngine;

public class PredatorFish : a_Fish
{
    [SerializeField] private float _restTime;

    protected override void Awake()
    {
        Moving = new ComplexMove(transform, _verticalSpeed, _verticalAmplitude);
        base.Awake();
    }
    public override void FindNextVictim()
    {
        if (IsDead)
            return;

        ((ComplexMove)Moving).FindNextVictim<Fish>(VictimType.Both);
    }

    protected override void Kill()
    {
        ((ComplexMove)Moving).Target.GetComponent<Fish>().TotalyDie();
    }
}