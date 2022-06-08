using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CubeSpawner : MonoBehaviour
{
    public static CubeSpawner instance;
    private Queue<Cube> cubesQueue = new Queue<Cube>();
    [SerializeField] private int cubesQueueCapacity = 20;
    [SerializeField] private bool autoQueueGrow = true;
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Color[] cubeColors;
    [HideInInspector] public int maxCubeNumber;
    private int maxPower = 12;
    private Vector3 defaultSpawnPosition;

    private void Awake()
    {
        instance = this;
        defaultSpawnPosition = this.transform.position;
        maxCubeNumber = (int)Mathf.Pow(2, maxPower);
        InitializeCubesQueue();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    #region Functions

    private void InitializeCubesQueue()
    {
        for (int i = 0; i < cubesQueueCapacity; i++)
        {
            AddCubeToQueue();
        }
    }

    private void AddCubeToQueue()
    {
        Cube cube = Instantiate(cubePrefab, defaultSpawnPosition, Quaternion.identity, transform).GetComponent<Cube>();
        cube.gameObject.SetActive(false);
        cube.isMainCube = false;
        cubesQueue.Enqueue(cube);
    }

    public Cube Spawn(int number, Vector3 position)
    {
        if (cubesQueue.Count == 0)
        {
            if (autoQueueGrow)
            {
                cubesQueueCapacity++;
                AddCubeToQueue();;
            }
            else
            {
                Debug.LogError(("[Cubes Queue]: no more cubes available in the pool"));
            }
        }

        Cube cube = cubesQueue.Dequeue();
        cube.transform.position = position;
        cube.SetNumber(number);
        cube.SetColor(GetColor(number));
        cube.gameObject.SetActive(true);
        return cube;

    }

    public Cube SpawnRandom()
    {
        return Spawn(GenerateRandomNumber(), defaultSpawnPosition);
    }

    public void DestroyCube(Cube cube)
    {
        cube.cubeRigidbody.velocity=Vector3.zero;
        cube.cubeRigidbody.angularVelocity = Vector3.zero;
        cube.transform.rotation=Quaternion.identity;
        cube.isMainCube = false;
        cube.gameObject.SetActive(false);
        cubesQueue.Enqueue(cube);

    }
    public int GenerateRandomNumber()
    {
        int randomNumber = Random.Range(1, 6);
        return (int)Mathf.Pow(2,randomNumber );
    } 

    private Color GetColor(int number)
    {
        return cubeColors[(int)(Mathf.Log(number) / Mathf.Log(2))-1];
    }
    #endregion
}
