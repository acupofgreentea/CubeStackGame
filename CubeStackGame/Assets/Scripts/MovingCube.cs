using UnityEngine;

public class MovingCube : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public static MovingCube CurrentCube {get; private set;}

    public static MovingCube LastCube {get; private set;}

    private void OnEnable() 
    {
        if(LastCube == null)
            LastCube = GameObject.FindGameObjectWithTag("StartCube").GetComponent<MovingCube>();
        
        CurrentCube = this;
    }

    private void Update()
    {
        Movement();
    }
    
    public void Stop()
    {
        moveSpeed = 0;

        float hangover = transform.position.z - LastCube.transform.position.z;
    }

    private void Movement()
    {
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}
