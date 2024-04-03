using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ScapeAnimation : MonoBehaviour
{
    
    public GameObject SpawnPrefabInitial;
    public GameObject SpawnPrefabVanish;
    public GameObject ObjectAnimated;

    public void StartMove()
    {
        StartCoroutine(SpawnInk());
      
        Debug.Log("Write Boton");
    }

    IEnumerator SpawnInk()
    {
        Debug.Log("Write Boton");
        Instantiate(SpawnPrefabInitial, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Instantiate(SpawnPrefabVanish, transform.position, Quaternion.identity);
    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnDelay());

    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(4);
        ObjectAnimated.GetComponent<Animator>().Play("SepiaReturn 0");
    }

}

