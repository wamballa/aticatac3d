using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelector : MonoBehaviour
{
    public string selectedEnemyName;
    public EnemyDatabase enemyDatabase;
    public int selectedEnemyIndex;

    void OnEnable()
    {
        selectedEnemyName = enemyDatabase.enemyTypes[selectedEnemyIndex].name;
    }

    private void Start()
    {
        //print("Selected Enemy Index = " + selectedEnemyIndex);
    }
}
