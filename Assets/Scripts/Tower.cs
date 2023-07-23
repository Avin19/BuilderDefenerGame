using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] private float shootTimerMax;
    private float shootTimer;
    private Emeny targetEmeny;
    private float timer;
    private float maxTimer;

    private Vector3 shootingposition;
    private void Awake() {
        shootingposition = transform.Find("ShootingPoint").position;
    }

private void Update() {
    HandleTragetting();
    Shooting();
}

    private void HandleTragetting ()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer= maxTimer;
            LookAtTarget();;
        }
    }
    private void Shooting()

    { shootTimer -= Time.deltaTime;

        if(shootTimer<= 0f)
         {  shootTimer+= shootTimerMax;
            if(targetEmeny!= null){
        Arrow.Create(shootingposition,targetEmeny);
    }}
    }
    private void LookAtTarget()
    {
        float maxArea =20f;
        Collider2D[] collider2DArray = Physics2D.OverlapCircleAll(transform.position,maxArea);
        foreach(Collider2D collider2d in collider2DArray)
        {
            Emeny emeny = collider2d.GetComponent<Emeny>();

            if(emeny != null)
            {
                // There is an emeny 
                if(targetEmeny == null)
                {
                    targetEmeny= emeny;
                }
                else{
                    if(Vector3.Distance(transform.position,targetEmeny.transform.position)< Vector3.Distance(transform.position,emeny.transform.position))
                    {
                        targetEmeny= emeny;
                    }
                }
            }
        }
    }
}
