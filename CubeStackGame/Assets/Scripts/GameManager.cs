using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner cubeSpawner;

    private void Awake() 
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
    }
    
    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();
                
            cubeSpawner.SpawnCube();
        }    
    }
}
