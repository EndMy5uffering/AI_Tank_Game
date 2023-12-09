using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControler : MonoBehaviour
{

    public float speed = 100f;
    public float rotSpeed = 5f;
    public float accell = 1.0f;
    public Camera cam;

    public GameObject bullet;

    private bool hasHit = false;
    private Vector3 hitPoint = Vector3.zero;

    private Vector3 direction = Vector3.forward;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)) 
        {
            this.transform.Translate(speed * direction * Time.deltaTime, Space.World);    
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(-speed * direction * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //this.transform.Rotate(this.transform.up, -rotSpeed);
            this.direction = (Quaternion.Euler(0, -rotSpeed * Time.deltaTime, 0) * direction).normalized;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //this.transform.Rotate(this.transform.up, rotSpeed);
            this.direction = (Quaternion.Euler(0, rotSpeed * Time.deltaTime, 0) * direction).normalized;

        }

        if (Input.GetMouseButtonDown(0)) 
        {
            FieringCheck();
        }

        this.transform.LookAt(this.transform.position + this.direction);
    }

    void FieringCheck() 
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            hasHit = true;
            hitPoint = hit.point;

            GameObject nbullet = GameObject.Instantiate(bullet);

            Vector3 detPos = hitPoint;
            detPos.y = this.transform.position.y;
            Vector3 dir = detPos - this.transform.position;
            dir = dir.normalized;

            Quaternion rot = Quaternion.FromToRotation(this.transform.position, dir);

            nbullet.GetComponent<Bullet>().direction = dir;
            nbullet.transform.SetPositionAndRotation(this.transform.position + dir, rot);
            nbullet.transform.localScale = new Vector3(5, 5, 5);
            return;
        }
    }

    public void OnDrawGizmos()
    {
        if (hasHit) 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.hitPoint, 5);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, this.transform.position + this.direction * 5);
    }
}
