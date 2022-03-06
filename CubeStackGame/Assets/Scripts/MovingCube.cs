using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public static MovingCube CurrentCube {get; private set;}

    public static MovingCube LastCube {get; private set;}

    private Renderer rend;

    private void OnEnable() 
    {
        if(LastCube == null)
            LastCube = GameObject.FindGameObjectWithTag("StartCube").GetComponent<MovingCube>();
        
        CurrentCube = this;

        rend = GetComponent<Renderer>();

        rend.material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void Update()
    {
        Movement();
    }
    
    public void Stop()
    {
        moveSpeed = 0;

        float hangover = transform.position.z - LastCube.transform.position.z;

        float direction = hangover > 0 ? 1f : -1f;

        SplitCubeZ(hangover, direction);
    }
    
    private void SplitCubeZ(float hangover, float direction)
    {
        float newZScale = LastCube.transform.localScale.z - Mathf.Abs(hangover);

        float newZPos = LastCube.transform.position.z + (hangover / 2);


        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZScale);

        transform.position = new Vector3(transform.position.x, transform.position.y, newZPos);   

        FallingCube(newZScale, direction);
    }

    private void FallingCube(float newZScale, float direction)
    {
        float fallingBlockScale = transform.localScale.z - newZScale;

        float cubeEdge = transform.position.z + (newZScale / 2f * direction);

        float fallingBlockZPos = cubeEdge + fallingBlockScale / 2f * direction;

        SpawnDropCube(fallingBlockZPos, fallingBlockScale);
    }

    private void SpawnDropCube(float fallingBlockZPos, float fallingBlockScale)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockScale);
        cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);

        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = rend.material.color;
        Destroy(cube, 1f);
    }

    private void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
