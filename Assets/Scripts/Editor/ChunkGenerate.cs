using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChunkGenerate : EditorWindow
{
    private int UNITVALUE = 5;
    private Vector2 ChunkSize;
    private GameObject PointPrefab;

    [MenuItem("GameTools/ChunkGenerate")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChunkGenerate));
    }

    private void OnGUI()
    {
        ChunkSize = EditorGUILayout.Vector2Field("Chunk Size", ChunkSize);
        PointPrefab = (GameObject)EditorGUILayout.ObjectField("Point Prefab", PointPrefab, typeof(GameObject), true);


        if (GUILayout.Button("Generate"))
        {
            CreateChuck();
        }

    }

    void CreateChuck()
    {
        var root = new GameObject("Chunk");
        var ground = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ground.name = "Ground";
        ground.transform.localScale = new Vector3(ChunkSize.x,3f,ChunkSize.y);
        ground.transform.parent = root.transform;
        float point = ChunkSize.x * UNITVALUE;
        PointPrefab.transform.localScale = new Vector3(ChunkSize.x / 2f, 2f, ChunkSize.y / 2f);
        var WestPoint = Instantiate(PointPrefab,new Vector3(point,0,0), Quaternion.identity,root.transform);
        WestPoint.name = "ExitPointWest";
        var NorthPoint = Instantiate(PointPrefab, new Vector3(0, 0, point), Quaternion.identity,root.transform);
        NorthPoint.name = "ExitPointNorth";
        var EastPoint = Instantiate(PointPrefab, new Vector3(-point, 0, 0), Quaternion.identity, root.transform);
        EastPoint.name = "ExitPointEast";
        var SouthPoint = Instantiate(PointPrefab, new Vector3(0, 0, -point), Quaternion.identity, root.transform);
        SouthPoint.name = "ExitPointSouth";

    }

}
