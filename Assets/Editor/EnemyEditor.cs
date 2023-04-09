using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemySelector))]
public class EnemySpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EnemySelector enemySelector = (EnemySelector)target;

        // Get the names of all the enemies in the database
        string[] enemyNames = new string[enemySelector.enemyDatabase.enemyTypes.Count];
        for (int i = 0; i < enemySelector.enemyDatabase.enemyTypes.Count; i++)
        {
            enemyNames[i] = enemySelector.enemyDatabase.enemyTypes[i].name;
        }

        // Show a dropdown menu to select the enemy name
        int selectedIndex = EditorGUILayout.Popup("Enemy Type", enemySelector.selectedEnemyIndex, enemyNames);
        enemySelector.selectedEnemyIndex = selectedIndex;
    }
}
