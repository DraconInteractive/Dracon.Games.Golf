using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseLevelEffect : MonoBehaviour
{
    public GameObject[] outObjects, inObjects;

    public void Do ()
    {
        foreach (var go in outObjects)
        {
            go.SetActive(false);
        }
        foreach (var go in inObjects)
        {
            go.SetActive(true);
        }
    }
}
