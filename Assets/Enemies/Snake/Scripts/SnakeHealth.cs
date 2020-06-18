using System.Collections;
using UnityEngine;

public class SnakeHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 4;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private Material snakeOriginalMaterial;
    private SkinnedMeshRenderer snakeMeshRenderer;
    private int currentHealth;
    static private int s_Alive = 0;
    public int Alive { get { return s_Alive; } set { s_Alive = value; }}
    
    [SerializeField] private GameObject deathPlayer;
    
    void Awake()
    {
        snakeMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        currentHealth = maxHealth;
        s_Alive++;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        StartCoroutine(DamageVfx());

        if (currentHealth <= 0)
        {
            GameObject deathSfxPlayer = Instantiate(deathPlayer, transform.position, Quaternion.identity);
            s_Alive--;
            Debug.Log("Matou: " + s_Alive);
            Die();

        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator DamageVfx()
    {
        snakeMeshRenderer.material = damageMaterial;
        yield return new WaitForSeconds(0.3f);
        snakeMeshRenderer.material = snakeOriginalMaterial; 
    }

}
