using System;
using System.Collections;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField][Range(0.5f, 1.5f)] private float fireRate = 1f;
    [SerializeField][Range(1f,10f)] private int damage = 1;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shotSfx;
    [SerializeField] private AudioClip reloadSfx;
    private float timer;
    private int maxAmmo = 8;
    private int currentAmmo;
    [SerializeField] private float reloadTime = 1.5f;
    private bool isRealoding = false;
    [SerializeField] private TextMeshProUGUI currentAmmoText;
    [SerializeField] private LayerMask shootableLayerMask;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRealoding)
        { 
            return;
        }

        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                timer = 0f;
                FireGun();
            }
        }

        if(currentAmmo <= 0 || (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo))
        {
            StartCoroutine(Reload());
        }
        
        currentAmmoText.text = currentAmmo.ToString() + " / ∞";
    }

    private IEnumerator Reload()
    {
        isRealoding = true;
        Debug.Log("Reloading...");
        audioSource.PlayOneShot(reloadSfx, 0.3f);

        yield return new WaitForSeconds(reloadTime);

        isRealoding = false;
        currentAmmo = maxAmmo;
    }

    private void FireGun()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hitInfo;
        audioSource.PlayOneShot(shotSfx, 0.3f);
        
        currentAmmo--;

        Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 2f);


        if(Physics.Raycast(ray, out hitInfo, 200, shootableLayerMask, QueryTriggerInteraction.Ignore))
        {
            var health = hitInfo.collider.GetComponentInParent<SnakeHealth>();
            if (health != null)
            {
                Debug.Log("Acertou");
                health.TakeDamage(damage);
            }
        }

    }
}
