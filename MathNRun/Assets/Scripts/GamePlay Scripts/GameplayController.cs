using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    [SerializeField] private GameObject[] laneObstacleObjects;
    [SerializeField] private GameObject[] pathObstacleObjects;
    [SerializeField] private GameObject[] collectibleObjects;

    [SerializeField] private GameObject elevatedPathObject;

    [SerializeField] private GameObject[] optionsList;

    [SerializeField] private string correctOptionTag;

    [SerializeField] private GameObject caveObject;
    [SerializeField] private Transform[] lanes;
    [SerializeField] private Transform[] heights;

    [SerializeField] private float minLaneObstacleDelay;
    [SerializeField] private float maxLaneObstacleDelay;

    [SerializeField] private float minPathObstacleDelay;
    [SerializeField] private float maxPathObstacleDelay;

    [SerializeField] private float minCollectibleDelay;
    [SerializeField] private float maxCollectibleDelay;

    [SerializeField] private float minElevatedPathDelay;
    [SerializeField] private float maxElevatedPathDelay;

    [SerializeField] private float minCaveDelay;
    [SerializeField] private float maxCaveDelay;

    [SerializeField] private float minQuestionDelay;
    [SerializeField] private float maxQuestionDelay;

    public float lastCavePos;

    private float groundLength;
    private PlayerController playerController;

    [SerializeField] private int laneObstacleSpawnChance;
    [SerializeField] private int pathObstacleSpawnChance;

    [SerializeField] private int caveSpawnChance;
    [SerializeField] private int elevatedPathSpawnChance;

    [SerializeField] private int questionSpawnChance;

    private ArrayList optionsNumberList = new ArrayList();



    private float speedMultiplier;
    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        //optionsNumberList add options numbers
        optionsNumberList.Add(0);
        optionsNumberList.Add(1);
        optionsNumberList.Add(2);

        groundLength = GameObject.Find("GroundBlock").GetComponent<GenerateEnv>().groundLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        if (caveObject != null)
        {
            StartCoroutine("GenerateCaves");
        }

        if (laneObstacleObjects.Length > 0)
        {
            StartCoroutine("GenerateLaneObstacles");
        }

        if (pathObstacleObjects.Length > 0)
        {
            StartCoroutine("GeneratePathObstacles");
        }

        if (collectibleObjects.Length > 0)
        {
            StartCoroutine("GenerateCollectibleObjects");
        }

        if (elevatedPathObject != null)
        {
            StartCoroutine("GenerateElevatedPathObjects");
        }

        if (optionsList != null)
        {
            StartCoroutine("GenerateQuestion");
        }
    }

    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator GenerateQuestion()
    {
        float timer = Random.Range(minQuestionDelay, maxQuestionDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreateQuestion(playerController.gameObject.transform.position.z);

        StartCoroutine("GenerateQuestion");
    }

    void CreateQuestion(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = questionSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            speedMultiplier = playerController.frontSpeed / 5f;

            float optionsZPos = playerPos + playerController.frontSpeed + (groundLength / 2);

            ArrayList optionsNumberListClone = (ArrayList)optionsNumberList.Clone();

            for (int i = 0; i < 3; i++)
            {
                int optionNumber = Random.Range(0, optionsNumberListClone.Count);

                GameObject optionObject = optionsList[(int)optionsNumberListClone[optionNumber]];

                optionsNumberListClone.RemoveAt(optionNumber);

                Vector3 optionObjectPos = new Vector3(optionObject.transform.position.x, optionObject.transform.position.y, optionsZPos);
                if (i == 0)
                {
                    optionObject.GetComponentInChildren<Text>().text = i.ToString();
                    optionObject.tag = correctOptionTag;
                    optionObject.GetComponent<BoxCollider>().isTrigger = true;
                }
                else
                {
                    optionObject.tag = "Obstacle";
                    optionObject.GetComponentInChildren<Text>().text = "nono";
                }
                SpawnObstacle(optionObjectPos, optionObject);
            }

        }
    }

    IEnumerator GenerateElevatedPathObjects()
    {
        float timer = Random.Range(minCaveDelay, maxCaveDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreateElevatedPathObjects(playerController.gameObject.transform.position.z);

        StartCoroutine("GenerateElevatedPathObjects");
    }

    void CreateElevatedPathObjects(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = elevatedPathSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            speedMultiplier = playerController.frontSpeed / 5f;

            int obstacleLane = Random.Range(0, lanes.Length);

            float elevatedPathZPos = playerPos + playerController.frontSpeed + (groundLength / 2);

            Vector3 elevatedPathPos = new Vector3(lanes[obstacleLane].transform.position.x, elevatedPathObject.transform.position.y, elevatedPathZPos);

            SpawnObstacle(elevatedPathPos, elevatedPathObject);

        }
    }

    //Generate Obstacles based upon player speed
    //if speed is more, then obstacle spawn timer delay is less
    IEnumerator GenerateCaves()
    {
        float timer = Random.Range(minCaveDelay, maxCaveDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreateCaves(playerController.gameObject.transform.position.z);

        StartCoroutine("GenerateCaves");
    }

    void CreateCaves(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = caveSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            speedMultiplier = playerController.frontSpeed / 5f;

            float caveZPos = playerPos + playerController.frontSpeed + groundLength;

            Vector3 cavePos = new Vector3(caveObject.transform.position.x, caveObject.transform.position.y, caveZPos);

            SpawnObstacle(cavePos, caveObject);

        }
    }

    //Generate Obstacles based upon player speed
    //if speed is more, then obstacle spawn timer delay is less
    IEnumerator GenerateLaneObstacles()
    {
        float timer = Random.Range(minLaneObstacleDelay, maxLaneObstacleDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreateLaneObstacles(playerController.gameObject.transform.position.z);

        StartCoroutine("GenerateLaneObstacles");
    }

    //randomly select a lane to spawn obstacle
    //randomly select obstacleZPos to spawn an obstacle
    void CreateLaneObstacles(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = laneObstacleSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            int obstacleLane = Random.Range(0, lanes.Length);
            int obstacleType = Random.Range(0, laneObstacleObjects.Length);

            speedMultiplier = playerController.frontSpeed / 5f;

            float obstacleZPos = playerPos + Random.Range(playerController.frontSpeed + 5f, groundLength / speedMultiplier);

            Vector3 obstaclePos = new Vector3(lanes[obstacleLane].transform.position.x, lanes[obstacleLane].transform.position.y, obstacleZPos);

            SpawnObstacle(obstaclePos, laneObstacleObjects[obstacleType]);

        }
    }

    //Generate Obstacles based upon player speed
    //if speed is more, then obstacle spawn timer delay is less
    IEnumerator GenerateCollectibleObjects()
    {
        float timer = Random.Range(minCollectibleDelay, maxCollectibleDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreateCollectibleObjects(playerController.gameObject.transform.position.z);

        StartCoroutine("GenerateCollectibleObjects");
    }

    //randomly select a lane to spawn obstacle
    //randomly select obstacleZPos to spawn an obstacle
    void CreateCollectibleObjects(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = laneObstacleSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            int obstacleLane = Random.Range(0, lanes.Length);
            int obstacleType = Random.Range(0, collectibleObjects.Length);

            int heightPosition = Random.Range(0, heights.Length);

            speedMultiplier = playerController.frontSpeed / 5f;

            float obstacleZPos = playerPos + Random.Range(playerController.frontSpeed + 5f, groundLength / speedMultiplier);

            Vector3 collectiblePos = new Vector3(lanes[obstacleLane].transform.position.x, heights[heightPosition].transform.position.y, obstacleZPos);

            SpawnObstacle(collectiblePos, collectibleObjects[obstacleType]);

        }
    }

    //Generate Obstacles based upon player speed
    //if speed is more, then obstacle spawn timer delay is less
    IEnumerator GeneratePathObstacles()
    {
        float timer = Random.Range(minPathObstacleDelay, maxPathObstacleDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);


        CreatePathObstacles(playerController.gameObject.transform.position.z);

        StartCoroutine("GeneratePathObstacles");
    }

    //randomly select a lane to spawn obstacle
    //randomly select obstacleZPos to spawn an obstacle
    void CreatePathObstacles(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = pathObstacleSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            int obstacleLane = 1;
            int obstacleType = Random.Range(0, pathObstacleObjects.Length);

            speedMultiplier = playerController.frontSpeed / 5f;

            float obstacleZPos = playerPos + Random.Range(playerController.frontSpeed + 5f, groundLength / speedMultiplier);

            Vector3 obstaclePos = new Vector3(lanes[obstacleLane].transform.position.x, pathObstacleObjects[obstacleType].transform.position.y, obstacleZPos);

            SpawnObstacle(obstaclePos, pathObstacleObjects[obstacleType]);

        }
    }

    public void DestroyNearObjects()
    {
        GameObject[] obstaclesList = GameObject.FindGameObjectsWithTag("Obstacle");
        float playerZPos = playerController.transform.position.z;

        //destroy objects in near distance of player
        //especially done after reviving player
        for (int i = 0; i < obstaclesList.Length; i++)
        {
            if (obstaclesList[i].transform.position.z <= (playerZPos + groundLength / 2))
            {
                Destroy(obstaclesList[i]);
            }
        }

    }

    void SpawnObstacle(Vector3 obstaclePos, GameObject obstacleType)
    {
        GameObject obstacle = Instantiate(obstacleType, obstaclePos, obstacleType.transform.rotation);
    }

}
