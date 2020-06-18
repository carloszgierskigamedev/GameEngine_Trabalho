using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] private float totalHealthPoints = 100f;
    [SerializeField] private float stopTime = 0;
    [SerializeField] private float impulseForce = 0;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private AudioClip damageSfx;
    [SerializeField] private GameObject deathPlayer;
    private CharacterController characterController;
    private PlayerMovement playerMovement;
    private Vector3 playerPosition;
    private AudioSource audioSource;
    private float currentHealthPoints;
    private bool invulnerable = false;

    public float TotalHealthPoints { get { return totalHealthPoints; } }
    public float CurrentHealthPoints { get { return currentHealthPoints; } set { currentHealthPoints = value; } }

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
        currentHealthPoints = totalHealthPoints;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        healthText.text = currentHealthPoints.ToString();
    }

    public void DealDamage(float attackDamage, Vector3 enemyPosition)
    {
        if(invulnerable)
        {
            Debug.Log("Invul");
            return;
        }

        GameObject.FindObjectOfType<FlashController>().Flash();
        audioSource.PlayOneShot(damageSfx, 0.4f);
        currentHealthPoints -= attackDamage;
        StartCoroutine(FreezeMovementAndAttack(enemyPosition));
        StartCoroutine(InvulnerableTime());

        if (currentHealthPoints <= 0) 
        {
            GameObject deathSfxPlayer = Instantiate(deathPlayer, transform.position, Quaternion.identity);
            healthText.text = "0";
            Destroy(this.gameObject);
        }

    }

    private IEnumerator InvulnerableTime()
    {
        invulnerable = true;
        yield return new WaitForSeconds(1f);
        invulnerable = false;
    } 

    IEnumerator FreezeMovementAndAttack(Vector3 enemyPosition)
    {
        playerPosition = transform.position;
        Vector3 impuseDirection = playerPosition - enemyPosition;
        playerMovement.enabled = false;
        
        characterController.SimpleMove(impuseDirection * 40);

        yield return new WaitForSeconds(0.2f);

        playerMovement.enabled = true;
    }
}
