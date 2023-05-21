public class Food : a_Fish
{
    protected override void Awake()
    {
        Moving = new LeftRightMove(_verticalSpeed, _verticalAmplitude);
        base.Awake();
    }

    public override void FindNextVictim()
    {
        //((ComplexMove)Moving).FindNextVictim<>();
    }

    protected override void Kill()
    {
        //((ComplexMove)Moving).Target.GetComponent<>().Die();
    }
}