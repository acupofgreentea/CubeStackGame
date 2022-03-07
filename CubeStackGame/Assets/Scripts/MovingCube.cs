using UnityEngine;
using UnityEngine.SceneManagement;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField] private MoveDirection moveDirection;

    public static MovingCube CurrentCube {get; private set;}

    public static MovingCube LastCube {get; private set;}

    public MoveDirection MoveDir {get; set;}

    private Renderer rend;

    private void OnEnable() 
    {
        if(LastCube == null)
            LastCube = GameObject.FindGameObjectWithTag("StartCube").GetComponent<MovingCube>();
        
        CurrentCube = this;

        rend = GetComponent<Renderer>();

        rend.material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube.transform.localScale.x, transform.localScale.y, LastCube.transform.localScale.z);
    }

    private Color GetRandomColor()
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f));
    }

    private void Update()
    {
        Movement();
    }

    private float GetHangover()
    {
        if(moveDirection == MoveDirection.Z)
            return transform.position.z - LastCube.transform.position.z;
        else
            return transform.position.x - LastCube.transform.position.x;
    }
    
    public void Stop()
    {
        moveSpeed = 0;

        float hangover = GetHangover();

        float max = MoveDir == MoveDirection.Z ? LastCube.transform.localScale.z : LastCube.transform.localScale.x;

        if(Mathf.Abs(hangover) >= max)
        {
            LastCube = null;
            CurrentCube = null;

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        float direction = hangover > 0 ? 1f : -1f;

        if(moveDirection == MoveDirection.Z)
            SplitCubeZ(hangover, direction);
        else
            SplitCubeX(hangover, direction);


        
        LastCube = this;
    }
    
    private void SplitCubeX(float hangover, float direction)
    {
        float newXScale = LastCube.transform.localScale.x - Mathf.Abs(hangover);

        float newXPos = LastCube.transform.position.x + (hangover / 2);


        transform.localScale = new Vector3(newXScale, transform.localScale.y, transform.localScale.z);

        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);   

        FallingCube(newXScale, direction);
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

        if(moveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockScale);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPos);
        }
        else
        {
            cube.transform.localScale = new Vector3(fallingBlockScale, transform.localScale.y, transform.localScale.z);
            cube.transform.position = new Vector3(fallingBlockZPos, transform.position.y, transform.position.z);
        }


        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = rend.material.color;
        Destroy(cube, 1f);
    }

    private void Movement()
    {
        if(MoveDir == MoveDirection.Z)
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        else
            transform.position += transform.right * moveSpeed * Time.deltaTime;
    }
}
