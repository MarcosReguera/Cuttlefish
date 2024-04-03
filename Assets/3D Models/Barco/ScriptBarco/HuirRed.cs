using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class HuirRed : MonoBehaviour
{
    private bool alcanzadoporlared = false;
    private Vector3 target;
    public float distancia;
    public float velocity;
    private Vector3 initialposition;
    public float restartDelay = 15;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Red")
        {
            alcanzadoporlared = true;
            Debug.Log("Collider Activado");

            target = transform.position + transform.forward * distancia;
        }
        else
        {
            Debug.Log("Collider Activado sin tag");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialposition = transform.position;
        StartCoroutine(Restart());
    }

    // Update is called once per frame
    void Update()
    {
        if (alcanzadoporlared == true)
        {
            Vector3 currentVelocity = Vector3.zero;
            this.transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, velocity * Time.deltaTime);
        }

    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(restartDelay);
        alcanzadoporlared = false;
        this.transform.position = initialposition;
        StartCoroutine(Restart());
    }
}
