using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Diver : a_Employee
{
    private void Update()
    {
        if (_lastWorkedTime + _cooldown <= Time.time)
        {
            SearchActionElements<a_Fish>();
        }
    }

    public override void Work(Transform target)
    {
        if (target.GetComponent<a_Fish>())
        {
            TakeFish(target);
        }
    }

    private void TakeFish(Transform target)
    {
        _animator.SetTrigger("clean");
        target.gameObject.GetComponent<a_Fish>().TotalyDie();

        Goals.Instance.DeadTook++;
        Goals.Instance.CheckDeads();
    }

    protected override IEnumerable<T> Filter<T>(IEnumerable<T> collection)
    {
        return collection.Where(x => (x as a_Fish).IsDead);
    }
}