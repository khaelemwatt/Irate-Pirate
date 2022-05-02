using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    //#--------------------# VARIABLES #--------------------#
    PlayerModel playerModel;
    InventoryController inventoryController;
    InventoryModel inventoryModel;
    PauseController pauseController;
    PickupModel pickupModel;
    HealthbarController healthbarController;

    //#--------------------# START #--------------------#
    void Start()
    {
        playerModel = gameObject.GetComponent<PlayerModel>();
        inventoryController = gameObject.GetComponent<InventoryController>();
        inventoryModel = gameObject.GetComponent<InventoryModel>();        
        pauseController = GameObject.FindWithTag("UI").GetComponent<PauseController>();
        healthbarController = GameObject.FindWithTag("Healthbar").GetComponent<HealthbarController>();
     
        ref Animator playerAnimator = ref playerModel.PlayerAnimator();
        ref float health = ref playerModel.Health();
        ref float maxHealth = ref playerModel.MaxHealth();               

        playerAnimator = gameObject.GetComponent<Animator>();

        health = maxHealth;

        healthbarController.SetMaxHealth((int)maxHealth);
        healthbarController.SetHealth((int)health); 
    }

    //#--------------------# UPDATE #--------------------#
    void Update()
    {  
        ref Vector2 movement = ref playerModel.Movement();
        ref float rollCoolCounter = ref playerModel.RollCoolCounter();
        ref float rollCounter = ref playerModel.RollCounter();
        ref float rollLength = ref playerModel.RollLength();
        ref float reloadTime = ref playerModel.ReloadTime();
        ref bool isRolling = ref playerModel.IsRolling();
        ref bool isReloading = ref playerModel.IsReloading();
        ref Vector2 mousePos = ref playerModel.MousePos();
        ref Vector2 mouseDir = ref playerModel.MouseDir();
        ref Rigidbody2D rb = ref playerModel.Rb();
        ref Camera cam = ref playerModel.Cam();
        ref GameObject currentWeapon = ref inventoryModel.CurrentWeapon();

        //#----------# Movement #----------#
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        if(Input.GetButtonDown("Fire2"))
        {
            if(rollCoolCounter <=0 && rollCounter <=0){
                isRolling = true;
                rollCounter = rollLength;
                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                mouseDir = mousePos - rb.position;
                mouseDir.Normalize();
            }        
        }  

        //#----------# Shoot #----------#   
         if(Input.GetButtonDown("Fire1"))
        {           
            if(isReloading == false && Time.timeScale != 0f){
                reloadTime = currentWeapon.GetComponent<GunController>().ShootGun();
                StartCoroutine(Reload(reloadTime));
            }            
        }

        if(Input.GetKeyDown("1"))
            inventoryController.switchToWeapon(0);

        if(Input.GetKeyDown("2"))
            inventoryController.switchToWeapon(1);
            
        if(Input.GetKeyDown("3"))
            inventoryController.switchToWeapon(2);
    }

    //#--------------------# FIXED UPDATE #--------------------#
    void FixedUpdate()
    {
        ref Rigidbody2D rb = ref playerModel.Rb();
        ref Vector2 mouseDir = ref playerModel.MouseDir();
        ref Vector2 movement = ref playerModel.Movement();
        ref float movementSpeed = ref playerModel.MovementSpeed();
        ref float rollCounter = ref playerModel.RollCounter();
        ref float rollSpeed = ref playerModel.RollSpeed();
        ref float rollCooldown = ref playerModel.RollCooldown();
        ref bool isRolling = ref playerModel.IsRolling();
        ref float rollCoolCounter = ref playerModel.RollCoolCounter();
        ref Animator playerAnimator = ref playerModel.PlayerAnimator();

        //#----------# Movement #----------#
        //Move Player
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        if(movement.Equals(Vector3.zero)){
            playerAnimator.SetFloat("Speed", 0);
        }else{
            playerAnimator.SetFloat("Speed", 1);
        }

        //Roll skill
        if(rollCounter > 0)
        {
            rollCounter -= Time.deltaTime;
            rb.MovePosition(rb.position + mouseDir * rollSpeed * Time.fixedDeltaTime);

            if(rollCounter <= 0)
            {
                
            isRolling = false;
            rollCoolCounter = rollCooldown;
            }
        }

        if(rollCoolCounter > 0)
        {
            rollCoolCounter -= Time.deltaTime;
        }  
    }

    IEnumerator Reload(float reloadTime){
        playerModel.isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        playerModel.isReloading = false;
    }

    
    public void CollideWithDock(GameObject dock){
        Debug.Log("Collision with Dock");

        ref string currentRoom = ref playerModel.CurrentRoom();

        if(dock.CompareTag("Dock")){
            int row = int.Parse(currentRoom[0].ToString());
            int col = int.Parse(currentRoom[1].ToString());
            DockModel dockModel = dock.GetComponent<DockModel>();
            ref Vector3Int dockDir = ref dockModel.Direction();
            if(dockDir.x == 3){
                col += 1;
            }else if(dockDir.x == -3){
                col -= 1;
            }else if(dockDir.y == 3){
                row += 1;
            }else if(dockDir.y == -3){
                row -= 1;
            }
            currentRoom = row.ToString() + col.ToString();
        }
    }

    public void Damage(float damage){        
        ref float health = ref playerModel.Health();
        Debug.Log("Hit");
        health -= damage;
        if(health<=0){
            pauseController.DeathScreen();
        }
        healthbarController.SetHealth((int)health);
    }

    public void RestoreHealth(float restore){
        ref float health = ref playerModel.Health();
        ref float maxHealth = ref playerModel.MaxHealth();

        if((restore + health) >= maxHealth){
            health = maxHealth;
        }else{
            health += restore;
        }

        healthbarController.SetHealth((int)health);
    }

    //#--------------------# ONCOLLISIONENTER2D #--------------------#
    public void OnCollisionEnter2D(Collision2D collision)
    {
        // ref bool isInvulnerable = ref playerModel.IsInvulnerable();
        // ref bool isTouchingEnemy = ref playerModel.IsTouchingEnemy();

        // if(enemy.CompareTag("Enemy"))
        // {
        //     if(isInvulnerable == false){
        //         isInvulnerable = true;
        //         Damage(5f);
        //         StartCoroutine(CheckForCollisionExit());
        //     }
        //     isTouchingEnemy = true;
        // }
        
        if(collision.collider.gameObject.CompareTag("Gun")){
            GameObject gun = collision.collider.gameObject;
            PickupModel pickupModel = gun.GetComponent<PickupModel>();
            inventoryController.addWeapon(pickupModel.GunName());
            Destroy(collision.gameObject.transform.parent.gameObject); 
        }
    }

    // IEnumerator CheckForCollisionExit(){
    //     yield return new WaitForSeconds(2);
    //     while(playerModel.isTouchingEnemy){            
    //         Damage(5f);
    //         yield return new WaitForSeconds(2);
    //     }   
    //     playerModel.isInvulnerable = false;     
    // }

    // void OnCollisionExit2D(Collision2D other){
    //     ref bool isTouchingEnemy = ref playerModel.IsTouchingEnemy();
    //     Debug.Log("Collision Exit");
    //     isTouchingEnemy = false;
    // }
}
