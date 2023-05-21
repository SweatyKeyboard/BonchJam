using System.Collections;
using UnityEngine;

public class PredatorFish : a_Fish
{
    [SerializeField] private float _restTime;
    private float _lastEatTime;
    private bool _isFollowingTarget;

    private bool _isReadyToSearch = true;


    private void Start()
    {
        _lastEatTime -= _restTime;
    }
    private new void Update()
    {
        base.Update();

        if (_lastEatTime + _restTime < Time.time && !_isFollowingTarget)
        {
            _isReadyToSearch = true;
            FindNextVictim();
        }
    }

    protected override void Awake()
    {
        Moving = new ComplexMove(transform, _verticalSpeed, _verticalAmplitude);
        base.Awake();
    }
    public override void FindNextVictim()
    {
        if (IsDead)
            return;
        if (!_isReadyToSearch)
            return;

        _isFollowingTarget =
                ((ComplexMove)Moving).FindNextVictim<Fish>(VictimType.Both);
    }

    protected override void Kill()
    {
        ((ComplexMove)Moving).Target.GetComponent<Fish>().TotalyDie();
        _lastEatTime = Time.time;
        _isFollowingTarget = false;
        _isReadyToSearch = false;
    }
}