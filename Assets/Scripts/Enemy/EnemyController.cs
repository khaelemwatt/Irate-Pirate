using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   
    EnemyModel enemyModel;
    PlayerModel playerModel;
    PlayerController playerController;
    ItemList itemList;

    public void Start()
    {                
        enemyModel = gameObject.GetComponent<EnemyModel>();
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        itemList = GameObject.FindWithTag("ItemList").GetComponent<ItemList>();

        ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();

        ref GameObject player = ref enemyModel.Player();        

        distanceToPlayer = 10f;

        player = GameObject.FindWithTag("Player");
        ref bool isAttacking = ref enemyModel.IsAttacking();
        isAttacking = true;
        StartCoroutine(AttackCooldown());
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
                DropItem();
                Die();
            }
        }        
    }

    public void DropItem(){
        ref List<GameObject> allItems = ref itemList.AllItems();
        
        System.Random rand = new System.Random();
        Debug.Log("Spawn Chance");
        if(rand.Next(0, 9) <= 1){
            int randomNumber = rand.Next(0, allItems.Count);
            Instantiate(allItems[randomNumber], gameObject.transform.position, Quaternion.identity);            
        }
    }

    public void Die(){
        ref string enemyName = ref enemyModel.EnemyName();
        switch(enemyName){
            case "Skull":
                gameObject.GetComponent<SkullController>().Die();
                break;
            case "Goldbeard":
                gameObject.GetComponent<GoldbeardController>().Die();
                break;
            case "Parrot":
                gameObject.GetComponent<ParrotController>().Die();
                break;
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
        ref string enemyName = ref enemyModel.EnemyName();
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
            // if(isAttacking == false)
            // {
            //     StartCoroutine(attack());
            // }    
            playerPos = player.transform.position;        
            dirToPlayer = playerPos - rb.position;
            if(enemyName == "Parrot" && isAttacking == false){
                Debug.Log("Parrot Attack");
                ParrotController parrotController = gameObject.GetComponent<ParrotController>();
                parrotController.Attack(playerController, dirToPlayer, gameObject.transform.position);
                isAttacking = true;
                StartCoroutine(AttackCooldown());
            }
            moveTo(dirToPlayer);
        }else{
            if(isWandering==false){
                StartCoroutine(wander()); 
            }                       
        }
    }

    IEnumerator AttackCooldown(){
        yield return new WaitForSeconds(1.5f);
        enemyModel.isAttacking = false;
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
            enemyModel.animator.SetFloat("speed", 10f);   
            moveTo(enemyModel.wanderDir);
            yield return new WaitForSeconds(0.01f);
            enemyModel.wanderCounter -= Time.deltaTime;
        }
        yield return new WaitForSeconds(1f);
        enemyModel.animator.SetFloat("speed", 10f);
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
        ref float distanceToPlayer = ref enemyModel.DistanceToPlayer();
        if(other.CompareTag("MainCamera")){
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

    public void OnCollisionEnter2D(Collision2D other){        
        ref bool isTouchingPlayer = ref enemyModel.IsTouchingPlayer();
        ref bool isInvulnerable = ref playerModel.IsInvulnerable();
        ref bool isAttacking = ref enemyModel.IsAttacking();
        if(other.collider.gameObject.CompareTag("PlayerHitbox")){
            Debug.Log("Attack");
            isAttacking = true;
            if(isInvulnerable == false){
                isInvulnerable = true;
                Attack();
                StartCoroutine(CheckForCollisionExit());
            }
            isTouchingPlayer = true;
        }
    }

    IEnumerator CheckForCollisionExit(){
        yield return new WaitForSeconds(2);
        playerModel.isInvulnerable = false; 
        while(enemyModel.isTouchingPlayer){            
            Attack();
            yield return new WaitForSeconds(2);
        }               
    }

    void OnCollisionExit2D(Collision2D other){
        ref bool isTouchingPlayer = ref enemyModel.IsTouchingPlayer();
        ref bool isAttacking = ref enemyModel.IsAttacking();
        if(other.collider.gameObject.CompareTag("PlayerHitbox")){
            isTouchingPlayer = false;
            isAttacking = false;
        }
        
    }

    void Attack(){
        ref string enemyName = ref enemyModel.EnemyName();
        ref GameObject player = ref enemyModel.Player();
        switch(enemyModel.enemyName){
            case "Skull":
                SkullController skullController = gameObject.GetComponent<SkullController>();
                skullController.Attack(playerController);
                break;
            case "Goldbeard":
                Debug.Log("Goldbeard Attack");
                GoldbeardController goldbeardController = gameObject.GetComponent<GoldbeardController>();
                goldbeardController.Attack(playerController);
                break;
        }
    }
}
