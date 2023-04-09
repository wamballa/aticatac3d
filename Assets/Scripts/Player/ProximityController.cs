using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

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

    private Image topBarPF;
    private Image bottomBarPF;
    private Image leftBarPF;
    private Image rightBarPF;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseBars();
    }

    void Update()
    {
        HandleProximity();
    }

    private void InitialiseBars()
    {
        topBarPF = GameObject.Find("TopBar").GetComponent<Image>();
        bottomBarPF = GameObject.Find("BottomBar").GetComponent<Image>();
        leftBarPF = GameObject.Find("LeftBar").GetComponent<Image>();
        rightBarPF = GameObject.Find("RightBar").GetComponent<Image>();

        SetOpacity(topBarPF, 0f);
        SetOpacity(bottomBarPF, 0f);
        SetOpacity(rightBarPF, 0f);
        SetOpacity(leftBarPF, 0f);
    }



    private void HandleProximity()
    {
        float currentDistance = FindNearestEnemyDistance(out Transform closestEnemyTransform);
        float normalizedDistance = Mathf.Clamp01((currentDistance - minDistance) / (maxDistance - minDistance));
        float currentOpacity = Mathf.Lerp(maxOpacity, minOpacity, normalizedDistance);

        if (closestEnemyTransform != null)
        {
            int dirID = GetDirectionID(closestEnemyTransform);

            SetOpacity(topBarPF, dirID == 0 ? currentOpacity : 0f);
            SetOpacity(bottomBarPF, dirID == 1 ? currentOpacity : 0f);
            SetOpacity(rightBarPF, dirID == 2 ? currentOpacity : 0f);
            SetOpacity(leftBarPF, dirID == 3 ? currentOpacity : 0f);
        }
    }

    void SetOpacity(Image proximityImage, float opacity)
    {
        Color color = proximityImage.color;
        color.a = opacity;
        proximityImage.color = color;

    }

    float FindNearestEnemyDistance(out Transform closestEnemyTransform)
    {
        Collider[] enemiesInRange = Physics.OverlapSphere(transform.position, proximityRadius, enemyLayer);

        float minDistance = 100;
        closestEnemyTransform = null;

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
        return minDistance;
    }

    int GetDirectionID(Transform enemy)
    {
        Vector3 toEnemy = enemy.position - transform.position;
        float forwardDot = Vector3.Dot(transform.forward, toEnemy.normalized);
        float rightDot = Vector3.Dot(transform.right, toEnemy.normalized);

        if (forwardDot > 0.5f)
        {
            return 0;
        }
        else if (forwardDot < -0.5f)
        {
            return 1;
        }
        else if (rightDot > 0.5f)
        {
            return 2;
        }
        else if (rightDot < -0.5f)
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }
}
