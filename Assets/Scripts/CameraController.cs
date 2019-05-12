using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject camera;
    public GameObject ship;
    
    void Update()
    {
        camera.transform.position = new Vector3(ship.transform.position.x,ship.transform.position.y,-10); // Lætur myndavélina elta geimskipið
    }
}
