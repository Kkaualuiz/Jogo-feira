using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;

    public float speed;
    public float jumpHeight;
    private float jumpVelocity;
    public float gravity;

    public float rayRadius;
    public LayerMask layer;
    public LayerMask coinlayer;

    public float horizontalSpeed;
    private bool isMovingLeft;
    public bool isMovingRight;

    public Animator anim;
    public bool isdead;

    private GameController gc;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        gc = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * speed;

        if (controller.isGrounded)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < 1f && !isMovingRight)
            {
                isMovingRight = true;
                StartCoroutine(RightMove());
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > -1f && !isMovingLeft)
            {
                isMovingLeft = true;
                StartCoroutine(LeftMove());
            }
        }
        else
        {
            jumpVelocity -= gravity;
        }

        OnCollision();

        direction.y = jumpVelocity;

        controller.Move(direction * Time.deltaTime);
    }

    IEnumerator LeftMove()
    {
        for (float i = 0; i < 3; i += 0.1f)
        {
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }

        isMovingLeft = false;
    }

    IEnumerator RightMove()
    {
        for (float i = 0; i < 3; i += 0.1f)
        {
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }

        isMovingRight = false;
    }

    void OnCollision()
    {
        RaycastHit hit;

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayRadius, layer) && !isdead)
        {
            //CHAMA GAME OVER
            anim.SetTrigger("die");

            isdead = true;
            speed = 0;
            jumpHeight = 0;
            horizontalSpeed = 0;
            Invoke("GameOver", 2f);
            
        }


        RaycastHit coinhit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(2f, 2f, 2f)), out coinhit, rayRadius, coinlayer))
        {
            //AO BATER NA MOEDA
            gc.AddCoin();
            Destroy(coinhit.transform.gameObject);

        }
    }

    void GameOver()
    {
        gc.ShowGameOver();
    }

}

