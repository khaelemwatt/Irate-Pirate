using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    //#--------------------# VARIABLES #--------------------#
    PlayerModel playerModel;

    //#--------------------# START #--------------------#
    void Start()
    {
        playerModel = gameObject.GetComponent<PlayerModel>();

        ref int currentInvSlot = ref playerModel.CurrentInvSlot();
        ref Dictionary<string, GameObject> allWeapons = ref playerModel.AllWeapons();
        ref List<GameObject> weapons = ref playerModel.Weapons();
        ref GameObject musket = ref playerModel.Musket();
        ref GameObject blunderbuss = ref playerModel.Blunderbuss();        
        ref Animator playerAnimator = ref playerModel.PlayerAnimator();

        playerAnimator = gameObject.GetComponent<Animator>();

        //#----------# Shoot #----------#  
        currentInvSlot = 0;
        weapons.RemoveAt(0);
        weapons.Add(blunderbuss);
        
        createWeapon(currentInvSlot);

        //ID List for all weapons. Used to spawn weapons in
        allWeapons = new Dictionary<string, GameObject>()
        {
            {"blunderbuss", blunderbuss},
            {"musket", musket}
        };
    }

    //#--------------------# UPDATE #--------------------#
    void Update()
    {  
        ref Vector2 movement = ref playerModel.Movement();
        ref float rollCoolCounter = ref playerModel.RollCoolCounter();
        ref float rollCounter = ref playerModel.RollCounter();
        ref float rollLength = ref playerModel.RollLength();
        ref bool isRolling = ref playerModel.IsRolling();
        ref Vector2 mousePos = ref playerModel.MousePos();
        ref Vector2 mouseDir = ref playerModel.MouseDir();
        ref Rigidbody2D rb = ref playerModel.Rb();
        ref Camera cam = ref playerModel.Cam();
        ref GameObject currentWeapon = ref playerModel.CurrentWeapon();

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
            currentWeapon.GetComponent<ShootGun>().Shoot();
        }

        if(Input.GetKeyDown("1"))
            switchToWeapon(0);

        if(Input.GetKeyDown("2"))
            switchToWeapon(1);
            
        if(Input.GetKeyDown("3"))
            switchToWeapon(2);
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

    //#--------------------# SWITCHTOWEAPON #--------------------#
    void switchToWeapon(int invSlot)
    {   
        ref int currentInvSlot = ref playerModel.CurrentInvSlot();
        ref List<GameObject> weapons = ref playerModel.Weapons();
        ref GameObject currentWeapon = ref playerModel.CurrentWeapon();
        if(currentInvSlot != invSlot && weapons.Count >= invSlot + 1)
        {
            if(weapons[invSlot] != null)
            {
                Destroy(currentWeapon);
                createWeapon(invSlot);                
                currentInvSlot = invSlot;
            }            
        }        
    }

    //#--------------------# CREATEWEAPON #--------------------#
    public void createWeapon(int invSlot){
        ref List<GameObject> weapons = ref playerModel.Weapons();
        ref GameObject currentWeapon = ref playerModel.CurrentWeapon();
        ref GameObject weaponHolder = ref playerModel.WeaponHolder();

        currentWeapon = Instantiate(weapons[invSlot], weaponHolder.transform.position, Quaternion.identity);
        currentWeapon.transform.parent = weaponHolder.transform;
    }

    //#--------------------# ADDWEAPON #--------------------#
    public void addWeapon(string gunName, Sprite gunSprite)
    {
        ref List<GameObject> weapons = ref playerModel.Weapons();
        ref Dictionary<string, GameObject> allWeapons = ref playerModel.AllWeapons();

        int place = weapons.Count;
        weapons.Add(allWeapons[gunName]);
        // invSlots[place].GetComponent<SpriteRenderer>().sprite = gunSprite;

        // Vector3 invPosition = invSlots[place].transform.position;
        // Vector3 invScale = invSlots[place].transform.localScale;
        // invPosition = new Vector3(invPosition.x, invPosition.y - 0.056f, invPosition.z);
        // invScale = new Vector3(2.57f, 2.57f, invScale.z);
    }

    //#--------------------# ONTRIGGERENTER2D #--------------------#
    public void OnTriggerEnter2D(Collider2D other)
    {        
        if(other.CompareTag("Gun")){
            GunCollision gun = other.gameObject.GetComponent<GunCollision>();
            string gunName = gun.getGun();
            Sprite gunSprite = gun.getSprite();
            addWeapon(gunName, gunSprite);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        
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

    //#--------------------# SHOOT #--------------------#
    public void Shoot()
    {
        ref GameObject bullet = ref playerModel.Bullet();
        ref Transform firePoint = ref playerModel.FirePoint();
        ref float bulletForce = ref playerModel.BulletForce();

        GameObject newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation);
        
        Rigidbody2D rb = newBullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        Destroy(newBullet, 2f);
    }

    //#--------------------# ONCOLLISIONENTER2D #--------------------#
    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject enemy = collision.collider.gameObject;
        //Debug.Log("10");
        if(enemy.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
        }
    }
}
