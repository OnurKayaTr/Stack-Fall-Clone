using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;
    float currunettime;
    bool invincible;
    public GameObject fireShild;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            carpma = true;

        }
        if (Input.GetMouseButtonUp(0))
        {
            carpma = false;
        }

        if (invincible)
        {
            currunettime -= Time.deltaTime * .35f;
            if (!fireShild.activeInHierarchy)
            {
                fireShild.SetActive(true);
            }
        }
        else
        {
            if (fireShild.activeInHierarchy)
            {
                fireShild.SetActive(false);
            }

            if (carpma)
            {
                currunettime += Time.deltaTime * 0.8f;
            }
            else
            {
                currunettime -= Time.deltaTime * 0.5f;
            }
        }
        
        if (currunettime > 1)
        {
            currunettime = 1;
            invincible = true;
        }
        else if (currunettime <= 0)
        {
            currunettime= 0;
            invincible = false;
        }

    }
    private void FixedUpdate()
    {
        if (carpma)
        {
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!carpma)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime*5, 0);
        }
        else
        {
            if (invincible)
            {
                if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "plane")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                }
              
            }
            else {

                if (collision.gameObject.tag == "enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();

                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log("Game Over");
                }


            }

            
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (!carpma)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}
