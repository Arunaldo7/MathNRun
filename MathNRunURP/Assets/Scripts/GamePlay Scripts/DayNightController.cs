using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightController : MonoBehaviour
{
    //One Full Day Length(In Seconds)
    [SerializeField] private int fullDayLength;

    [SerializeField] private GameObject sun;
    // [SerializeField] private GameObject moon;

    [SerializeField] Material[] skyBoxList;

    private int totalTimeSlots;

    private int timePerSlot;

    [SerializeField] private Color dayColor;
    [SerializeField] private Color nightColor;

    [SerializeField] private float dayLightIntensity;
    [SerializeField] private float nightLightIntensity;

    private float rotationX;

    private bool isDay;

    float timeOfDay;

    float timePercent;

    int previousSlot, currentSlot;


    // Start is called before the first frame update
    void Start()
    {
        isDay = true;
        rotationX = 0f;
        timeOfDay = 0f;
        timePercent = 0f;
        currentSlot = 0;
        previousSlot = 0;
        totalTimeSlots = skyBoxList.Length;

        if (totalTimeSlots > 0)
        {
            timePerSlot = fullDayLength / totalTimeSlots;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameplayController.instance.playGame)
        {
            // moon.transform.Rotate(-(360 / fullDayLength) * Time.deltaTime, 0f, 0f);


            timeOfDay = (timeOfDay + Time.deltaTime) % fullDayLength;
            timePercent = timeOfDay / fullDayLength;
            rotationX = timePercent * 360;
            //As we have to rotate 360 degree for one complete cycle,
            //divide it by fullDayLength to get exact day night cycle time as preferred(in seconds)
            if (timePercent <= 0.5)
            {
                sun.transform.localRotation = Quaternion.Euler(new Vector3(rotationX, 6f, 0f));
            }
            else
            {
                sun.transform.localRotation = Quaternion.Euler(new Vector3(360 - rotationX, -6f, 0f));
            }

            //for first half of day, display the day skybox
            //for rest of the day, display night skybox
            currentSlot = (int)Mathf.Floor(timeOfDay / timePerSlot);


            if (currentSlot != previousSlot)
            {
                previousSlot = currentSlot;
                RenderSettings.skybox = skyBoxList[currentSlot];
                if (((currentSlot + 1) < totalTimeSlots / 2) && !isDay)
                {
                    sun.gameObject.GetComponent<Light>().color = dayColor;
                    sun.gameObject.GetComponent<Light>().intensity = dayLightIntensity;
                    isDay = true;
                }
                else if (((currentSlot + 1) >= totalTimeSlots / 2) && isDay)
                {
                    sun.gameObject.GetComponent<Light>().color = nightColor;
                    sun.gameObject.GetComponent<Light>().intensity = nightLightIntensity;
                    isDay = false;
                }
            }
            // RenderSettings.skybox.SetFloat("_Rotation", totalTimeSlots * Time.time);
        }
    }
}
