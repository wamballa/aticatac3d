using UnityEngine;
using UnityEditor;

public class StaircaseGenerator : EditorWindow
{
    private int numberOfSteps = 1;
    private float stepWidth = 1f;
    private float stepHeight = 0.2f;
    private float stepDepth = 1f;
    private GameObject parentObject;

    [MenuItem("Tools/Staircase Generator")]
    public static void ShowWindow()
    {
        GetWindow<StaircaseGenerator>("Staircase Generator");
    }

    private void OnGUI()
    {
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Staircase Generator", EditorStyles.boldLabel);

        numberOfSteps = EditorGUILayout.IntField("Number of Steps", numberOfSteps);
        stepWidth = EditorGUILayout.FloatField("Step Width", stepWidth);
        stepHeight = EditorGUILayout.FloatField("Step Height", stepHeight);
        stepDepth = EditorGUILayout.FloatField("Step Depth", stepDepth);

        EditorGUILayout.Space();

        parentObject = (GameObject)EditorGUILayout.ObjectField("Parent Object", parentObject, typeof(GameObject), true);

        EditorGUILayout.Space();

        if (GUILayout.Button("Generate Staircase"))
        {
            GenerateStaircase();
        }
    }

    private void GenerateStaircase()
    {
        if (parentObject == null)
        {
            parentObject = new GameObject("Staircase");
        }

        for (int i = 0; i < numberOfSteps; i++)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = new Vector3(stepWidth, stepHeight, stepDepth);
            cube.transform.position = new Vector3(0, stepHeight * i + stepHeight / 2, stepDepth * i + stepDepth / 2);
            cube.transform.SetParent(parentObject.transform);
        }
    }
}
