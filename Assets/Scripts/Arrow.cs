using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   
    public static Arrow Create(Vector3 position ,Emeny emeny)
    {
        Transform pfArrow = Resources.Load<Arrow>("pfarrow").transform;
        Transform arrowTransform = Instantiate(pfArrow, position, Quaternion.identity);

        Arrow arrow = arrowTransform.GetComponent<Arrow>();
        arrow.SetTargetEmeny(emeny);
        return arrow;

    }
     private Emeny targetEmeny;
     private Vector3 lastMove;
     private float timeToDie=2f;
    private void Update() {
        Vector3 movdir;
        if(targetEmeny!= null)
        {
             movdir =   (targetEmeny.transform.position - transform.position).normalized;
             lastMove = movdir;
        }
        else{
            movdir=lastMove;
        }
        
        

        float moveSpeed =20f;
        transform.position += movdir* Time.deltaTime*moveSpeed;
        transform.eulerAngles = new Vector3(0,0,UtilsClass.GetRotationAngle(movdir));
        timeToDie -= Time.deltaTime;
        if(timeToDie <= 0f)
        {
            Destroy(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D other) {
        Emeny emeny = other.GetComponent<Emeny>();

        if(emeny != null)
        {
            // Hit an emeny
            int damageAmount =10 ;
            emeny.GetComponent<HealthSystem>().Damage(damageAmount);
            Destroy(gameObject); 
        }
    }

    private void SetTargetEmeny(Emeny targetEmeney)
    {
        this.targetEmeny = targetEmeney;
    }
}
