using System.Collections.Generic;
using UnityEngine;

public class LiveCollection : MonoBehaviour
{
    public static LiveCollection Instance { get; private set; }

    public List<a_Fish> Livers { get; private set; } = new List<a_Fish>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FindFoodAgain()
    {
        foreach (a_Fish fish in Livers)
        {
            fish.FindNextVictim();
        }
    }
}
