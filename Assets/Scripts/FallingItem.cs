using UnityEngine;

// Attach to each falling snack, splat, and bird prefab
public class FallingItem: MonoBehaviour
{
    public float fallSpeed = 2f;
        
    // Add properties for different snack types, point values, etc.

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);

        /*
        // Destroy the item when it goes off-screen to prevent memory leaks
        if(transform.position.y < -10f) 
        {
            Destroy(gameObject);
        }
        */
    }
}