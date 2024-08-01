using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameVenue : MonoBehaviour
{
    // Square Area Settings
    public float squareSize = 1000f; // Size of the square area
    public float wallHeight = 10f; // Height of the walls
    public float wallThickness = 1f; // Thickness of the walls
    public int numWallSegments = 200; // Number of segments to form the circular wall

    // Pole Settings
    public int numPoles = 200; // Number of poles in the square area
    public float minPoleHeight = 1f; // Minimum height of poles
    public float maxPoleHeight = 25f; // Maximum height of poles
    public float minPoleWidth = 0.1f; // Minimum width of poles
    public float maxPoleWidth = 10f; // Maximum width of poles

    // Obstacle Settings
    public int numObstacles = 50; // Number of obstacles in the square area
    public float minObstacleSize = 1f; // Minimum size of obstacles
    public float maxObstacleSize = 10f; // Maximum size of obstacles

    // Lighting Settings
    public int numLights = 20; // Number of lights in the square area
    public float minLightIntensity = 1f; // Minimum intensity of lights
    public float maxLightIntensity = 10f; // Maximum intensity of lights

    // Special Features
    public bool hasFountain = true; // Whether to create a fountain in the center of the square area
    public bool hasStage = true; // Whether to create a stage in the square area

    // Define a brown color variable
    public Color brownColor = new Color(0.55f, 0.27f, 0.07f);

    void Start()
    {
        CreateSquareArea();
        CreateCircularWalls();
        CreatePoles();
        CreateObstacles();
        CreateLights();
        CreateFountain();
        CreateStage();
    }

    void CreateSquareArea()
    {
        GameObject square = GameObject.CreatePrimitive(PrimitiveType.Plane);
        square.transform.position = Vector3.zero;
        square.transform.localScale = new Vector3(squareSize / 10, 1, squareSize / 10); // Scale the plane

        // Change floor color to dark
        Renderer squareRenderer = square.GetComponent<Renderer>();
        squareRenderer.material.color = Color.black;

        // Add a collider to the floor
        square.AddComponent<BoxCollider>();
    }

    void CreateCircularWalls()
    {
        float radius = squareSize / 2;
        for (int i = 0; i < numWallSegments; i++)
        {
            float angle = i * Mathf.PI * 2 / numWallSegments;
            Vector3 wallPosition = new Vector3(Mathf.Cos(angle) * radius, wallHeight / 2, Mathf.Sin(angle) * radius);
            Quaternion wallRotation = Quaternion.Euler(0, -angle * Mathf.Rad2Deg, 0);

            GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
            wall.transform.position = wallPosition;
            wall.transform.rotation = wallRotation;
            wall.transform.localScale = new Vector3(wallThickness, wallHeight, wallThickness * 2);

            // Add a collider to the wall
            wall.AddComponent<BoxCollider>();
        }
    }

    void CreatePoles()
    {
        for (int i = 0; i < numPoles; i++)
        {
            float poleHeight = Random.Range(minPoleHeight, maxPoleHeight);
            float poleWidthX = Random.Range(minPoleWidth, maxPoleWidth);
            float poleWidthZ = Random.Range(minPoleWidth, maxPoleWidth);
            Vector3 polePosition = new Vector3(Random.Range(-squareSize / 2, squareSize / 2), poleHeight / 2, Random.Range(-squareSize / 2, squareSize / 2));

            GameObject pole = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pole.transform.position = polePosition;
            pole.transform.localScale = new Vector3(poleWidthX, poleHeight, poleWidthZ);

            // Assign a random color to each pole
            Renderer poleRenderer = pole.GetComponent<Renderer>();
            poleRenderer.material.color = new Color(Random.value, Random.value, Random.value);

            // Add a collider to the pole
            pole.AddComponent<BoxCollider>();
        }
    }

    void CreateObstacles()
    {
        for (int i = 0; i < numObstacles; i++)
        {
            float obstacleSize = Random.Range(minObstacleSize, maxObstacleSize);
            Vector3 obstaclePosition = new Vector3(Random.Range(-squareSize / 2, squareSize / 2), obstacleSize / 2, Random.Range(-squareSize / 2, squareSize / 2));

            GameObject obstacle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            obstacle.transform.position = obstaclePosition;
            obstacle.transform.localScale = new Vector3(obstacleSize, obstacleSize, obstacleSize);

            // Assign a random color to each obstacle
            Renderer obstacleRenderer = obstacle.GetComponent<Renderer>();
            obstacleRenderer.material.color = new Color(Random.value, Random.value, Random.value);

            // Add a collider to the obstacle
            obstacle.AddComponent<SphereCollider>();
        }
    }

    void CreateLights()
    {
        for (int i = 0; i < numLights; i++)
        {
            float lightIntensity = Random.Range(minLightIntensity, maxLightIntensity);
            Vector3 lightPosition = new Vector3(Random.Range(-squareSize / 2, squareSize / 2), lightIntensity / 2, Random.Range(-squareSize / 2, squareSize / 2));

            GameObject light = new GameObject("Light");
            light.transform.position = lightPosition;
            Light lightComponent = light.AddComponent<Light>();
            lightComponent.type = LightType.Point;
            lightComponent.intensity = lightIntensity;
            lightComponent.range = 10f;
        }
    }

    void CreateFountain()
    {
        if (hasFountain)
        {
            GameObject fountain = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            fountain.transform.position = Vector3.zero;
            fountain.transform.localScale = new Vector3(10f, 10f, 10f);

            // Assign a water-like material to the fountain
            Renderer fountainRenderer = fountain.GetComponent<Renderer>();
            fountainRenderer.material.color = Color.blue;
            fountainRenderer.material.shader = Shader.Find("Standard");

            // Add a mesh collider to the fountain
            MeshCollider meshCollider = fountain.AddComponent<MeshCollider>();
            meshCollider.convex = true;
        }
    }

    void CreateStage()
    {
        if (hasStage)
        {
            GameObject stage = GameObject.CreatePrimitive(PrimitiveType.Cube);
            stage.transform.position = new Vector3(0f, 5f, -squareSize / 2 + 10f);
            stage.transform.localScale = new Vector3(squareSize / 2, 5f, 10f);

            // Assign a wood-like material to the stage
            Renderer stageRenderer = stage.GetComponent<Renderer>();
            stageRenderer.material.color = brownColor;
            stageRenderer.material.shader = Shader.Find("Standard");

            // Add a collider to the stage
            stage.AddComponent<BoxCollider>();
        }
    }
}