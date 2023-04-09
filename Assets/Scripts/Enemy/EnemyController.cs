using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Visual Effects")]
    public GameObject sparklePF;
    public GameObject firstChild;
    public GameObject popPF;

    [Header("Audio")]

    [Header("Movement")]
    public bool canMove = false;

    [Header("Setup Materials")]
    public List<Material> materials;

    private Animator animator;
    private SphereCollider sphereCollider;
    private EventManager eventManager;

    protected void Start()
    {
        InitializeComponents();
        SetupMaterial();
        PrepareForSpawn();
    }

    private void InitializeComponents()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        sphereCollider = GetComponent<SphereCollider>();
        animator = GetComponent<Animator>();

        if (sphereCollider == null) print("ERROR: no sphere collider found");
    }

    protected void SetupMaterial()
    {
        MeshRenderer[] meshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        Material targetMaterial;

        int random = UnityEngine.Random.Range(0, materials.Count);
        targetMaterial = materials[random];

        foreach (MeshRenderer m in meshRenderers)
        {
            m.material = targetMaterial;
        }
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            HandleDeath(other.gameObject);
        }
    }

    public IEnumerator ShowEnemy()
    {
        yield return new WaitForSeconds(1);
        ActivateEnemy();
    }

    public bool GetCanMove()
    {
        return canMove;
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
        eventManager.onEnemyDeath.Invoke(10);

        GameObject pop = Instantiate(popPF, transform.position, Quaternion.identity);
        Destroy(pop, 1f);
        Destroy(other);

        Destroy(gameObject);
    }


}
