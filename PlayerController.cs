using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public Rigidbody rb;
    public Collider boxCollider;
    public Animator anim;
    public float fallrate;
    private float moveX;

    public bool gameOver;

    public CharacterCustomization ccustom;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = this.GetComponent<Animator>();
        boxCollider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetBool("startingAnim") == false && gameOver == false)
        {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
            boxCollider.enabled = true;

            Falling();
        }
    }

    // Falling rate
    void Falling()
    {
        transform.position -= transform.up * Time.deltaTime * fallrate;
        fallrate += 0.1f * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        Vector3 velocity = rb.velocity;
        velocity.x = moveX;
        rb.velocity = velocity;
    }
    
    void OnCollisionEnter(Collision collide)
    {
        if(collide.gameObject.CompareTag("Platform"))
        {
            anim.SetBool("hitPlatform", true);
            gameOver = true;
            rb.isKinematic = true;

            FindObjectOfType<AudioManager>().Pause("MainMusic");
            FindObjectOfType<AudioManager>().StopPlaying("NylonFlapping");
            FindObjectOfType<AudioManager>().StopPlaying("Wind");
            
            FindObjectOfType<AudioManager>().Play("HitCloud");
            FindObjectOfType<AudioManager>().Play("GameOver");

            foreach (var customization in ccustom.Customizations)
            {
                customization.subObjectIndex = 0;
                customization.UpdateSubObjects();
            }
        }
    }
}
