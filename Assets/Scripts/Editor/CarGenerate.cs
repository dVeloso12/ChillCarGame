using UnityEngine;
using UnityEditor;
using System.Collections;

public class CarGenerate : EditorWindow
{

    private string CarName;
    private GameObject baseModel;
    private GameObject LeftWheel;
    private GameObject RightWheel;

    private GameObject Effects;



    [MenuItem("GameTools/GenerateCar")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CarGenerate));
    }

    private void OnGUI()
    {
        baseModel = (GameObject)EditorGUILayout.ObjectField("Base Model",baseModel, typeof(GameObject), true);
        LeftWheel = (GameObject)EditorGUILayout.ObjectField("LeftWheel ", LeftWheel, typeof(GameObject), true);
        RightWheel = (GameObject)EditorGUILayout.ObjectField("RightWheel", RightWheel, typeof(GameObject), true);

        Effects = (GameObject)EditorGUILayout.ObjectField("Effects :",Effects, typeof(GameObject), true);
        CarName = EditorGUILayout.DelayedTextField("Car Name:", CarName);

        if (GUILayout.Button("Generate"))
        {
          GenerateCar();
        }
    }


    void GenerateCar()
    {
        var carRoot = new GameObject(CarName);
        var carRBroot = carRoot.AddComponent<Rigidbody>();
        carRBroot.mass = 950f;
        var carController = carRoot.AddComponent<PrometeoCarController>();
      

        var body = GameObject.Instantiate(baseModel,carRoot.transform);
        var bodyCollider = new GameObject("Collider");
        bodyCollider.AddComponent<MeshCollider>();
        var meshColide = bodyCollider.AddComponent<MeshCollider>();
        meshColide.sharedMesh = body.GetComponent<MeshFilter>().mesh;
        meshColide.convex = true;
        bodyCollider.transform.parent = body.transform;

        var rootWheels = new GameObject("Wheels");
        rootWheels.transform.parent = carRoot.transform;
        var MeshWheelsRoot = new GameObject("Meshes");
        MeshWheelsRoot.transform.parent = rootWheels.transform;
        var CollidersWheelsRoot = new GameObject("Colliders");
        CollidersWheelsRoot.transform.parent = rootWheels.transform;


        for (int i = 0; i < 2; i++)
        {
            string side;
            if(i == 0)
            {
                side = "Front";
            }
            else
            {
                side = "Back";
            }
            var leftWheelModel = GameObject.Instantiate(LeftWheel, MeshWheelsRoot.transform);
            leftWheelModel.name = side + "LeftWheel";
            carController.frontLeftMesh = leftWheelModel;
            var rightWheelModel = GameObject.Instantiate(RightWheel, MeshWheelsRoot.transform);
            rightWheelModel.name = side + "RightWheel";
            carController.frontRightMesh = leftWheelModel;



            var leftWheelCollider = new GameObject(side+ "LeftWheel");
            leftWheelCollider.AddComponent<WheelCollider>();
            leftWheelCollider.transform.parent = CollidersWheelsRoot.transform;
      

            var rightWheelCollider = new GameObject(side + "RightWheel");
            rightWheelCollider.AddComponent<WheelCollider>();
            rightWheelCollider.transform.parent = CollidersWheelsRoot.transform;
    

            if (side == "Front")
            {
                leftWheelCollider.transform.position = new Vector3( - 1, 0, 2 );
                rightWheelCollider.transform.position = new Vector3(+ 1, 0, 2 );
                rightWheelModel.transform.position = new Vector3( + 1, 0, 2 );
                leftWheelModel.transform.position = new Vector3(- 1, 0, 2 );

            }
            else
            {
                leftWheelCollider.transform.position = new Vector3(-1, 0, -2);
                rightWheelCollider.transform.position = new Vector3 (1, 0, -2);
                rightWheelModel.transform.position = new Vector3( 1, 0, -2 );
                leftWheelModel.transform.position = new Vector3(-1, 0, -2 );
            }
            


        }

        var effects = GameObject.Instantiate(Effects, carRoot.transform);
        effects.SetActive(false);

    }

}
