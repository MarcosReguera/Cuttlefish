using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOffset : MonoBehaviour
{
    float randomOffset;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        randomOffset = Random.Range(0f, 100f);

        anim.Play("Take 001", 0, randomOffset);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
