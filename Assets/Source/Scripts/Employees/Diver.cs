using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Diver : a_Employee
{
    private void Update()
    {
        SearchActionElements<a_Fish>();
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
        target.gameObject.GetComponent<a_Fish>().TotalyDie();
    }

    protected override IEnumerable<T> Filter<T>(IEnumerable<T> collection)
    {
        return collection.Where(x => (x as a_Fish).IsDead);
    }
}