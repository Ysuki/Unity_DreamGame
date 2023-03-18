using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SyncUiInfo : MonoBehaviour
{

    public Statistics statistics;
    public Slider sliderHealth;
    public Slider sliderMana;
    public TMP_Text level;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sliderHealth.maxValue = statistics.maxHealth;
        sliderHealth.value = statistics.health;
        sliderMana.maxValue = statistics.maxMana;
        sliderMana.value = statistics.mana;
        level.text = statistics.level.ToString();
    }
}
