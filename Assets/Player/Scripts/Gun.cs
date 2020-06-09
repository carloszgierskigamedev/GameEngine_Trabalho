using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField][Range(0.5f, 1.5f)] private float fireRate = 1f;
    [SerializeField][Range(1f,10f)] private int damage = 1;
    private AudioSource audioSource;
    [SerializeField] private AudioClip shot;
    private float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= fireRate)
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                timer = 0f;
                FireGun();
            }
        }
    }

    private void FireGun()
    {
        
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hitInfo;
        audioSource.PlayOneShot(shot, 0.3f);

        Debug.DrawRay(ray.origin, ray.direction * 200, Color.red, 2f);


        if(Physics.Raycast(ray, out hitInfo, 200))
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
