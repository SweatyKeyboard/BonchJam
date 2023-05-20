public class PredatorFish : a_Fish
{    protected override void Awake()
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
        ((ComplexMove)Moving).Target.GetComponent<Fish>().TottalyDie();
    }
}