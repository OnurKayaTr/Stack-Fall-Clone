using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class LevelSpawner : MonoBehaviour
{

    public GameObject[] obstacleModel;
    [HideInInspector]
    public GameObject[] obstaclePreafab = new GameObject[4];
    
    public GameObject winPrefab;
    private GameObject temp10obstacle, temp20obstacle;
    private int level = 1, addNumber = 7;
    float obstacleNumber;






    
    // Start is called before the first frame update
    void Start()

    {
        level = PlayerPrefs.GetInt("Level", 1);

        float randomnumber = Random.value;
        randomObstacleGeneretor();
        for (obstacleNumber=0; obstacleNumber> -level-addNumber; obstacleNumber -= 0.5f)
        {
            if(level<=20)
            {
                temp10obstacle = Instantiate(obstaclePreafab[Random.Range(0, 2)]);
            }
            if (level > 20 && level<50)
            {
                temp10obstacle = Instantiate(obstaclePreafab[Random.Range(1, 3)]);
            }
            if (level >= 50 && level < 100)
            {
                temp10obstacle = Instantiate(obstaclePreafab[Random.Range(2, 4)]);
            }
            if (level >100)
            {
                temp10obstacle = Instantiate(obstaclePreafab[Random.Range(3, 4)]);
            }
            temp10obstacle.transform.position = new Vector3(0, obstacleNumber - 0.01f, 0);
            temp10obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
            if (Mathf.Abs(obstacleNumber) >= level*0.3f&& Mathf.Abs(obstacleNumber) <= level * 0.06f)
            {
                temp10obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                temp10obstacle.transform.eulerAngles += Vector3.up * 180;
            }else if (Mathf.Abs(obstacleNumber) > level*0.8f )
            {
                temp10obstacle.transform.eulerAngles = new Vector3(0, obstacleNumber * 8, 0);
                if (randomnumber > 0.75f)
                {
                    temp10obstacle.transform.eulerAngles += Vector3.up * 180;
                }
                

            }
           



            temp10obstacle.transform.parent = FindObjectOfType<RotateManager>().transform;
        }
        temp20obstacle = Instantiate(winPrefab);
        temp20obstacle.transform.position  = new Vector3(0, obstacleNumber - 0.01f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void randomObstacleGeneretor()
    {
        int random = Random.Range(0, 5);

        switch (random)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePreafab[i] = obstacleModel[i];
                }
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePreafab[i] = obstacleModel[i+4];
                }
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePreafab[i] = obstacleModel[i + 8];
                }
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePreafab[i] = obstacleModel[i + 12];
                }
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                {
                    obstaclePreafab[i] = obstacleModel[i + 16];
                }
                break;
            default:
                break;
        }
    }




    public void NextLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        SceneManager.LoadScene(0);
    }
}
