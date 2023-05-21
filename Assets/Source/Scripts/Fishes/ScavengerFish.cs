using UnityEngine;

public class ScavengerFish : a_Fish
{
    protected override void Awake()
    {
        Moving = new ComplexMove(transform, _verticalSpeed, _verticalAmplitude);
        base.Awake();
    }

    public override void FindNextVictim()
    {
        ((ComplexMove)Moving).FindNextVictim<a_Fish>(VictimType.Dead);
    }

    protected override void Kill()
    {
        ((ComplexMove)Moving).Target.GetComponent<a_Fish>().TotalyDie();
    }
}
