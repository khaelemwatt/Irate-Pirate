using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//GetIntersectionCoordinates() method directly taken from https://blog.dakwamine.fr/?p=1943

public class CameraController : MonoBehaviour
{
    CameraModel cameraModel;

    void Start(){
        cameraModel = gameObject.GetComponent<CameraModel>();

        ref Vector3 center = ref cameraModel.Center();
        center = new Vector3(Screen.width/2, Screen.height/2, 0);
    }

    void Update()
    {
        ref Vector3 mousePos = ref cameraModel.MousePos();
        ref Vector3 mouseDir = ref cameraModel.MouseDir();
        ref Vector2 intersect = ref cameraModel.Intersect();
        ref Vector3 center = ref cameraModel.Center();
        ref float percentAlong = ref cameraModel.PercentAlong();

        mousePos = Input.mousePosition;
        mouseDir = mousePos - center;

        Vector2 bl = new Vector2(0, 0);
        Vector2 br = new Vector2(Screen.width, 0);
        Vector2 tl = new Vector2(0, Screen.height);
        Vector2 tr = new Vector2(Screen.width, Screen.height);

        if(mouseDir.y > 0)
        {
            //Mouse is above player so Quads 1 and 2
            if(mouseDir.x > 0){
                //Mouse is to the right                
                intersect = GetIntersectionPointCoordinates(tl, tr, center, mousePos);
                if(intersect.x > Screen.width)
                {
                    intersect = GetIntersectionPointCoordinates(br, tr, center, mousePos);
                }
            }else{
                //Mouse is to the left
                intersect = GetIntersectionPointCoordinates(tl, tr, center, mousePos);
                if(intersect.x < 0)
                {
                    intersect = GetIntersectionPointCoordinates(tl, bl, center, mousePos);
                }
            }
        }else{
            if(mouseDir.x > 0){
                //Mouse is to the right                
                intersect = GetIntersectionPointCoordinates(bl, br, center, mousePos);
                if(intersect.x > Screen.width)
                {
                    intersect = GetIntersectionPointCoordinates(br, tr, center, mousePos);
                }
            }else{
                //Mouse is to the left
                intersect = GetIntersectionPointCoordinates(bl, br, center, mousePos);
                if(intersect.x < 0)
                {
                    intersect = GetIntersectionPointCoordinates(tl, bl, center, mousePos);
                }
            }
        }

        float distToMouse = Vector3.Distance(mousePos, center);
        float distToEdge = Vector3. Distance(intersect, mousePos);
        if(distToEdge + distToMouse > Screen.height/2)
        {
            distToEdge = Screen.height/2;
        }
        
        percentAlong = distToMouse/(distToMouse + distToEdge);

    }

    public Vector2 GetIntersectionPointCoordinates(Vector2 A1, Vector2 A2, Vector2 B1, Vector2 B2)
    {
        float tmp = (B2.x - B1.x) * (A2.y - A1.y) - (B2.y - B1.y) * (A2.x - A1.x);
    
        if (tmp == 0)
        {
            // No solution!
            return Vector2.zero;
        }
    
        float mu = ((A1.x - B1.x) * (A2.y - A1.y) - (A1.y - B1.y) * (A2.x - A1.x)) / tmp;
    
        return new Vector2(
            B1.x + (B2.x - B1.x) * mu,
            B1.y + (B2.y - B1.y) * mu
        );
    }

    void FixedUpdate()
    {
        ref Camera cam = ref cameraModel.Cam();
        ref GameObject player = ref cameraModel.Player();
        ref Vector3 moveDir = ref cameraModel.MoveDir();
        ref Vector3 mousePos = ref cameraModel.MousePos();
        ref float maxMove = ref cameraModel.MaxMove();
        ref float moveSpeed = ref cameraModel.MoveSpeed();        
        ref float percentAlong = ref cameraModel.PercentAlong();

        moveDir = mousePos - cam.WorldToScreenPoint(player.transform.position);
        moveDir.Normalize();
        moveDir = new Vector3(moveDir.x * maxMove * percentAlong, moveDir.y * maxMove * percentAlong, 0);
        transform.position = Vector3.Lerp(transform.position, player.transform.position + moveDir, moveSpeed);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
