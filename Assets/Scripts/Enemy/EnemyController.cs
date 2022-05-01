using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    EnemyModel enemyModel;

    public void Start()
    {                
        enemyModel = gameObject.GetComponent<EnemyModel>();
        ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();

        ref GameObject player = ref enemyModel.Player();

        distanceToPlayer = 10f;

        player = GameObject.FindWithTag("Player");
    }
    
    public void Damage(int damage)
    {
        ref Animator animator = ref enemyModel.Animator();
        ref bool isInvulnerable = ref enemyModel.isInvulnerable;
        ref int health = ref enemyModel.Health();

        if(isInvulnerable==false){
            animator.SetBool("hit", true);
            StartCoroutine(TurnOffHitAnim());
            health -= damage;
            if(health <= 0)
            {
                Destroy(gameObject);
            }
        }        
    }

    IEnumerator TurnOffHitAnim(){
        enemyModel.isInvulnerable = true;
        yield return new WaitForSeconds(0.2f);  
        enemyModel.animator.SetBool("hit", false);      
        yield return new WaitForSeconds(1);     
        enemyModel.isInvulnerable = false;    
    }

    void FixedUpdate()
    {
        ref GameObject player = ref enemyModel.Player();
        ref Rigidbody2D rb = ref enemyModel.Rb();
        ref Vector2 playerPos = ref enemyModel.PlayerPos();
        ref Vector2 enemyPos = ref enemyModel.EnemyPos();        
        ref Vector2 dirToPlayer = ref enemyModel.DirToPlayer();
        ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();
        ref float lineOfSight = ref enemyModel.LineOfSight();
        ref bool isAttacking = ref enemyModel.IsAttacking();
        ref bool isWandering = ref enemyModel.IsWandering();

        // playerPos = player.transform.position;
        // enemyPos = gameObject.transform.position;
        // distanceToPlayer = Vector3.Distance(enemyPos, playerPos);
        //distanceToPlayer = (enemyPos - playerPos).magnitude;
        if(distanceToPlayer <= lineOfSight)
        {
            if(isAttacking == false)
            {
                StartCoroutine(attack());
            }    
            playerPos = player.transform.position;        
            dirToPlayer = playerPos - rb.position;
            moveTo(dirToPlayer);
        }else{
            if(isWandering==false){
                StartCoroutine(wander()); 
            }                       
        }
    }

    IEnumerator wander()
    {
        enemyModel.isWandering = true;
        enemyModel.wanderCounter = (float)enemyModel.rand.Next(2);
        enemyModel.wanderTarget = new Vector2(enemyModel.rb.position.x + randomFloat(), enemyModel.rb.position.y + randomFloat());
        enemyModel.wanderDir = enemyModel.wanderTarget - enemyModel.rb.position;
        //As long as player isnt in our line of sight we continue to wander
        while(enemyModel.wanderCounter > 0)
        {            
            moveTo(enemyModel.wanderDir);
            yield return new WaitForSeconds(0.01f);
            enemyModel.wanderCounter -= Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        enemyModel.isWandering= false;
    }

    float randomFloat()
    {
        ref System.Random rand = ref enemyModel.Rand();
        float value = (float)rand.NextDouble();
        if(value >= 0.5){
            value = value/2f;
        }
        float newValue =  rand.NextDouble() <= 0.5 ? value*(-1) : value;
        return newValue;
    }

    void moveTo(Vector2 dir)
    {
        // ref bool isTouchingBorder = ref enemyModel.IsTouchingBorder();
        // if(isTouchingBorder == false){
        //     ref Rigidbody2D rb = ref enemyModel.Rb();
        //     ref float movementSpeed = ref enemyModel.MovementSpeed();
        //     rb.MovePosition(rb.position + dir.normalized * movementSpeed * Time.fixedDeltaTime);
        // }else{
        //     ref Rigidbody2D rb = ref enemyModel.Rb();
        //     ref float movementSpeed = ref enemyModel.MovementSpeed();
        //     rb.MovePosition(rb.position + dir.normalized * -1 * movementSpeed * Time.fixedDeltaTime);
        // }     
        ref Rigidbody2D rb = ref enemyModel.Rb();
        ref float movementSpeed = ref enemyModel.MovementSpeed();
        rb.MovePosition(rb.position + dir.normalized * movementSpeed * Time.fixedDeltaTime); 
    }

    // public void OnCollisionEnter2D(Collision2D other){
    //     if(other.collider.gameObject.CompareTag("Border")){
    //         ref bool isTouchingBorder = ref enemyModel.IsTouchingBorder();
    //         isTouchingBorder = true;
    //     }        
    // }

    // public void OnCollisionExit2D(Collision2D other){
    //     if(other.collider.gameObject.CompareTag("Border")){
    //         ref bool isTouchingBorder = ref enemyModel.IsTouchingBorder();
    //         isTouchingBorder = false;
    //     } 
    // }

    public void OnTriggerEnter2D(Collider2D other)
    {        
        Debug.Log("trigger enter");
        ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();
        if(other.CompareTag("MainCamera")){
            Debug.Log("camera trigger enter");
            distanceToPlayer = 0f;
        }        
    }

    public void OnTriggerExit2D(Collider2D other)
    {        
        // ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();
        // if(other.CompareTag("MainCamera")){
        //     distanceToPlayer = 10f;
        // }        
    }

    public virtual IEnumerator attack()
    {
        Debug.Log("Attack");
        enemyModel.isAttacking = true;
        yield return new WaitForSeconds(1f);
        enemyModel.isAttacking = false;
    }
}
