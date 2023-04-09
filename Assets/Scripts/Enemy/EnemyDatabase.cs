using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Enemies/Enemy Database")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyType> enemyTypes = new List<EnemyType>();

    public GameObject sparklePrefab;
    public GameObject popPF;
    public List<Material> materials;

    public LookAtPlayer lookAtPlayer;
    public SetObjectColour setObjectColour;

}

[System.Serializable]
public class EnemyType
{
    public string name;
    public int health;
    public GameObject model;
}
