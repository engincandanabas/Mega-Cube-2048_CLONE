using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushForce;
    [SerializeField] private float cubeMaxPosX;
    [Space] [SerializeField] private TouchSlider _touchSlider;
    private Cube mainCube;
    private bool isPointerDown;
    private bool canMove;
    private Vector3 cubePos;
    
    void Start()
    {
        // Spawn Cube
        SpawnCube();
        canMove = true;
        // Spawn new cube
        //Listen to slider events
        _touchSlider.onPointerDownEvent += OnPointerDown;
        _touchSlider.onPointerDragEvent += OnPointerDrag;
        _touchSlider.onPointerUpEvent += OnPointerUp;
    }

    void Update()
    {
        if (isPointerDown)
            mainCube.transform.position = Vector3.Lerp(
                mainCube.transform.position,
                cubePos,
                moveSpeed * Time.deltaTime);
    }

    #region Functions

    private void OnPointerDown()
    {
        isPointerDown = true;
    }

    private void OnPointerDrag(float xMovement)
    {
        if (isPointerDown)
        {
            cubePos = mainCube.transform.position;
            cubePos.x = xMovement * cubeMaxPosX;
        }
        
    }

    private void OnPointerUp()
    {
        if (isPointerDown && canMove)
        {
            isPointerDown = false;
            canMove = false;
        }
        //Push the cube
        mainCube.cubeRigidbody.AddForce(Vector3.forward*pushForce,ForceMode.Impulse);
        //Spawn a new cube
        Invoke("SpawnNewCube",0.3f);
    }

    private void SpawnNewCube()
    {
        mainCube.isMainCube = false;
        canMove = true;
        SpawnCube();
    }
    private void SpawnCube()
    {
        mainCube = CubeSpawner.instance.SpawnRandom();
        mainCube.isMainCube = true;
        
        //reset cubePos
        cubePos = mainCube.transform.position;
    }
    private void OnDestroy()
    {
        _touchSlider.onPointerDownEvent -= OnPointerDown;
        _touchSlider.onPointerDragEvent -= OnPointerDrag;
        _touchSlider.onPointerUpEvent -= OnPointerUp;
    }

    #endregion
}
