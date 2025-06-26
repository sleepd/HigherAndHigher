using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public List<GameObject> targets;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowTargets()
    {
        foreach (GameObject target in targets)
        {
            target.SetActive(true);
        }
    }
}
