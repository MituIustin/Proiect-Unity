using System.Diagnostics;
using UnityEngine;

public class PlayerController_ForNow : MonoBehaviour
{
    public float speed;

    public float groundDist;

    public LayerMask terrainLayer;

    public Rigidbody rb;

    public SpriteRenderer sr;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {

     RaycastHit hit;

     Vector3 castPos = transform.position;
     castPos.y += 1;

     if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer)){
        if (hit.collider != null){
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;
            transform.position = movePos;
        }
     }

     float x = Input.GetAxis("Horizontal");
     float y = Input.GetAxis("Vertical");
     Vector3 moveDir = new Vector3(x, 0, y);
     rb.linearVelocity=moveDir*speed;

     if (x!= 0 && x<0){
         sr.flipX = true;
     } else if (x!=0 && x>0){
         sr.flipX = false;
     }

    }
}
