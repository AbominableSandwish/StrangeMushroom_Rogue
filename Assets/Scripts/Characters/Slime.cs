using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FollowerMovement
{
    Lerp,
    SeekAndArrive,
    PursueLerp,
    PursueSeekAndArrive
}

public class Slime : MonoBehaviour {

    // Use this for initialization
    private const int  FIELD_Of_VIEW = 3;
    private const int  PRODUCT_CAPESPEED = 5;
    private const float DISTANCE_MIN_NODE = 0.25f;
    private const float TIME_FOR_MOVE = 1;
    private const float TIME_FOR_ATTACK = 0.25f;

    [SerializeField] private FollowerMovement followerMovement = FollowerMovement.Lerp;
    [SerializeField] private float speed = 4.0f;
    [SerializeField] private float capedSpeed = 4.0f;

    private Rigidbody2D m_Body;
    private GameObject target;
    private Vector3[] positions_Patroller;

    private float speed_boost =0;

    float counter_time;
    float counter_time_attack;
    int number_patroller = 0;
    int move_counter=0;

    List<Node> Path;
    Vector3 dir;
    BFS bfs;



    private enum state
    {
        PATROLLER,
        FOLLOWER,
        JUMP,
        MOVE
    }
    state state_enemy = state.PATROLLER;

    void Start () {
        speed += GameObject.Find("Scoring").GetComponent<ScoringManger>().GetSpeedBoost();
        capedSpeed += GameObject.Find("Scoring").GetComponent<ScoringManger>().GetSpeedBoost()* PRODUCT_CAPESPEED;
        counter_time = Random.Range(0.0f, 1.5f);
        System.Random rand = new System.Random((int)System.DateTime.Now.Ticks);
        number_patroller = rand.Next(0, 3);
        target = GameObject.Find("player(Clone)");
        m_Body = GetComponent<Rigidbody2D>();
        speed = speed * Random.Range(1.0f, 1.5f);
        StartCoroutine(WaitAndSearch(1));
    }



    // Update is called once per frame
    void FixedUpdate()
    {
        if (positions_Patroller != null)
        {
            Vector3 direction = Vector3.zero;
            switch (state_enemy)
            {
                case state.PATROLLER:
                    if (bfs != null)
                    {
                        Path = bfs.CalculateBFS(GameObject.Find("GameManager").GetComponent<Grid>(), target.transform.position, transform.position);
                        int countdistancenode = 0;
                        foreach (Node node in Path)
                        {
                            countdistancenode++;
                        }
                        if (countdistancenode < FIELD_Of_VIEW)
                        {
                            state_enemy = state.FOLLOWER;
                        }
                    }
                 

                    Vector2 delta_pos = positions_Patroller[number_patroller] - transform.position;
                   
                    if (delta_pos.sqrMagnitude <= 0.8f)
                    {
                        number_patroller++;
                        if (number_patroller == positions_Patroller.Length )
                        {
                            number_patroller = 0;
                        }
                    }
                    else
                    {
                        dir = delta_pos.normalized * Time.deltaTime;
                        m_Body.velocity = capedSpeed * dir;
                    }
                    break;

                case state.FOLLOWER:
                   
                    if ((target.transform.position - transform.position).sqrMagnitude > FIELD_Of_VIEW)
                    {
                        Path = bfs.CalculateBFS(GameObject.Find("GameManager").GetComponent<Grid>(), target.transform.position, transform.position);
                        if ((transform.position - new Vector3((int)Path[0].Position.x, (int)Path[0].Position.y)).sqrMagnitude <= DISTANCE_MIN_NODE)
                        {
                            Path.RemoveAt(0);
                        }
                        dir = (new Vector3((int)Path[0].Position.x, (int)Path[0].Position.y) - transform.position).normalized;
                        m_Body.velocity = speed * dir;
                    }
                    else {
                        if (move_counter <= 0)
                        {
                            counter_time = 0;
                            state_enemy = state.JUMP;
                        }
                        else
                        {
                            counter_time += Time.deltaTime;
                            dir = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);
                            if (counter_time >= TIME_FOR_MOVE)
                            {
                                move_counter--;
                                m_Body.velocity = dir * speed / 2;
                            }
                        }
                    }
                   
                    break;

                case state.JUMP:
                    if(counter_time == 0)
                    {
                        dir = target.transform.position - transform.position;
                    }
                    counter_time += Time.deltaTime;
                    if(counter_time <= 1f)
                    {
                            m_Body.velocity = dir.normalized*speed;
                    }
                    else
                    {
                        counter_time = 0;
                        move_counter = 3;
                        state_enemy = state.FOLLOWER;
                    }
                    break;
            }
        }
    }

    public void SetPositionsPatroller(Vector3[] _pos_patroller)
    {
        positions_Patroller = new Vector3[_pos_patroller.Length];
        positions_Patroller = _pos_patroller;
    }

    private void OnDrawGizmos()
    {
        switch (state_enemy)
        {
            case state.FOLLOWER:
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, new Vector3((int)Path[0].Position.x, (int)Path[0].Position.y));
                break;

            case state.PATROLLER:
                Gizmos.color = Color.cyan;
                Gizmos.DrawLine(transform.position ,positions_Patroller[number_patroller]);
                break;
        }
      
    }


    private IEnumerator WaitAndSearch(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            bfs = new BFS(GameObject.Find("GameManager").GetComponent<GameManager>().getColumns(), GameObject.Find("GameManager").GetComponent<GameManager>().getRows());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == (int)LAYERMASKS.PLAYER)
        {
            counter_time_attack += Time.deltaTime;
            if (counter_time_attack >= TIME_FOR_ATTACK)
            {
                counter_time_attack = 0;
                target.GetComponent<PlayerCharacter>().TakeDamage();
                Debug.Log("Take_Damage");
            }
        }
    }
}
