using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuTrigger : MonoBehaviour {
    static bool retryOldBones;
    static bool retryOlov;
    static bool retryIzel;
    Player player;
    public GameObject SpaceText;
    public GameObject Panel;
    public Text Titulo;
    public string TituloJuego;
    public Color TextColor = Color.yellow;
    public GameObject StartTxt;
    public GameObject ExixtTxt; 
    public int SceneIndx;
    public int FightScn;
    public float OurTime;
    //public Scene BossScene;
    public float Score;
    string ScoreLetter;
    public Text ScoreLet;
    public Text ScoreTime;
    public string bossTip;
    public TextMesh Tip_Text;
    public bool ready=true;

	AudioSource button;

    bool input=false;
    bool start = true;
    bool input2=false;

	void Start()
	{
        print("He llegado aqui");
//        print(PlayerPrefs.GetFloat(SceneIndx.ToString()));
        Score = PlayerPrefs.GetFloat(FightScn.ToString(), 0);

        //PlayerPrefs.SetFloat("Max" + FightScn.ToString(), Score);
        PlayerPrefs.SetFloat("Maxi" + FightScn.ToString(), OurTime);
        //print(SceneManager.GetSceneAt(SceneIndx).name);
        //        print(gameObject.name + " score: " + Score);
        button = GetComponent<AudioSource> ();
	}

    // Use this for initialization
    private void Update()
    {
        arcade();
        if (input)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                player.enabled = false;
                Panel.SetActive(true);
                SetScore();
                Titulo.text = TituloJuego;
                Titulo.color = TextColor;
                input = false;
                input2 = true;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("AAAAAH ME HAN TOCADO");
        if (collision.GetComponent<Player>())
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                
                player = collision.GetComponent<Player>();
                SpaceText.GetComponent<Text>().color = TextColor;
                if (ready)
                {
                    input = true;
                    SpaceText.GetComponent<Text>().text = "Press space to enter";
                }
                else
                {
                    input = false;
                    string[] soon = { "Soon...", "Buy - 5$", "DLC - Buy?", "We're working on it", "We need artists","Not enough monkeys", "Just...not yet", "Tomorrow...maybe", "On repairs", "Try later", "Yeah...well...no","Not enough cofee","Do you eve have a life?" };
                    SpaceText.GetComponent<Text>().text = soon[Random.Range(0, soon.Length)];
                }
                SpaceText.GetComponent<ParpadeoEfecto>().enabled = ready;
                SpaceText.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            input = false;
            SpaceText.SetActive(false);
        }
    }
    void arcade()
    {
        if (input2)
        {
            ExixtTxt.GetComponent<ParpadeoEfecto>().enabled = !start;
            StartTxt.GetComponent<ParpadeoEfecto>().enabled = start;
			if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown (KeyCode.DownArrow)) {
				start = false;
				button.Play ();
			} else if (Input.GetKeyDown (KeyCode.W) || Input.GetKeyDown (KeyCode.UpArrow)) {
				start = true;
				button.Play ();
			}

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                if (start)
                {
                    Tip_Text.text = bossTip;
                    LevelLoader lvloader = FindObjectOfType<LevelLoader>();
                    lvloader.level = checkLoad();
                    lvloader.TextColor = TextColor;
                    lvloader.Title_Text.color = TextColor;
                    lvloader.LevelName = TituloJuego;
                    StartCoroutine(MoveToPlace());
                }
                else
                    player.enabled = true;
                start = true;
                input2 = false;
                Panel.SetActive(false);
            }
        }
    }
    IEnumerator MoveToPlace ()
    {
        player.anim.SetBool("Up", true);
        while(player.transform.position!= FindObjectOfType<LevelLoader>().gameObject.transform.position)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, FindObjectOfType<LevelLoader>().gameObject.transform.position,player.speed* Time.deltaTime/2);
            yield return new WaitForEndOfFrame();
        }
            player.anim.SetBool("Up", false);
        player.enabled = true;
        
        yield return null;
    }
    void SetScore()
    {
        if (Score == 0)
            ScoreLetter = "";
        else if (Score > OurTime * 2.5)
            ScoreLetter = "D";
        else if (Score > OurTime * 2)
            ScoreLetter = "C";
        else if (Score > OurTime * 1.5)
            ScoreLetter = "B";
        else if (Score > OurTime)
            ScoreLetter = "A";
        else
            ScoreLetter = "S";
        ScoreLet.text = ScoreLetter;
        //PlayerPrefs.SetString("Rank" + FightScn.ToString(), ScoreLetter);
        ScoreTime.text = Mathf.Round(Score).ToString() + " secs";
    }
    private int checkLoad()
    {
        if (SceneIndx == 5 && !retryOlov)
        {
            cleanretrys();
            retryOlov = true;
            return SceneIndx;
        }
        else if (SceneIndx == 5 && retryOlov)
        {
            return 4;
        }
        else if (SceneIndx == 6 && !retryOldBones)
        {
            cleanretrys();
            retryOldBones = true;
            return SceneIndx;
        }
        else if (SceneIndx == 6 && retryOldBones)
        {
            return 3;
        }
        else if (SceneIndx == 7 && !retryIzel)
        {
            cleanretrys();
            retryIzel = true;
            return SceneIndx;
        }
        else if (SceneIndx == 7 && retryIzel)
        {
            return 9;
        }
        else return 1;

    }
    private void cleanretrys()
    {
        retryIzel = false;
        retryOlov = false;
        retryOldBones = false;
    }
}


