using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrow;
    private float cooldownTimer = Mathf.Infinity;

    private void Attack()
    {
        cooldownTimer = 0;

        arrow[Findarrow()].transform.position = firePoint.position;
        arrow[Findarrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int Findarrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left);

        if (cooldownTimer > attackCooldown)
            Attack();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && cooldownTimer >= attackCooldown)
            Attack();
    }
}
