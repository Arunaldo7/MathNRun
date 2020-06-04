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

    [SerializeField] private GameObject[] potionObjects;

    [SerializeField] private string[] destroyableObjectTags;

    [SerializeField] private GameObject[] optionsList;

    [SerializeField] public GameObject question;

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

    [SerializeField] private float minPotionDelay;
    [SerializeField] private float maxPotionDelay;

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

    [SerializeField] private int potionSpawnChance;

    [SerializeField] private int collectibleObjectSpawnChance;


    [SerializeField] private int questionSpawnChance;

    public bool playGame;

    private string[] operators = { "+", "-", "*", "/" };

    private ArrayList optionsNumberList = new ArrayList();


    private float speedMultiplier;
    private void Awake()
    {
        MakeInstance();
    }

    // Start is called before the first frame update
    void Start()
    {
        playGame = true;
        //optionsNumberList add options numbers
        optionsNumberList.Add(0);
        optionsNumberList.Add(1);
        optionsNumberList.Add(2);

        groundLength = GameObject.Find("GroundBlock").GetComponent<GenerateEnv>().groundLength;
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        StartCoroutines();
    }

    public void StartCoroutines()
    {
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

        if (optionsList.Length > 0)
        {
            StartCoroutine("GenerateQuestion");
        }

        if (potionObjects.Length > 0)
        {
            StartCoroutine("GeneratePotionObject");
        }
    }

    public void StopCoroutines()
    {
        StopAllCoroutines();
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

        //new question will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreateQuestion(playerController.gameObject.transform.position.z);
        }

        StartCoroutine("GenerateQuestion");
    }

    private int GetCorrectAnswer(string selectedOperator, int number1, int number2)
    {
        switch (selectedOperator)
        {
            case "+":
                return number1 + number2;
            case "-":
                return number1 - number2;
            case "*":
                return number1 * number2;
            case "/":
                return number1 / number2;
        }

        return 0;
    }

    private int[] ChooseNumbers(string selectedOperator)
    {
        int[] numbers = new int[2];

        //if multiply choose any one number as one digit - for easy calculation
        if (selectedOperator == "*")
        {
            int choose = Random.Range(0, 2);
            if (choose == 0)
            {
                numbers[0] = Random.Range(2, 100);
                numbers[1] = Random.Range(2, 10);
            }
            else
            {
                numbers[0] = Random.Range(2, 10);
                numbers[1] = Random.Range(2, 100);
            }
        }
        else
        {
            numbers[0] = Random.Range(2, 100);
            numbers[1] = Random.Range(2, 100);
        }

        if (selectedOperator == "/")
        {
            while ((numbers[0] % numbers[1] != 0) || (numbers[0] == numbers[1]))
            {
                numbers[0] = Random.Range(2, 100);
                numbers[1] = Random.Range(2, 100);
            }
        }
        else if (selectedOperator == "-")
        {
            while (numbers[0] == numbers[1])
            {
                numbers[0] = Random.Range(2, 100);
                numbers[1] = Random.Range(2, 100);
            }
        }

        return numbers;
    }

    void CreateQuestion(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = questionSpawnChance / 10;


        if (0 <= randomNum && randomNum <= maxNum)
        {
            float optionsZPos = playerPos + playerController.frontSpeed + groundLength;

            //select random operator first and then select numbers as random
            //if operator is /(divide) then select both numbers only if number1 is divisible by number2

            string selectedOperator = operators[Random.Range(0, operators.Length)];

            int[] questionNumbers = ChooseNumbers(selectedOperator);

            int correctAnswer = GetCorrectAnswer(selectedOperator, questionNumbers[0], questionNumbers[1]);

            int[] wrongAnswers = new int[2];

            int[] wrongOptionOneNumbers = ChooseNumbers(selectedOperator);

            wrongAnswers[0] = GetCorrectAnswer(selectedOperator, wrongOptionOneNumbers[0], wrongOptionOneNumbers[1]);

            while (wrongAnswers[0] == correctAnswer)
            {
                wrongOptionOneNumbers = ChooseNumbers(selectedOperator);
                wrongAnswers[0] = GetCorrectAnswer(selectedOperator, wrongOptionOneNumbers[0], wrongOptionOneNumbers[1]);
            }

            int[] wrongOptionTwoNumbers = ChooseNumbers(selectedOperator);
            wrongAnswers[1] = GetCorrectAnswer(selectedOperator, wrongOptionTwoNumbers[0], wrongOptionTwoNumbers[1]);

            while (wrongAnswers[1] == correctAnswer && wrongAnswers[1] == wrongAnswers[0])
            {
                wrongOptionTwoNumbers = ChooseNumbers(selectedOperator);
                wrongAnswers[1] = GetCorrectAnswer(selectedOperator, wrongOptionTwoNumbers[0], wrongOptionTwoNumbers[1]);
            }


            question.GetComponentInChildren<Text>().text = questionNumbers[0].ToString() + selectedOperator + questionNumbers[1].ToString();
            question.SetActive(true);

            ArrayList optionsNumberListClone = (ArrayList)optionsNumberList.Clone();

            Vector3 optionObjectPos = new Vector3(0f, 0f, 0f);
            for (int i = 0; i < 3; i++)
            {
                int optionNumber = Random.Range(0, optionsNumberListClone.Count);

                GameObject optionObject = optionsList[(int)optionsNumberListClone[optionNumber]];

                optionsNumberListClone.RemoveAt(optionNumber);

                optionObjectPos = new Vector3(optionObject.transform.position.x, optionObject.transform.position.y, optionsZPos);
                //correct answer spawns first
                if (i == 0)
                {
                    optionObject.GetComponentInChildren<Text>().text = correctAnswer.ToString();
                    optionObject.tag = correctOptionTag;
                    optionObject.GetComponent<BoxCollider>().isTrigger = true;
                }
                else
                {
                    optionObject.tag = "Obstacle";
                    optionObject.GetComponentInChildren<Text>().text = wrongAnswers[i - 1].ToString();
                    optionObject.GetComponent<BoxCollider>().isTrigger = false;
                }
                SpawnObstacle(optionObjectPos, optionObject);
            }
        }
    }

      IEnumerator GeneratePotionObject()
    {
        float timer = Random.Range(minPotionDelay, maxPotionDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);

        //new object will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreatePotionObject(playerController.gameObject.transform.position.z);
        }

        StartCoroutine("GeneratePotionObject");
    }

    void CreatePotionObject(float playerPos)
    {
        int randomNum = Random.Range(0, 10);

        int maxNum = potionSpawnChance / 10;

        if (0 <= randomNum && randomNum <= maxNum)
        {
            speedMultiplier = playerController.frontSpeed / 5f;

            int obstacleLane = Random.Range(0, lanes.Length);
            int potionType = Random.Range(0, laneObstacleObjects.Length);

            float potionObjectZPos = playerPos + playerController.frontSpeed + (groundLength / 2);

            Vector3 potionObjectPos = new Vector3(lanes[obstacleLane].transform.position.x, potionObjects[potionType].transform.position.y, potionObjectZPos);

            SpawnObstacle(potionObjectPos, potionObjects[potionType]);

        }
    }

    IEnumerator GenerateElevatedPathObjects()
    {
        float timer = Random.Range(minCaveDelay, maxCaveDelay) / playerController.frontSpeed;
        yield return new WaitForSeconds(timer);

        //new object will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreateElevatedPathObjects(playerController.gameObject.transform.position.z);
        }

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

        //new object will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreateCaves(playerController.gameObject.transform.position.z);
        }


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

        //new object will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreateLaneObstacles(playerController.gameObject.transform.position.z);
        }


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

        int maxNum = collectibleObjectSpawnChance / 10;

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

        //new object will be created only when there is no question currently active
        if (!question.gameObject.activeInHierarchy)
        {
            CreatePathObstacles(playerController.gameObject.transform.position.z);
        }


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
        float playerZPos = playerController.transform.position.z;

        for (int i = 0; i < destroyableObjectTags.Length; i++)
        {
            GameObject[] destroyableObjectList = GameObject.FindGameObjectsWithTag(destroyableObjectTags[i]);

            //destroy all present objects in near distance of player
            //especially done after reviving player
            for (int j = 0; j < destroyableObjectList.Length; j++)
            {
                Destroy(destroyableObjectList[j]);
            }
        }

        question.SetActive(false);

        //start all couroutines after 5 seconds
        Invoke("StartCoroutines", 5.0f);

    }

    void SpawnObstacle(Vector3 obstaclePos, GameObject obstacleType)
    {
        GameObject obstacle = Instantiate(obstacleType, obstaclePos, obstacleType.transform.rotation);
    }

}
