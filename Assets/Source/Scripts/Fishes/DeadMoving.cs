using UnityEngine;

public class DeadMoving : IMoveType
{
    private float _verticalAmplitude;
    private float _verticalSpeed;

    public DeadMoving(float verticalSpeed, float verticalAmplitude)
    {
        _verticalAmplitude = verticalAmplitude;
        _verticalSpeed = verticalSpeed;
    }

    public Vector2 Direction
    {
        get => Vector2.zero;
        set => Direction = value;
    }

    public Vector2 Move(float _horizontalSpeed)
    {
        return Vector2.down * _horizontalSpeed / 2 +
            Vector2.right* Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmplitude / 2;
    }
}
