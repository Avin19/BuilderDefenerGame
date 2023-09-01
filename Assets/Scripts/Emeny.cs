using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emeny : MonoBehaviour
{

    public static Emeny Create(Vector3 position)
    {
        Transform pfemeny = Resources.Load<Transform>("Emeny");
        Transform enemyTransform = Instantiate(pfemeny, position, Quaternion.identity);

        Emeny emeny = enemyTransform.GetComponent<Emeny>();
        return emeny;


    }
    private Transform targetTransfrom;
    private Rigidbody2D rigidbody2D;
    private HealthSystem healthSystem;
    private float lookForTimer, maxlookforTimer = 0.2f;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        if (BuildingManager.Instance.GetHqBuilding() != null)
        {
            targetTransfrom = BuildingManager.Instance.GetHqBuilding().transform;
        }
        lookForTimer = Random.Range(0f, maxlookforTimer);
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDie += Emeny_OnDie;
        healthSystem.OnDamaged += Emeny_OnDamage;
    }

    private void Emeny_OnDamage(object sender, System.EventArgs e)
    {
       Soundmanager.Instance.PlaySound(Soundmanager.Sound.EnemyHit);
    }

    private void Emeny_OnDie(object sender, System.EventArgs e)
    {
        Soundmanager.Instance.PlaySound(Soundmanager.Sound.EnemyDie);
        Destroy(gameObject);
    }

    private void Update()
    {
        HandleMovement();
        HandleTargeting();

    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Building building = other.gameObject.GetComponent<Building>();
        if (building != null)
        {
            //coilled with a building 
            HealthSystem healthSystem = building.GetComponent<HealthSystem>();
            healthSystem.Damage(10);
            

            Destroy(gameObject);
        }
    }
    private void HandleMovement()
    {
        if (targetTransfrom != null)
        {
            Vector3 movDir = (targetTransfrom.position - transform.position).normalized;
            float movSpeed = 6f;
            rigidbody2D.velocity = movDir * movSpeed;
        }
        else
        {
            rigidbody2D.velocity = Vector3.zero;
        }

    }
    private void HandleTargeting()
    {
        lookForTimer -= Time.deltaTime;
        if (lookForTimer <= 0f)
        {
            lookForTimer += maxlookforTimer;
            LookForIt();
        }


    }
    private void LookForIt()
    {
        float targetMaxRadius = 10f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position, targetMaxRadius);

        foreach (Collider2D collider2D in collider2DArray)
        {
            Building building = collider2D.GetComponent<Building>();
            if (building != null)
            {
                // is it a building 

                if (targetTransfrom == null)
                {
                    targetTransfrom = building.transform;
                }

                else
                {
                    if (Vector3.Distance(transform.position, building.transform.position) < Vector3.Distance(transform.position, targetTransfrom.position))
                    {
                        // closer Transform 
                        targetTransfrom = building.transform;
                    }
                }

            }
        }
        if (targetTransfrom == null)
        {
            if (BuildingManager.Instance.GetHqBuilding() != null)
        {
            targetTransfrom = BuildingManager.Instance.GetHqBuilding().transform;
        }        }

    }
}
