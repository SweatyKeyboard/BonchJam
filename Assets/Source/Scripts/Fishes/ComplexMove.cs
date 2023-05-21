using UnityEngine;

public class ComplexMove : IMoveType
{
    private Transform _target;
    private Transform _self;
    private float _verticalAmplitude;
    private float _verticalSpeed;


    public float Distance => Vector2.Distance(_target.position, _self.position);
    public Transform Target => _target;

    private Vector2 _direction;
    public Vector2 Direction
    {
        get => _direction;
        set => _direction = value;
    }

    public ComplexMove(Transform self, float verticalSpeed, float verticalAmplitude)
    {
        _self = self;
        _verticalAmplitude = verticalAmplitude;
        _verticalSpeed = verticalSpeed;
    }

    public bool FindNextVictim<T>(VictimType type) where T : a_Fish
    {
        T[] victims = GameObject.FindObjectsOfType<T>();

        int i = 0;
        int closestIndex = -1;
        float closestDistance = float.MaxValue;
        foreach (T victim in victims)
        {
            if (type == VictimType.Alive && victim.IsDead)
            {
                i++;
                continue;
            }

            if (type == VictimType.Dead && !victim.IsDead)
            {
                i++;
                continue;
            }

            float distance = Vector2.Distance(_self.position, victim.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestIndex = i;
            }
            i++;
        }

        if (closestIndex == -1)
        {
            _target = null;
            return false;
        }

        _target = victims[closestIndex].transform;
        return true;
    }


    public Vector2 Move(float speed)
    {
        if (_target == null)
        {
            return Direction * speed +
               Vector2.up * Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmplitude;
        }
        return (_target.position - _self.position).normalized * speed;
    }

}
