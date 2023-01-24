using UnityEngine;
using UnityEditor;
using System.Collections;

public class ChunkGenerate : EditorWindow
{
    private int UNITVALUE = 5;
    private Vector2 ChunkSize;
    private GameObject PointPrefab;
    private GameObject ExitPointPrefab;
    private enum ExitPoint
    {
        North,South,West,East
    };
    private ExitPoint exit_point;

    [MenuItem("GameTools/ChunkGenerate")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ChunkGenerate));
    }

    private void OnGUI()
    {
        ChunkSize = EditorGUILayout.Vector2Field("Chunk Size", ChunkSize);
        PointPrefab = (GameObject)EditorGUILayout.ObjectField("Point Prefab", PointPrefab, typeof(GameObject), true);
        ExitPointPrefab = (GameObject)EditorGUILayout.ObjectField("ExitPoint Prefab", ExitPointPrefab, typeof(GameObject), true);
        exit_point = (ExitPoint)EditorGUILayout.EnumFlagsField("Exit Point Direction : ", exit_point);

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
        //PointPrefab.transform.localScale = new Vector3(ChunkSize.x / 2f, 2f, ChunkSize.y / 2f);
        var WestPoint = Instantiate(PointPrefab,new Vector3(point, -1.26f, 0),Quaternion.identity, root.transform);
        WestPoint.transform.localEulerAngles = new Vector3(0, 90f, 0);
        WestPoint.name = "ExitPointWest";
        var NorthPoint = Instantiate(PointPrefab, new Vector3(0, -1.26f, point), Quaternion.identity,root.transform);
        NorthPoint.name = "ExitPointNorth";
        var EastPoint = Instantiate(PointPrefab, new Vector3(-point, -1.26f, 0), Quaternion.identity, root.transform);
        EastPoint.transform.localEulerAngles = new Vector3(0, -90f, 0);
        EastPoint.name = "ExitPointEast";
        var SouthPoint = Instantiate(PointPrefab, new Vector3(0, -1.26f, -point), Quaternion.identity, root.transform);
        SouthPoint.name = "ExitPointSouth";
        switch(exit_point)
        {
            case ExitPoint.East:
                {
                    var exit = Instantiate(ExitPointPrefab, new Vector3(0, 0, 0), Quaternion.identity, EastPoint.transform);
                    exit.transform.localScale = new Vector3(1, 15, 1);
                    exit.transform.localPosition = new Vector3(0, 8, 0);
                    break;
                }
            case ExitPoint.North:
                {
                    var exit = Instantiate(ExitPointPrefab, new Vector3(0, 0, 0), Quaternion.identity, NorthPoint.transform);
                    exit.transform.localScale = new Vector3(1, 15, 1);
                    exit.transform.localPosition = new Vector3(0, 8, 0);

                    break;
                }
            case ExitPoint.West:
                {
                    var exit = Instantiate(ExitPointPrefab, new Vector3(0, 0, 0), Quaternion.identity, WestPoint.transform);
                    exit.transform.localScale = new Vector3(1, 15, 1);
                    exit.transform.localPosition = new Vector3(0, 8, 0);

                    break;
                }
            case ExitPoint.South:
                {
                    var exit = Instantiate(ExitPointPrefab, new Vector3(0, 0, 0), Quaternion.identity, SouthPoint.transform);
                    exit.transform.localScale = new Vector3(1, 15, 1);
                    exit.transform.localPosition = new Vector3(0, 8, 0);

                    break;
                }
        }

    }

}
