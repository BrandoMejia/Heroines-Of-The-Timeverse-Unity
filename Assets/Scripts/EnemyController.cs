using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public NavMeshAgent enemyAgent;
    public Animator enemyAnimator;
    public Vector2 patrolRange;
    public Transform[] playerTransform;
    private EnemyState currentState;
    private Vector3 randomPosition;
    public float patrolTime;
    //public HealthController health;
    public Image imgVida;
    public bool attack;
    //AudioManager instanceAudioManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        currentState = EnemyState.PATROL;
        UpdateState();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case EnemyState.CHASE:
                if (PlayerPrefs.GetInt("Character") == 1)
                {
                    enemyAgent.SetDestination(playerTransform[0].position);
                }

                if (PlayerPrefs.GetInt("Character") == 2)
                {
                    enemyAgent.SetDestination(playerTransform[1].position);
                }

                if (PlayerPrefs.GetInt("Character") == 3)
                {
                    enemyAgent.SetDestination(playerTransform[2].position);
                }
                //AudioManager.instanceAudioManager.PlaySFX(SFXType.ZOMBIEATTACK);
                break;

            case EnemyState.ATTACK:
                enemyAnimator.SetBool("Attack", true);
                attack = true;
                //AudioManager.instanceAudioManager.PlaySFX(SFXType.ZOMBIEATTACK);
                enemyAnimator.SetBool("Attack", false);
                attack = false;
                break;
        }

        for(int i = 0; i <= 2; i++)
        {
            if (this.transform.position.x - playerTransform[i].transform.position.x < 1.5f || this.transform.position.z - playerTransform[i].transform.position.z < 1.5)
            {
                attack = true;
            }
            else
            {
                attack = false;
            }
        }

        if (imgVida.fillAmount == 0)
        {
            StartCoroutine(EnemyDeath());
          //AudioManager.instanceAudioManager.PlaySFX(SFXType.ZOMBIEDIE);
        }

        enemyAnimator.SetFloat("Speed", enemyAgent.velocity.sqrMagnitude);
    }

    private void UpdateState()
    {
        switch(currentState)
        {
            case EnemyState.PATROL:
                InvokeRepeating("GenerateRandomDestination", 0f, patrolTime);
                break;
        }

        if(attack)
        {
            currentState = EnemyState.ATTACK;
        }
    }

    private void GenerateRandomDestination()
    {
        if (PlayerPrefs.GetInt("Character") == 1)
        {
            randomPosition = playerTransform[0].position + new Vector3(Random.Range(-patrolRange.x, patrolRange.x), 0, Random.Range(-patrolRange.y, patrolRange.y));
        }

        if (PlayerPrefs.GetInt("Character") == 2)
        {
            randomPosition = playerTransform[1].position + new Vector3(Random.Range(-patrolRange.x, patrolRange.x), 0, Random.Range(-patrolRange.y, patrolRange.y));
        }

        if (PlayerPrefs.GetInt("Character") == 3)
        {
            randomPosition = playerTransform[2].position + new Vector3(Random.Range(-patrolRange.x, patrolRange.x), 0, Random.Range(-patrolRange.y, patrolRange.y));
        }
        enemyAgent.SetDestination(randomPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision with: " + other.gameObject.name);
        if(other.transform.CompareTag("Player"))
        {
            if(currentState == EnemyState.PATROL)
            {
                currentState = EnemyState.CHASE;
                //AudioManager.instanceAudioManager.PlayMusic(1);
                CancelInvoke("GenerateRandomDestination");
            }

            
        }

        if (other.gameObject.tag == "Melee")
        {
            imgVida.fillAmount = imgVida.fillAmount - 0.1f;
        }

        if (other.gameObject.tag == "Projectile")
        {
            imgVida.fillAmount = imgVida.fillAmount - 0.1f;
        }

    }

    IEnumerator EnemyDeath()
    {
        enemyAnimator.SetTrigger("Death");
        yield return new WaitForSeconds(10.0f);
        Destroy(this.gameObject);
    }
}

public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
};

