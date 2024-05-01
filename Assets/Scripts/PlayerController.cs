using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    bool carpma;
    float currunettime;
    bool invincible;

    public GameObject fireShild;
    [SerializeField]
    AudioClip win, death , idestory, destory, bounce;

    public int currentObstacleNumber;
    public int totalObstacleNumber;


    public Image invidInvictibleSlider;
    public GameObject InvictibleObject;
    public GameObject gameOverUI;
    public GameObject finishUI;





    public enum PlayerState
    {
        Prepare,Playing,Died, Finish
    }
    [HideInInspector]
    public PlayerState playerstate= PlayerState.Prepare;

    void Start()
    {
        totalObstacleNumber = FindObjectsOfType<ObstacleController>().Length;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentObstacleNumber = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if(playerstate==PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
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


            if (currunettime >= 0.15f || invidInvictibleSlider.color == Color.red)
            {
                InvictibleObject.SetActive(true);
            }
            else
            {
                InvictibleObject.SetActive(false);
            }




            if (currunettime >= 1)
            {
                currunettime = 1;
                invincible = true;
                invidInvictibleSlider.color = Color.red;
                
            }
            else if (currunettime <= 0)
            {
                currunettime = 0;
                invincible = false;
                invidInvictibleSlider.color = Color.white;

            }


            if(InvictibleObject.activeInHierarchy)
            {
                invidInvictibleSlider.fillAmount = currunettime / 1;
            }
        }

       if (playerstate==PlayerState.Prepare) {
            if (Input.GetMouseButton(0)) {

                playerstate = PlayerState.Playing;
            }
        } 

       if (playerstate == PlayerState.Finish) {
        
        if(Input.GetMouseButtonDown(0))
            {
                FindObjectOfType<LevelSpawner>().NextLevel();
            }
        
        }


    }

    public void shatterObstacles()
    {
        if(invincible) { ScoreManager.instance.addScore(1); }
        else
        {
            ScoreManager.instance.addScore(2);
        }
    }

    private void FixedUpdate()
    {
        if(playerstate == PlayerState.Playing)
        {
            if (carpma)
            {
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
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
                    shatterObstacles();
                    SoundManager.instance.playSoundFx(idestory, 0.5f);
                    currentObstacleNumber++;
                }

            }
            else {

                if (collision.gameObject.tag == "enemy")
                {
                    //Destroy(collision.transform.parent.gameObject);
                    collision.transform.parent.GetComponent<ObstacleController>().ShatterAllObstacles();
                    shatterObstacles();
                    SoundManager.instance.playSoundFx(destory, 0.5f);
                    currentObstacleNumber++;
                }
                else if (collision.gameObject.tag == "plane")
                {
                    Debug.Log("Game Over");
                    gameOverUI.SetActive(true);
                    playerstate = PlayerState.Finish;
                    gameObject.GetComponent<Rigidbody>().isKinematic = true;
                    ScoreManager.instance.ResetScore();
                    SoundManager.instance.playSoundFx(death, 0.5f);
                }


            }




            FindObjectOfType<GameUI>().levelSilderFill(currentObstacleNumber / (float)totalObstacleNumber);


            if(collision.gameObject.tag =="Finish" && playerstate==PlayerState.Playing)
            {
                playerstate = PlayerState.Finish;
                finishUI.SetActive(true);
                finishUI.transform.GetChild(0).GetComponent<Text>().text = "Level" + PlayerPrefs.GetInt("Level", 1);

                SoundManager.instance.playSoundFx(win, 0.5f);
            }
            
            
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (!carpma || collision.gameObject.tag=="Finish")
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
            SoundManager.instance.playSoundFx(bounce, 0.5f);
        }
    }
}
