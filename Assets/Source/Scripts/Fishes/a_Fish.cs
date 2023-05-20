using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public abstract class a_Fish : MonoBehaviour, IVictim
{
    [SerializeField] protected float _horizontalSpeed;
    [SerializeField] protected float _verticalAmplitude;
    [SerializeField] protected float _verticalSpeed;

    [SerializeField] private Color _diedTint;

    [SerializeField] private float _eatDistance;
    [SerializeField] private float _eatDuration;
    private bool _isEating;

    [SerializeField] private float _lifeLength;
    [SerializeField] private float _eatingLongerLife;
    private float _lifeTime;

    [SerializeField] private float _barDecreaseBySecondOnDead;
    [SerializeField] private float _barIncrease;
    private bool _isDecreasingBar;

    protected IMoveType Moving;

    private Rigidbody2D _rigidBody;
    private SpriteRenderer _spriteRenderer;

    private bool _isDead;
    public bool IsDead => _isDead;

    public bool IsFollowable { get; set; } = true;

    public abstract void FindNextVictim();
    protected abstract void Kill();
    protected virtual void Awake()
    {
        _lifeTime = _lifeLength;
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Bar.Instance.AdditionalDecreaseBySecond -= _barIncrease;
        FindNextVictim();
    }

    protected void Update()
    {
        LiveLife();

        Vector2 direction = Moving.Move(_horizontalSpeed);
        _rigidBody.MovePosition((Vector2)transform.position + direction);

        if (IsDead)
            return;

        if (!_isEating)
        {
            if (direction.x < 0)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }
        }

        if (Moving.GetType() == typeof(ComplexMove))
            CheckEating();
    }
    private void LiveLife()
    {
        _lifeTime -= Time.deltaTime;
        if (_lifeTime <= 0 && !_isDead)
        {
            Die();
        }
    }

    private void CheckEating()
    {
        if (_isEating)
            return;

        if (((ComplexMove)Moving).Target == null)
            return;

        if (((ComplexMove)Moving).Distance < _eatDistance)
        {
            StartCoroutine(Eating());
        }
    }

    private IEnumerator Eating()
    {
        _isEating = true;
        yield return new WaitForSeconds(_eatDuration);

        if (!_isDead)
        {
            Kill();
            _lifeTime += _eatingLongerLife;
            _isEating = false;
            yield return new WaitForSeconds(0.1f);

            LiveCollection.Instance.FindFoodAgain();
        }
    }


    public virtual void Die()
    {
        _isDead = true;
        IsFollowable = false;
        _spriteRenderer.color = _diedTint;
        _spriteRenderer.flipY = true;
        Moving = new DeadMoving(_verticalSpeed, _verticalAmplitude);

        Bar.Instance.AdditionalDecreaseBySecond += _barDecreaseBySecondOnDead;
        _isDecreasingBar = true;
    }

    public void TottalyDie()
    {
        LiveCollection.Instance.Livers.Remove(this);

        if (_isDecreasingBar)
        {
            Bar.Instance.AdditionalDecreaseBySecond -= _barDecreaseBySecondOnDead - _barIncrease;
        }

        Destroy(gameObject);
    }

    public void Flip(Vector2 direction)
    {
        if (IsDead)
            return;

        Moving.Direction = direction;
    }
}