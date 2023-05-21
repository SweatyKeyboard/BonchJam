using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialCutscene : MonoBehaviour
{
    public GameObject water;
    public GameObject hud;

    public void Disable()
    {
        water.SetActive(true);
        hud.SetActive(true);
        gameObject.SetActive(false);
    }
}
