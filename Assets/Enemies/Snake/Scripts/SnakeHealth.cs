using UnityEngine;

public class SnakeHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    private int currentHealth;
    static private int s_Killed = 0;
    public int Killed { get { return s_Killed; } set { s_Killed = value; }}
    
    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            s_Killed++;
            Debug.Log("Matou: " + s_Killed);
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}
