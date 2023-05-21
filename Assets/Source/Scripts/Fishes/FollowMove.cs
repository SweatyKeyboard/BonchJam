using UnityEngine;

public class FollowMove : IMoveType
{
    private Transform _target;
    private Transform _self;

    public float Distance => Vector2.Distance(_target.position, _self.position);
    public Transform Target => _target;

    private Vector2 _direction;
    public Vector2 Direction 
    {
        get => _direction;
        set => _direction = value;
    }

    public FollowMove(Transform self)
    {
        _self = self;
    }

    public void FindNextVictim<T>() where T : MonoBehaviour, IVictim
    {
        T[] victims = GameObject.FindObjectsOfType<T>();

        int i = 0;
        int closestIndex = 0;
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
        _target = victims[closestIndex].transform;
    }

    public Vector2 Move(float speed)
    {
        return (_target.position - _self.position).normalized * speed;
    }
}
