using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DamageType { True_Damage, Physical_Damage, Magic_Damage };
public enum Team { None, Red, Blue };
public enum Class { None, Player, Monster, Creep };



public class SceneManager : MonoBehaviour
{


    public int frame;


    public string[] scenePlayersIds = new string[] { };
    public void AddScenePlayersIds(string index)
    {
        List<string> myList = new List<string>(scenePlayersIds);
        myList.Add(index);
        scenePlayersIds = myList.ToArray();
    }

    private void Update()
    {
        Application.targetFrameRate = frame;
    }
}
