using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RestartPosition : MonoBehaviour
{
    public float restartDelay = 15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Restart());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        this.transform.GetComponent<Animator>().Play("RestartPosition");
        StartCoroutine(Restart());
    }
}
