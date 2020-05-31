using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light sun;
    [SerializeField] private LightPreset lightPreset;

    [SerializeField] private float timeOfDay;

    [SerializeField] private int fullDayLength;

    [SerializeField] Material[] skyBoxList;
    // [SerializeField] Material skyBoxDay;

    bool isDay;

    private int totalTimeSlots;

    private int timePerSlot;

    private float currSunIntensity;

    private bool isIntensityChanged;

    void Start()
    {
        isDay = true;
        totalTimeSlots = skyBoxList.Length;


        if (totalTimeSlots > 0)
        {
            timePerSlot = fullDayLength / totalTimeSlots;
        }
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = lightPreset.AmbientColour.Evaluate(timePercent);
        // RenderSettings.fogColor = lightPreset.FogColour.Evaluate(timePercent);
        sun.color = lightPreset.SunColour.Evaluate(timePercent);

        sun.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360), 0f, 0f));
    }

    private void FixedUpdate()
    {
        if (Application.isPlaying)
        {
            timeOfDay = timeOfDay + Time.deltaTime;
            timeOfDay = timeOfDay % fullDayLength;

            float timePercent = timeOfDay / fullDayLength;

            UpdateLighting(timePercent);

            int timeInSeconds = (int)Time.time + 1;

            int timeSlot = (int)(timeInSeconds % fullDayLength);

            for (int i = 0; i < totalTimeSlots; i++)
            {
                if (timeSlot > (timePerSlot * i) && (timeSlot < timePerSlot * (i + 1)))
                {
                    RenderSettings.skybox = skyBoxList[i];
                    break;
                }

                if((i+1) < totalTimeSlots / 2){
                    sun.intensity = 1f;
                }else{
                    sun.intensity = 0.75f;
                }
            }

            RenderSettings.skybox.SetFloat("_Rotation", (360 / (fullDayLength)) * Time.time);
        }
    }
}
