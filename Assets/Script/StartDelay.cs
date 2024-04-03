using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDelay : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SpawnDelay());

    }


    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(2);
        this.GetComponent<Animator>().Play("SepiaReturn");
    }
}