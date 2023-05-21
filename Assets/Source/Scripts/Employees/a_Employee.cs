using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public abstract class a_Employee : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] protected float _workTime;

    [SerializeField] protected float _cooldown;
    protected float _lastWorkedTime;

    private SpriteRenderer _spriteRenderer;
    protected Animator _animator;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f, 0.9f, 1f, 0.5f);
        Gizmos.DrawWireSphere(transform.position, _radius);
    }

    public abstract void Work(Transform target);

    protected IEnumerator WorkCoroutine(float duration, Transform target, System.Action<Transform> resultAction)
    {
        if (transform.position.x > 0)
            _animator.SetBool("fix left", true);
        else
            _animator.SetBool("fix right", true);
        yield return new WaitForSeconds(duration);
        resultAction?.Invoke(target);
        _lastWorkedTime = Time.time;
        FindAnyObjectByType<Hook>().IsLocked = false;
        FindAnyObjectByType<CraneHorizontalMove>().IsLocked = false;
        if (transform.position.x > 0)
            _animator.SetBool("fix left", false);
        else
            _animator.SetBool("fix right", false);
    }

    protected void SearchActionElements<T>() where T : MonoBehaviour, ISelectable
    {
        T[] objects = FindObjectsOfType<T>();
        T[] inDistance = objects.Where(x => Vector2.Distance(x.transform.position, transform.position) < _radius).ToArray();

        List<T> selected = Filter(inDistance).ToList();
        List<T> deselected = objects.Except(selected).ToList();

        selected.ForEach(x => x.Select());
        deselected.ForEach(x => x.Deselect());

        selected.ForEach(x => x.IsSelected = true);
        deselected.ForEach(x => x.IsSelected = false);

        selected.ForEach(x =>
        {
                x.Clicked += Work;
            if (x.Clicked?.GetInvocationList().Length > 1)
                x.Clicked -= Work;

        });

        deselected.ForEach(x =>
        {
            if (x.Clicked?.GetInvocationList().Length > 1)
                x.Clicked -= Work;
        });
    }

    protected abstract IEnumerable<T> Filter<T>(IEnumerable<T> collection);
}