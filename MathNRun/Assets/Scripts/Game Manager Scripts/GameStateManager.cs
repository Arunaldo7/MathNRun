using System.Collections;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance;

    private GameData gameData;

    public bool isGameStartedFirstTime;

    public bool isMusicOn;

    public int selectedPlayer;

    public int totalCoins;

    public int highScore;

    public int currentScore;

    public int currentCoins;

    public int currentCorrectAns;

    public int totalCorrectAns;

    public int highCorrectAns;

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitializeGameVariables();
    }

    void InitializeGameVariables()
    {
        LoadData();

        

        if (gameData != null)
        {
            isGameStartedFirstTime = gameData.GetIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            isGameStartedFirstTime = false;
            isMusicOn = true;
            selectedPlayer = 0;

            totalCoins = 0;

            highScore = 0;

            currentScore = 0;

            currentCoins = 0;

            currentCorrectAns = 0;

            totalCorrectAns = 0;

            highCorrectAns = 0;

            gameData = new GameData();

            SaveData();
        }
        else
        {
            isGameStartedFirstTime = gameData.GetIsGameStartedFirstTime();
            isMusicOn = gameData.GetIsMusicOn();
            selectedPlayer = gameData.GetSelectedPlayer();

            totalCoins = gameData.GetTotalCoins();

            highScore = gameData.GetHighScore();

            totalCorrectAns = gameData.GetTotalCorrectAns();

            highCorrectAns = gameData.GetHighCorrectAns();

            currentScore = 0;
            currentCorrectAns = 0;
            currentCoins = 0;
        }
    }

    public void SaveData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Create(Application.persistentDataPath + "/GameData.dat");

            if (gameData != null)
            {
                gameData.SetIsGameStartedFirstTime(isGameStartedFirstTime);
                gameData.SetIsMusicOn(isMusicOn);
                gameData.SetSelectedPlayer(selectedPlayer);
                gameData.SetTotalCoins(totalCoins + currentCoins);
                if (currentScore > highScore)
                {
                    gameData.SetHighScore(currentScore);
                }
                gameData.SetTotalCorrectAns(totalCorrectAns + currentCorrectAns);

                if (currentCorrectAns > highCorrectAns)
                {
                    gameData.SetHighCorrectAns(highCorrectAns);
                }


                bf.Serialize(file, gameData);

            }

        }
        catch (Exception ex)
        {
            Debug.Log("Exception occured while creating data file : " + ex.InnerException);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }

    public void LoadData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat", FileMode.Open);

            gameData = (GameData)bf.Deserialize(file);
        }
        catch (Exception ex)
        {
            Debug.Log("Exception occured while creating data file : " + ex.InnerException);
        }
        finally
        {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}



[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;

    private bool isMusicOn;

    private int selectedPlayer;

    private int totalCoins;

    private int highScore;

    private int totalCorrectAns;

    private int highCorrectAns;

    public void SetIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool GetIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;
    }

    public void SetIsMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool GetIsMusicOn()
    {
        return this.isMusicOn;
    }

    public void SetSelectedPlayer(int selectedPlayer)
    {
        this.selectedPlayer = selectedPlayer;
    }

    public int GetSelectedPlayer()
    {
        return this.selectedPlayer;
    }

    public void SetTotalCoins(int totalCoins)
    {
        this.totalCoins = totalCoins;
    }

    public int GetTotalCoins()
    {
        return this.totalCoins;
    }

    public void SetHighScore(int highScore)
    {
        this.highScore = highScore;
    }

    public int GetHighScore()
    {
        return this.highScore;
    }

    public void SetTotalCorrectAns(int totalCorrectAns)
    {
        this.totalCorrectAns = totalCorrectAns;
    }

    public int GetTotalCorrectAns()
    {
        return this.totalCorrectAns;
    }

    public void SetHighCorrectAns(int highCorrectAns)
    {
        this.highCorrectAns = highCorrectAns;
    }

    public int GetHighCorrectAns()
    {
        return this.highCorrectAns;
    }
}
