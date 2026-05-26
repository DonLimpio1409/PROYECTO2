using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections; 
using Unity.VisualScripting;

public class FSMPlayerManager : StateMachineFlowPlayer
{
    //Estados
    public Walk walkState;
    public Fight fightState;
    public Die dieState;
    public TutorialSta tutorialState;

    private void Awake()
    {
        walkState = new Walk(this);
        fightState = new Fight(this);
        dieState = new Die(this);
        tutorialState = new TutorialSta(this);
        lifeList.Enqueue(life1);
        lifeList.Enqueue(life2);
        lifeList.Enqueue(life3);
    }
    protected override void GetInitialState(out TemplateStateMachinePlayer _stateMachine)
    {
        _stateMachine = tutorialState;
    }

    [Header("Elementos de uso")]
    public Rigidbody rb = new Rigidbody();
    public Animator anim = new Animator();
    public TextMeshProUGUI livesText;
    public WayPointDataPlayer wayPointData;
    public GameObject tutorialControl;
    public GameObject black;


    [Header("Walk")]
    public float speed = 1f;
    public bool enemyBlock;
    public int e = 0;
    public GameObject waypoint1;
    public GameObject waypoint2;
    public GameObject waypoint3;
    public GameObject waypoint4;
    public GameObject waypoint5;
    public GameObject waypoint6;
    public GameObject waypoint7;

    [Header("lvlPass")]
    public Animator endlvlMenu;

    [Header("Die")]
    public GameObject dieMenu;
    public GameObject tempBlackDie;
    public Animator redDie;

    [Header("Fight")]
    public List<GameObject> fightersList = new List<GameObject>();
    public GameObject cameraR;
    public int i = 0; 
    public bool blocking = false;
    public float cooldonwBlock = 2f;
    public float blocktime = 0;
    public int hp = 3;
    public Image lifeImage;
    public Queue<Sprite> lifeList = new Queue<Sprite>();
    public Sprite life1;
    public Sprite life2;
    public Sprite life3;

    [Header("EndGame")]
    public Animator endGameMenu;

    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyBlock = true;
            fightersList.Add(other.gameObject);
        }

        if(other.gameObject.CompareTag("Destiny"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endlvlMenu.SetBool("End", true);
            SoundController.Instance.footstepAudioSource.Stop();
            Time.timeScale = 0;
        }

        if(other.gameObject.CompareTag("EndGame"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            endGameMenu.SetBool("EndGame", true);
            SoundController.Instance.footstepAudioSource.Stop();
            Time.timeScale = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "WayPointPlayer")
        {
            Debug.Log("WayPointPlayer");
            e++;
            Destroy(other.gameObject);
        }
    }
}
