using UnityEngine;

public class LeftRightMove : IMoveType
{
    public Vector2 Direction = Vector2.right;

    private float _verticalAmplitude;
    private float _verticalSpeed;

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