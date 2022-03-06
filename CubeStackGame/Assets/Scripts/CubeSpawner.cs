using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cubePrefab;


    public void SpawnCube()
    {
        var cube = Instantiate(cubePrefab);

        if(MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.FindGameObjectWithTag("StartCube"))
        {
            cube.transform.position = new Vector3(transform.position.x, MovingCube.LastCube.transform.position.y + cube.transform.localScale.y, transform.position.z);
        }
        else
        {
            cube.transform.position = transform.position;
        }

    }
}
