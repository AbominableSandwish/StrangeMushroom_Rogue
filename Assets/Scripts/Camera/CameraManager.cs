using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private const int DISTANCE_MAX = 1;
    private const float DELTA_SPEED = 1.2f;
    private const int DELTA_CAMERA = 6;

    Rigidbody2D _rigid;
    Transform target;
    Vector2 delta_pos = Vector2.zero;

    enum direction
    {
        RIGHT,
        LEFT,
        UP,
        DOWN,
        NULL
    }

    direction player_x;
    direction player_y;

    private void Start()
    {
        _rigid = gameObject.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void FixedUpdate () {

      

        if (target != null)
        {
            Vector2 pos_camera = new Vector2(transform.position.x, transform.position.y);
            


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                player_x = direction.LEFT;
                pos_camera += Vector2.right * DISTANCE_MAX*2;
            }

            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                player_x = direction.RIGHT;
                pos_camera -= Vector2.right * DISTANCE_MAX*2;
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                player_x = direction.DOWN;
                pos_camera += Vector2.up * DISTANCE_MAX*2;
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                player_x = direction.UP;
                pos_camera -= Vector2.up * DISTANCE_MAX*2;
            }

            Vector2 pos_player = new Vector2(target.position.x, target.position.y);
            delta_pos = (pos_player - pos_camera);
            float distance = delta_pos.sqrMagnitude;


            if (-DISTANCE_MAX <= distance && distance >= DISTANCE_MAX)
            {
                if (delta_pos.x >= DISTANCE_MAX || delta_pos.x <= -DISTANCE_MAX)
                {
                    _rigid.AddForce(Vector2.right * Time.deltaTime * delta_pos.normalized.x * DELTA_SPEED);
                }

                if (delta_pos.y >= DISTANCE_MAX || delta_pos.y <= -DISTANCE_MAX)
                {
                    _rigid.AddForce(Vector2.up * Time.deltaTime * delta_pos.normalized.y * DELTA_SPEED);
                }
            }

        }
        else
        {
            target = GameObject.Find("player(Clone)").transform;
            transform.position = target.transform.position - Vector3.forward * DELTA_CAMERA;
        }
	}

    private void OnDrawGizmos()
    {
        if (target != null)
        {

            // Gizmos.DrawCube(delta_pos + new Vector2(transform.position.x, transform.position.y), Vector3.one);
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(transform.position + new Vector3(0, -1, 5), transform.position + new Vector3(1, -1, 5));
            Gizmos.DrawLine(transform.position+ new Vector3(0, 1, 5), transform.position + new Vector3(1, 1, 5));

            Gizmos.DrawLine(transform.position + new Vector3(0, -1, 5), transform.position + new Vector3(-1, -1, 5));
            Gizmos.DrawLine(transform.position + new Vector3(0, 1, 5), transform.position + new Vector3(-1, 1, 5));

            Gizmos.DrawLine(transform.position + new Vector3(-1, -1, 5), transform.position + new Vector3(-1, 0, 5));
            Gizmos.DrawLine(transform.position + new Vector3(-1, 1, 5), transform.position + new Vector3(-1, 0, 5));

            Gizmos.DrawLine(transform.position + new Vector3(1, -1, 5), transform.position + new Vector3(1, 0, 5));
            Gizmos.DrawLine(transform.position + new Vector3(1, 1, 5), transform.position + new Vector3(1, 0, 5));

            Gizmos.DrawLine(transform.position + new Vector3(0, 1, 5), transform.position + new Vector3(0, 0, 5));
            Gizmos.DrawLine(transform.position + new Vector3(0, -1, 5), transform.position + new Vector3(0, 0, 5));
           
        }
    }
}
