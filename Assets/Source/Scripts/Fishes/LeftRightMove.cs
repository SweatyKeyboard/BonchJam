using UnityEngine;

public class LeftRightMove : IMoveType
{
    private float _verticalAmplitude;
    private float _verticalSpeed;

    private Vector2 _direction;
    public Vector2 Direction
    {
        get => _direction;
        set => _direction = value;
    }
    public LeftRightMove(float verticalSpeed, float verticalAmplitude)
    {
        _verticalAmplitude = verticalAmplitude;
        _verticalSpeed = verticalSpeed;
    }

    public Vector2 Move(float _horizontalSpeed)
    {
        return Direction * _horizontalSpeed + 
               Vector2.up * Mathf.Sin(Time.time * _verticalSpeed) * _verticalAmplitude;
    }
}