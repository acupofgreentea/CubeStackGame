using UnityEngine;

public class GameManager : MonoBehaviour
{
    private CubeSpawner[] cubeSpawner;

    private int index;

    private CubeSpawner currentSpawner;

    private ScoreText scoreText;

    private void Awake() 
    {
        cubeSpawner = FindObjectsOfType<CubeSpawner>();

        scoreText = FindObjectOfType<ScoreText>();
    }
    
    private void Update() 
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(MovingCube.CurrentCube != null)
                MovingCube.CurrentCube.Stop();

            index = index == 0 ? 1 : 0;
            currentSpawner = cubeSpawner[index];
                
            currentSpawner.SpawnCube();

            scoreText.UpdateScore();
        }    
    }
}
