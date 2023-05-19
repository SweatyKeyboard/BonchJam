using UnityEngine;

public class DeadMoving : IMoveType
{
    public Vector2 Direction = Vector2.right;

    private float _verticalAmplitude;
    private float _verticalSpeed;

    public DeadMoving(float verticalSpeed, float verticalAmplitude)
    {
        _verticalAmplitude = verticalAmplitude;
        _verticalSpeed = verticalSpeed;
    }

    public Vector2 Move(float _horizontalSpeed)
    {
        return Vector2.up * Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmplitude / 2;
    }
}
