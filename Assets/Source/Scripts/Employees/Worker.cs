using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Worker : a_Employee
{
    [SerializeField] private float _conditionGrowth;

    private void Update()
    {
        SearchActionElements<Pipe>();
    }
    public override void Work(Transform target)
    {
        if (target.TryGetComponent(out Pipe pipe))
        {
            StartCoroutine(WorkCoroutine(_workTime, target, RepairPipe));
        }
    }

    private void RepairPipe(Transform target)
    {
        target.GetComponent<Pipe>().Condition += _conditionGrowth;
    }

    protected override IEnumerable<T> Filter<T>(IEnumerable<T> collection)
    {
        return collection.Where(x => (x as Pipe).Condition < 100);
    }
}
