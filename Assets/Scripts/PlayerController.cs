using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform basketTransform; // Assign the Shiba's basket Transform in the Inspector
    public LayerMask groundLayer; // Assign a layer for the ground to implement grounded jumping later (maybe)
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isJumping = false;

    public delegate void OnCatchSnack(string snackType);
    public static event OnCatchSnack CatchSnackEvent;

    public delegate void OnBonk(string bonkType);
    public static event OnBonk BonkEvent;

    public delegate void OnEatSplat();
    public static event OnEatSplat EatSplatEvent;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(basketTransform == null)
        {
            Debug.LogError("Basket Transform not assigned to PlayerController!");
        }
    }

    void Update()
    {
        // Basic horizontal movement (replace this with touch input later)
        moveInput.x = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Basic jump (implement variable jump based on touch duration later)
        if(Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isJumping = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground")) // this might change
        {
            isJumping = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(basketTransform != null && other.transform.IsChildOf(basketTransform))
        {
            switch (other.tag)
            {
                case "SafeSnack":
                    Debug.Log("Caught a safe snack!");
                    CatchSnackEvent?.Invoke("Safe");
                    Destroy(other.gameObject);
                    break;
                case "DangerousSnack":
                    Debug.Log("Caught a dangerous snack! Game Over!");
                    // Implement Game Over logic here (load a game over scene or popup)
                    Destroy(other.gameObject);
                    break;
                case "SpecialSnack":
                    Debug.Log("Caught a special snack!");
                    CatchSnackEvent?.Invoke("Special");
                    Destroy(other.gameObject);
                    break;
            }
            /*
            if(other.CompareTag("SafeSnack"))
            {
                Debug.Log("Caught a safe snack!");
                CatchSnackEvent?.Invoke("Safe");
                Destroy(other.gameObject);
            }
            else if(other.CompareTag("DangerousSnack"))
            {
                Debug.Log("Caught a dangerous snack! Game Over!");
                // Implement Game Over logic here (load a game over scene or popup)
                Destroy(other.gameObject);
            }
            else if(other.CompareTag("SpecialSnack"))
            {
                Debug.Log("Caught a special snack!");
                CatchSnackEvent?.Invoke("Special");
                Destroy(other.gameObject);
            }
            */
        }
        else
        {
            switch (other.tag)
            {
                case "Splat":
                    Debug.Log("Shiba ate a splat!");
                    EatSplatEvent?.Invoke();
                    Destroy(other.gameObject);
                    // Implement weight gain and speed reduction logic here
                    break;
                case "Bird":
                    Debug.Log("Shiba got bonked by a bird!");
                    BonkEvent?.Invoke("Bird");
                    // Implement point penalty and temporary stun/debuff
                    Destroy(other.gameObject);
                    break;
                case "FallingSnack":
                case "FallingDangerousSnack":
                case "FallingSpecialSnack":
                    Debug.Log("Snack hit Shiba's head!");
                    BonkEvent?.Invoke("Snack");
                    // Implement point penalty and temporary severe speed debuff
                    Destroy(other.gameObject);
                    break;
            }
        }
        /*
        else if(other.CompareTag("Splat"))
        {
            Debug.Log("Shiba ate a splat!");
            EatSplatEvent?.Invoke();
            Destroy(other.gameObject);
            // Implement weight gain and speed reduction logic here
        }
        else if(other.CompareTag("Bird"))
        {
            Debug.Log("Shiba got bonked by a bird!");
            BonkEvent?.Invoke("Bird");
            // Implement point penalty and temporary stun/debuff
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("FallingSnack") || other.CompareTag("FallingDangerousSnack") || other.CompareTag("FallingSpecialSnack"))
        {
            Debug.Log("Snack hit Shiba's head!");
            BonkEvent?.Invoke("Snack");
            // Implement point penalty and temporary severe speed debuff
            Destroy(other.gameObject);
        }
        */
    }
}
