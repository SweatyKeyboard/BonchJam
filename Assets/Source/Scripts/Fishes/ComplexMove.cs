using UnityEngine;

public class ComplexMove : IMoveType
{
    private Transform _target;
    private Transform _self;
    private float _verticalAmplitude;
    private float _verticalSpeed;
    public Vector2 Direction = Vector2.right;
    public float Distance => Vector2.Distance(_target.position, _self.position);
    public Transform Target => _target;

    public ComplexMove(Transform self, float verticalSpeed, float verticalAmplitude)
    {
        _self = self; 
        _verticalAmplitude = verticalAmplitude;
        _verticalSpeed = verticalSpeed;
    }

    public void FindNextVictim<T>() where T : MonoBehaviour, IVictim
    {
        T[] victims = GameObject.FindObjectsOfType<T>();


        int i = 0;
        int closestIndex = -1;
        float closestDistance = float.MaxValue;
        foreach (T victim in victims)
        {
            if (victim.IsFollowable)
            {
                float distance = Vector2.Distance(_self.position, victim.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            i++;
        }

        if (closestIndex == -1)
        {
            _target = null;
            return;
        }

        _target = victims[closestIndex].transform;
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
