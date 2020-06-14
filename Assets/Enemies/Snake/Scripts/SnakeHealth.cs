using System.Collections;
using UnityEngine;

public class SnakeHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    private AudioSource audioSource;
    private int currentHealth;
    static private int s_Alive = 0;
    public int Alive { get { return s_Alive; } set { s_Alive = value; }}
    
    [SerializeField] private AudioClip deathSfx;
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        s_Alive++;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            s_Alive--;
            Debug.Log("Matou: " + s_Alive);
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
