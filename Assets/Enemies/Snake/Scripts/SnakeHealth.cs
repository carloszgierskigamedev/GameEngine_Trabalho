using UnityEngine;

public class SnakeHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    private int currentHealth;
    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Die(); 
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
