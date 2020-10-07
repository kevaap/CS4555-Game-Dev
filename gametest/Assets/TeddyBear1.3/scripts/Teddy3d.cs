using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teddy3d : MonoBehaviour
{
    GameObject Teddy;
    Animator anim;
    Rigidbody rigid;
    Rigidbody rig;
    Transform trans;
    float jumpforce;
    bool gr;
    float speed;
    Vector3 v3Velocity;
    Vector3 v3VelocityAUX;
    Vector3 forcedir;
    RaycastHit hit;
    RaycastHit hit2;
    bool active;
    bool walk;
    float randomTime;
    float timeCounter;
    float fallCounter;
    GameObject[] BodyBones = new GameObject[11];



    void Start()
    {
        Teddy = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        trans = GetComponent<Transform>();
        jumpforce = 100f;
        forcedir = new Vector3(1f, 0f, 0f);
        active = true;
        randomTime = 12f;
        timeCounter = 0f;
        fallCounter = 0f;
        anim.SetInteger("idle", 300);
        BodyBones[0] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis").gameObject;
        BodyBones[1] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy L Thigh").gameObject;
        BodyBones[2] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy L Thigh/Teddy L Calf").gameObject;
        BodyBones[3] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy R Thigh").gameObject;
        BodyBones[4] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy R Thigh/Teddy R Calf").gameObject;
        BodyBones[5] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1").gameObject;
        BodyBones[6] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1/Teddy Neck/Teddy Head").gameObject;
        BodyBones[7] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1/Teddy L Clavicle/Teddy L UpperArm").gameObject;
        BodyBones[8] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1/Teddy L Clavicle/Teddy L UpperArm/Teddy L Forearm").gameObject;
        BodyBones[9] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1/Teddy R Clavicle/Teddy R UpperArm").gameObject;
        BodyBones[10] = Teddy.transform.Find("ROOT/Teddy 1/Teddy Pelvis/Teddy Spine/Teddy Spine1/Teddy R Clavicle/Teddy R UpperArm/Teddy R Forearm").gameObject;
        for (int bo = 0; bo < BodyBones.Length; bo++)
        {
            BodyBones[bo].GetComponent<Rigidbody>().useGravity = false;
            BodyBones[bo].GetComponent<Rigidbody>().isKinematic = true;
            BodyBones[bo].GetComponent<Collider>().enabled = false;
        }
    }


    void FixedUpdate()
    {

        //IDLES
        if (timeCounter > randomTime)
        {
            anim.SetInteger("idle", Random.Range(0, 1200));
            randomTime = Random.Range(1, 12);
            timeCounter = 0f;
        }
        timeCounter += Time.deltaTime;


        //CHECK GROUNDED       
        if (Physics.Raycast(trans.position + new Vector3(0.18f, 0.05f, 0.15f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(0.18f, 0.05f, -0.15f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(-0.18f, 0.05f, 0.15f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(-0.18f, 0.05f, -0.15f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(0f, 0.05f, 0f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(-0.087f, 0.075f, 0f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(0.087f, 0.075f, 0f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(0f, 0.075f, -0.08f), Vector3.down, 0.085f)
                || Physics.Raycast(trans.position + new Vector3(0f, 0.075f, 0.08f), Vector3.down, 0.085f))
        {
            anim.SetBool("grounded", true);
            gr = true;
            fallCounter = 0f;           
        }
        else
        {
            anim.SetBool("grounded", false);
            gr = false;
            fallCounter += Time.deltaTime;
        }


        //WALK
        if (Input.GetKey(KeyCode.LeftShift))
            walk = true;
        else
            walk = false;
        anim.SetBool("walk", walk);


        // SET THE FORCE DIRECTION       
        if (Physics.Raycast(trans.position + new Vector3(0f, 0.05f, 0f), Vector3.down, out hit))
        {
            forcedir = -Vector3.Cross(Vector3.Cross(hit.normal, trans.forward), hit.normal);
            forcedir.Normalize();
        }


        //TRANSLATE
        v3Velocity = rigid.velocity;
        speed = v3Velocity.magnitude;
        anim.SetFloat("run", Input.GetAxisRaw("Vertical"));
        if (Input.GetKey(KeyCode.W) && gr == true && speed < 5f && active == true)
        {
            if (walk == true) rigid.velocity = forcedir * -1f;
            if (walk == false) rigid.velocity = forcedir * -4f;
        }
        if (Input.GetKey(KeyCode.S) && gr == true && speed <= 1f && speed > -1.1f && active == true)
        {
            if (walk == true) rigid.velocity = forcedir * 1f;
            if (walk == false) rigid.velocity = forcedir * 3f;
        }
        anim.SetFloat("speed", speed);


        //TURN       
        if (Input.GetKey(KeyCode.D) && gr == true && active == true)
        {
            trans.Rotate(new Vector3(0f, 3f, 0f));
            if (anim.GetFloat("run") == 0f)
                anim.SetInteger("turn", 1);
        }
        else
        {
            if (Input.GetKey(KeyCode.A) && gr == true && active == true)
            {
                trans.Rotate(new Vector3(0f, -3f, 0f));
                if (anim.GetFloat("run") == 0f)
                    anim.SetInteger("turn", -1);
            }
            else anim.SetInteger("turn", 0);
        }


        //JUMP
        if (Input.GetKey(KeyCode.Space) && gr == true && active == true)
        {
            active = false;
            //static
            if (anim.GetFloat("run") <= 0f)
            {
                anim.Play("jumpin");
                rigid.AddForce(new Vector3(0f, 1f, 0f) * jumpforce * 1.5f, ForceMode.Acceleration);
            }
            //run
            if (anim.GetFloat("run") > 0f)
            {
                anim.Play("jumprunin");
                rigid.AddForce((trans.forward * 0.07f + new Vector3(0f, 2.5f, 0f)) * jumpforce, ForceMode.Acceleration);
            }
        }


        //HITS       
        if (Physics.Raycast(trans.position + new Vector3(-0.2f, 0.35f, 0f), trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(-0.2f, 1f, 0f), trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(0.2f, 0.35f, 0f), trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(0.2f, 1f, 0f), trans.forward, out hit2, 0.33f))
        {
            if (hit2.transform.tag == "enemy")
            {
                rigid.AddForce(trans.forward * -5f, ForceMode.Impulse);
                active = false;
                anim.Play("fall2");
            }
        }
        if (Physics.Raycast(trans.position + new Vector3(-0.2f, 0.35f, 0f), -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(-0.2f, 1f, 0f), -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(0.2f, 0.35f, 0f), -trans.forward, out hit2, 0.33f)
             || Physics.Raycast(trans.position + new Vector3(0.2f, 1f, 0f), -trans.forward, out hit2, 0.33f))
        {
            if (hit2.transform.tag == "enemy")
            {
                rigid.AddForce(trans.forward * 5f, ForceMode.Impulse);
                active = false;
                anim.Play("fall1");
            }
        }


        //RAGDOLL       
        if (speed > 11)
        {
            v3VelocityAUX = v3Velocity;
            active = false;
            GetComponent<Collider>().enabled = false;
            anim.enabled = false;
            rigid.isKinematic = true;
            fallCounter = 0f;
            CameraTeddy3d.dead = true;
            for (int bo = 0; bo < 11; bo++)
            {
                BodyBones[bo].GetComponent<Rigidbody>().useGravity = true;
                BodyBones[bo].GetComponent<Rigidbody>().isKinematic = false;
                BodyBones[bo].GetComponent<Collider>().enabled = true;
                BodyBones[bo].GetComponent<Rigidbody>().velocity = v3VelocityAUX * 1f;
            }
        }


        //RESTART
        if (Input.GetKeyDown(KeyCode.P))
        {
            for (int bo = 0; bo < BodyBones.Length; bo++)
            {
                BodyBones[bo].GetComponent<Rigidbody>().useGravity = false;
                BodyBones[bo].GetComponent<Rigidbody>().isKinematic = true;
                BodyBones[bo].GetComponent<Collider>().enabled = false;
            }
            CameraTeddy3d.dead = false;
            trans.position = (new Vector3(0f, 0.1f, 0f));
            trans.localRotation = Quaternion.Euler(0, 0, 0);
            rigid.velocity = (new Vector3(0f, 0f, 0f));
            anim.Play("static");
            anim.SetFloat("run", 0f);
            anim.SetBool("walk", false);
            active = true;
            GetComponent<Collider>().enabled = true;
            anim.enabled = true;
            rigid.isKinematic = false;
            fallCounter = 0f;
        }
    }

    public void Activate()
    {
        active = true;
    }



}