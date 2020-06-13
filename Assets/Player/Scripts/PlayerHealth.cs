using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{


    [SerializeField] private float totalHealthPoints = 100f;
    [SerializeField] private float stopTime = 0;
    [SerializeField] private float impulseForce = 0;
    [SerializeField] private TextMeshProUGUI healthText;
    private float currentHealthPoints;
    Rigidbody rigidBody;
    PlayerMovement playerMovement;
    Vector3 playerPosition;

    public float TotalHealthPoints { get { return totalHealthPoints; } }
    public float CurrentHealthPoints { get { return currentHealthPoints; } set { currentHealthPoints = value; } }

    void Awake()
    {

        currentHealthPoints = totalHealthPoints;

        playerMovement = GetComponent<PlayerMovement>();

        rigidBody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        healthText.text = currentHealthPoints.ToString();
    }

    public void DealDamage(float attackDamage, Vector3 enemyPosition)
    {
        currentHealthPoints -= attackDamage;
        StartCoroutine(FreezeMovementAndAttack(enemyPosition));

        if (currentHealthPoints <= 0) 
        {
            healthText.text = "0";
            Destroy(this.gameObject);
        }

    }

    IEnumerator FreezeMovementAndAttack(Vector3 enemyPosition)
    {
        playerPosition = transform.position;
        Vector3 impuseDirection = playerPosition - enemyPosition;
        //playerMovement.enabled = false;

        yield return new WaitForSeconds(stopTime);

        //playerMovement.enabled = true;
    }
}
