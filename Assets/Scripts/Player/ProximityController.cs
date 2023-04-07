using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProximityController : MonoBehaviour
{
    // Proximity sensor
    [Header("Proximity")]
    public float minDistance = 2.0f;
    public float maxDistance = 5.0f;
    public float maxOpacity = 1.0f;
    public float minOpacity = 0.0f;
    public LayerMask enemyLayer;
    public TMP_Text proximityDirectionText;

    private float proximityRadius = 5f;
    //private Image proximityImage;
    private Transform proximityPrefab;

    public Image topBarPF;
    public Image bottomBarPF;
    public Image leftBarPF;
    public Image rightBarPF;


    // Start is called before the first frame update
    void Start()
    {
        //proximityDirectionText = GameObject.Find("Warning Text").GetComponent<TMP_Text>();
        //proximityImage = GameObject.Find("ProximityOutline").GetComponent<Image>();
        //proximityPrefab = proximityImage.transform;

        topBarPF = GameObject.Find("TopBar").GetComponent<Image>();
        if (topBarPF == null) print("ERROR: no top bar");
        bottomBarPF = GameObject.Find("BottomBar").GetComponent<Image>();
        leftBarPF = GameObject.Find("LeftBar").GetComponent<Image>();
        rightBarPF = GameObject.Find("RightBar").GetComponent<Image>();





    }

    // Update is called once per frame
    void Update()
    {
        HandleProximity();
    }

    private void HandleProximity()
    {
        float currentDistance = FindNearestEnemyDistance(out Transform closestEnemy);
        float normalizedDistance = Mathf.Clamp01((currentDistance - minDistance) / (maxDistance - minDistance));
        float currentOpacity = Mathf.Lerp(maxOpacity, minOpacity, normalizedDistance);

        if (closestEnemy != null)
        {
            switch (UpdateDirectionText(closestEnemy))
            {
                case 0:
                    SetOpacity(topBarPF, currentOpacity);
                    SetOpacity(bottomBarPF, 0);
                    SetOpacity(rightBarPF, 0);
                    SetOpacity(leftBarPF, 0);
                    break;
                case 1:
                    SetOpacity(topBarPF, 0);
                    SetOpacity(bottomBarPF, currentOpacity);
                    SetOpacity(rightBarPF, 0);
                    SetOpacity(leftBarPF, 0);
                    break;
                case 2:
                    SetOpacity(topBarPF, 0);
                    SetOpacity(bottomBarPF, 0);
                    SetOpacity(rightBarPF, currentOpacity);
                    SetOpacity(leftBarPF, 0);
                    break;
                case 3:
                    SetOpacity(topBarPF, 0);
                    SetOpacity(bottomBarPF, 0);
                    SetOpacity(rightBarPF, 0);
                    SetOpacity(leftBarPF, currentOpacity); 
                    break;
                case 4:
                    print("UOIOI");
                    break;

            }
            //SetOpacity(UpdateDirectionText(closestEnemy), currentOpacity);
            //UpdateDirectionText(closestEnemy);
        }





    }

    void SetOpacity(Image proximityImage, float opacity)
    {

        Color color = proximityImage.color;
        color.a = opacity;
        proximityImage.color = color;
        
        //foreach (Transform t in proximityPrefab)
        //{
        //    color = Color.white;
        //    color.a = opacity;
        //    t.GetComponent<TMPro.TMP_Text>().color = color;
        //}
    }

    float FindNearestEnemyDistance(out Transform closestEnemyTransform)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, proximityRadius, enemyLayer);

        float minDistance = 100;
        closestEnemyTransform = null;
        //Transform closestEnemy = null;

        foreach (Collider enemyCollider in enemiesInRange)
        {
            Transform enemyTransform = enemyCollider.transform;
            float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);

            if (distanceToEnemy < minDistance)
            {
                minDistance = distanceToEnemy;
                closestEnemyTransform = enemyTransform;
            }
        }
        //print($"Num enemies = {enemiesInRange.Length}, nearestEnemy = {minDistance}");
        return minDistance;
    }

    int UpdateDirectionText(Transform enemy)
    {
        Vector3 toEnemy = enemy.position - transform.position;
        float forwardDot = Vector3.Dot(transform.forward, toEnemy.normalized);
        float rightDot = Vector3.Dot(transform.right, toEnemy.normalized);

        int dirID = 0;

        if (forwardDot > 0.5f)
        {
            //proximityDirectionText.text = "Front";
            dirID = 0;
        }
        else if (forwardDot < -0.5f)
        {
            //proximityDirectionText.text = "Behind";
            dirID = 1;
        }
        else if (rightDot > 0.5f)
        {
            //proximityDirectionText.text = "Right";
            dirID = 2;
        }
        else if (rightDot < -0.5f)
        {
            //proximityDirectionText.text = "Left";
            dirID = 3;
        }
        else
        {
            //proximityDirectionText.text = "N/A";
            dirID = 4;
        }
        return dirID;
    }
}
