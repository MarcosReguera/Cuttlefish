using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;



public class MoveAnimation : MonoBehaviour
{
    
    public GameObject SpawnPrefabInitial;
    public GameObject SpawnPrefabVanish;
    public GameObject ObjectAnimated;

    public bool ismoving = false;
    public bool ischanging = false;
    public GameObject SandLocator;
    public bool goingtosand = false;
    public GameObject Rocklocator;
    public bool goingtorock = false;
    public bool isreseatingposition = false;
    public Texture2D[] textures;
    public GameObject ResetLocator;

    private Texture2D baseTexture;
    private Texture2D transitionTexture;
    [Range(0, 360)] public float rotationClampValue = 50;


    public float speed;
    private float t;
    private float inercia;


    public void StartMove()
    {
        StartCoroutine(SpawnInk());
        StartCoroutine(Timecheck());

        ismoving = true;
        if (goingtosand == false && goingtorock == false)
        {
            goingtosand=true;
            baseTexture = textures[0];
            transitionTexture = textures[1];
            t= 0;
        }

        this.transform.GetChild(0).GetComponent<Animator>().Play("SepiaRefPausa");
        ischanging = true;
        t = 0;
        inercia = 1;
      
    }

    IEnumerator SpawnInk()
    {
        Debug.Log("spawn Ink");
        Instantiate(SpawnPrefabInitial, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
        Instantiate(SpawnPrefabVanish, transform.position, Quaternion.identity);
    }

    IEnumerator Timecheck()
    {
        yield return new WaitForSeconds(15);
        if (ismoving || ischanging)
        {
            Timecheck();
            Debug.Log("reset timecheck");
        }
        else
        {
            isreseatingposition = true;
            inercia = 1;
            ischanging = true;
            t = 0;
        }


    }
    // Start is called before the first frame update
    void Start()
    {

        Renderer rend;
        rend = ObjectAnimated.GetComponent<Renderer>();

        baseTexture = textures[0];
        transitionTexture = textures[1];

        rend.sharedMaterial.SetFloat("_Transition", 0);
        rend.sharedMaterial.SetTexture("_Variation_Map", transitionTexture);
        rend.sharedMaterial.SetTexture("_Base_Map", baseTexture);


        StartCoroutine(SpawnDelay());



    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(4);
        ObjectAnimated.GetComponent<Animator>().Play("SepiaReturn 0");
        StartCoroutine(Timecheck());
    }

    void Update()
    {
        Renderer rend;
        rend = ObjectAnimated.GetComponent<Renderer>();

        if (ismoving)
        {

            GameObject TargetObject = null;
           

            if (goingtosand == true)
            {
                //Debug.Log("Esta en Arena");
                TargetObject = SandLocator;
                transitionTexture = textures[1];
                rend.sharedMaterial.SetTexture("_Variation_Map", transitionTexture);

            }

            if (goingtorock == true)
            {
                //Debug.Log("Esta en Roca");
                TargetObject = Rocklocator;
                transitionTexture = textures[2];
                rend.sharedMaterial.SetTexture("_Variation_Map", transitionTexture);

            }


            

            Vector3 forwardspeed = gameObject.transform.forward;
            Vector3 target = TargetObject.transform.position - transform.position;
            Vector3 velocity = 2 * forwardspeed;

            if (inercia > 0)
            {
                inercia -= 0.4f * Time.deltaTime;
                Vector3 forward = gameObject.transform.forward;
                target = (new Vector3(forward.x, forward.y ,forward.z) * 20 * inercia) + ((TargetObject.transform.position - transform.position) * (1 - inercia));
            }


            this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, target, ref velocity, 2, 6);
            Debug.DrawRay(this.gameObject.transform.position, target);

            Vector3 direccionMirar = target;
            Quaternion rotationCur = Quaternion.LookRotation((direccionMirar));
            transform.localRotation = RotationClamp(rotationCur, rotationClampValue);

            float Distance;
            Distance = Vector3.Distance(this.gameObject.transform.position, TargetObject.transform.position);



            if (Distance <= .25)
            {

                ismoving = false;
                bool isinsand = goingtosand;
                bool istorock = goingtorock;

                if (isinsand == true)
                {
                    goingtosand = false;
                    goingtorock = true;
                    Debug.Log("Esta en Arena");

                }

                if (istorock == true)
                {
                    goingtosand = true;
                    goingtorock = false;
                    Debug.Log("Esta en Roca");

                }


            }



        }
        else
        {
            if (isreseatingposition == false)
            {
                
                Vector3 right = this.gameObject.transform.right;
                Vector3 forward = this.gameObject.transform.forward;
                Vector3 velocity = Vector3.zero;
                Vector3 target = new Vector3(forward.x + right.x / 500, 0, forward.z + right.z / 500);

                Debug.DrawRay(this.gameObject.transform.position, target);
                this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, this.gameObject.transform.position + target, ref forward, Time.deltaTime, 0.1f);
                Vector3 direccionMirar = target;
                transform.rotation = Quaternion.LookRotation((direccionMirar));
            }


        }

        if (isreseatingposition)
        {
            GameObject TargetObject = null;

            TargetObject = ResetLocator;
            transitionTexture = textures[0];
            rend.sharedMaterial.SetTexture("_Variation_Map", transitionTexture);

            Vector3 forwardspeed = gameObject.transform.forward;
            Vector3 target = TargetObject.transform.position - transform.position;
            Vector3 velocity = 2 * forwardspeed;

            if (inercia > 0)
            {
                inercia -= 0.4f * Time.deltaTime;
                Vector3 forward = gameObject.transform.forward;
                target = (new Vector3(forward.x, forward.y, forward.z) * 20 * inercia) + ((TargetObject.transform.position - transform.position) * (1 - inercia));
            }


            this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, target, ref velocity, 2, 6);
            Debug.DrawRay(this.gameObject.transform.position, target);

            Vector3 direccionMirar = target;
            Quaternion rotationCur = Quaternion.LookRotation((direccionMirar));
            transform.localRotation = RotationClamp(rotationCur, rotationClampValue);

            float Distance;
            Distance = Vector3.Distance(this.gameObject.transform.position, TargetObject.transform.position);

            if (Distance <= .25)
            {

                isreseatingposition = false;



            }



        }


        if (ischanging)
        {

            rend.sharedMaterial.SetFloat("_Transition", t);
            t += 0.25f * Time.deltaTime;

            if (t >= 1)
            {
                ischanging = false;
                rend.sharedMaterial.SetFloat("_Transition", 1);
                rend.sharedMaterial.SetTexture("_Base_Map", transitionTexture);

                t = 0;
            }


        }


    }

    static Quaternion RotationClamp(Quaternion rotationCur, float rotationClampValue)
    {

        Vector3 angleClamp = rotationCur.eulerAngles;
        rotationCur.eulerAngles = new Vector3(Mathf.Clamp((angleClamp.x > 180) ? angleClamp.x - 360 : angleClamp.x, -rotationClampValue, rotationClampValue), angleClamp.y, 0);
        return rotationCur;

    }

    static Quaternion RotationClampStatic(Quaternion rotationCur, float rotationClampValue)
    {

        Vector3 angleClamp = rotationCur.eulerAngles;
        rotationCur.eulerAngles = new Vector3(Mathf.Clamp((angleClamp.x > 180) ? angleClamp.x - 360 : angleClamp.x, -rotationClampValue, rotationClampValue), angleClamp.y, 0);
        return rotationCur;

    }

}

