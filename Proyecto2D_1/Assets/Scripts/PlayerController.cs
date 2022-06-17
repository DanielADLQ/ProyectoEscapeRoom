using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float verticalInput;
    public float horizontalInput;
    private bool isWalking;
    private Animator anim;
    public bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 6;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(canMove)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");

            if(horizontalInput!=0 || verticalInput!=0){

                isWalking = true;
                anim.SetBool("isWalking",isWalking);

                anim.SetFloat("MoveX",horizontalInput);
                anim.SetFloat("MoveY",verticalInput);

                this.transform.Translate(Vector3.up * playerSpeed * Time.deltaTime * verticalInput);
                this.transform.Translate(Vector3.right * playerSpeed * Time.deltaTime * horizontalInput);
            
            }
            else
            {
                isWalking = false;
                anim.SetBool("isWalking",isWalking);
            }
        }
    }
}
