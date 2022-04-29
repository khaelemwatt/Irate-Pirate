using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//#------------------------------# CREDIT #------------------------------#

//Player movement from this video
//https://www.youtube.com/watch?v=whzomFgjT50&list=PL8z36fSARS2MmrHH67XoYnI6OJdB8oPar&index=5

//Adapted Roll skill from this video
//Video shows roll with keyboard inputs and I adapted it to work with mouse position
//https://www.youtube.com/watch?v=tH57EInEb58




public class PlayerMovement : MonoBehaviour
{
   //#--------------------# VARIABLES #--------------------#
   //#----------# Float #----------#
   public float movementSpeed = 1f;
   public float rollSpeed = 2f;
   public float rollLength = 0.5f;
   public float rollCooldown = 1f;
   private float rollCounter;
   private float rollCoolCounter;

   //#----------# Bool #----------#
   public bool isRolling;   

   //#----------# Unity Objects #----------#
   public Rigidbody2D rb;
   public Camera cam;

   //#----------# Vector2 #----------#
   Vector2 mouseDir;
   Vector2 movement;
   Vector2 direction;

   //#--------------------# UPDATE #--------------------#
   void Update()
   {  
      movement.x = Input.GetAxisRaw("Horizontal");
      movement.y = Input.GetAxisRaw("Vertical");

      movement.Normalize();

      if(Input.GetButtonDown("Fire2"))
      {
         if(rollCoolCounter <=0 && rollCounter <=0){
            isRolling = true;
            rollCounter = rollLength;
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseDir = mousePos - rb.position;
            mouseDir.Normalize();
         }        
      }      
   }

   //#--------------------# FIXED UPDATE #--------------------#
   void FixedUpdate()
   {
      //Move Player
      rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

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
}
