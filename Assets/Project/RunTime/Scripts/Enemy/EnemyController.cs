using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Visual Effects")]
    public GameObject sparklePF;
    public GameObject firstChild;
    public GameObject popPF;

    [Header("Audio")]
    public AudioClip deathSound;
    private AudioSource audioSource;

    [Header("Movement")]
    public bool canMove = false;

    private Animator animator;
    private SphereCollider sphereCollider;

    protected void Start()
    {
        InitializeComponents();
        PrepareForSpawn();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            HandleDeath(other.gameObject);
        }
    }

    public IEnumerator ShowEnemy()
    {
        yield return new WaitForSeconds(2);
        ActivateEnemy();
    }

    public bool GetCanMove()
    {
        return canMove;
    }

    private void InitializeComponents()
    {
        audioSource = GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();

        if (sphereCollider == null) print("ERROR: no sphere collider found");
    }

    private void PrepareForSpawn()
    {
        canMove = false;

        if (animator != null)
        {
            animator.enabled = false;
        }

        sphereCollider.enabled = false;
    }

    private void ActivateEnemy()
    {
        sphereCollider.enabled = true;
        sparklePF.SetActive(false);
        firstChild.SetActive(true);
        animator.enabled = true;
        canMove = true;
    }

    private void HandleDeath(GameObject other)
    {
        GameObject pop = Instantiate(popPF, transform.position, Quaternion.identity);
        audioSource.PlayOneShot(deathSound);
        Destroy(pop, 1f);
        Destroy(gameObject);
        Destroy(other);
    }
}
