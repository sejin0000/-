using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager I;
    public static int Type = 1;

    public GameObject firstCard;
    public GameObject secondCard;

    public GameObject card;
    public GameObject endPanel;
    public GameObject nameCard; 

    public AudioClip match;                                 // AudioClip : 음악 파일 원본
    public AudioClip fail;
    public AudioSource audioSource;                        // AudioSource : 누가 음악 파일을 플레이 할 것인가

    public Text timeTxt;
    public Text bestTimeTxt;
    public Text scoreTxt;
    public Text countTxt;
    public Text nameTxet;

    public bool isGameEnd = false;

    int[] member = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int count = 0;
    int pair = 0;
    int t = 0;
    int k = 0;
    bool cooldowncheck = false;
    float cooldown = 0;
    float alive = Type * 20;

    void Awake()
    {
         I = this;
    }

    void Start()
    {
        Time.timeScale = 1;
        Dealer();
    }

    void Update()
    {
        if (pair == (Type * 3))
        {
            Invoke("GameOver",1.0f);
            PlayerPrefs.SetFloat("level10", Type + 1);
            Type += 1;
        }
        if (cooldowncheck)
        {
            cooldown += Time.deltaTime;
            if (cooldown >= 5)
            {
                firstCard.GetComponent<card>().Five_count();
                cooldown = 0;
            }
        }
        alive -= Time.deltaTime;
        timeTxt.text = alive.ToString("N2");
        if (alive <= 10f)
        {
            timeTxt.GetComponent<Animator>().enabled = true;
        }
        if (alive <= 0)
        {
            alive = 0;
            Time.timeScale = 0;
            GameOver();
        }

        
    }

    public void IsMatched()
    {
        string firstCardImage = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImage = secondCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        count++;
        if (firstCardImage == secondCardImage)
        {
            NamepanelOpen();
            pair++;
            alive += 2;
            audioSource.PlayOneShot(match);
            firstCard.GetComponent<card>().DestroyCard();
            secondCard.GetComponent<card>().DestroyCard();
        }
        else
        {
            FailpanelOpen();
            if (alive <= 10f)
            {
                timeTxt.GetComponent<Animator>().enabled = true;
            }

            audioSource.PlayOneShot(fail);
            firstCard.GetComponent<card>().CloseCard();
            secondCard.GetComponent<card>().CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    void Difficulty()
    {
        for (int i = 0; 9 > i ; i++)
        {
            if( i < Type * 3)
            {
                member[i] = i + 1;
                member[i + 9] = i + 1;
            }
            else
            {
                member[i] = 0;
                member[i + 9] = 0;
            }
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        scoreTxt.text = (alive *(Type * 200) + pair * 500).ToString("N0");
        countTxt.text = count.ToString("N0");
        endPanel.SetActive(true);
        isGameEnd = true;

        if (PlayerPrefs.HasKey("bestScore1") == false)
        {
            PlayerPrefs.SetFloat("bestScore1", alive);
        }
        else
        {
            if (PlayerPrefs.GetFloat("bestScore1") < alive)
            {
                PlayerPrefs.SetFloat("bestScore1", alive);
            }
        }
        bestTimeTxt.text = PlayerPrefs.GetFloat("bestScore1").ToString("N2");
    }

    public void Retry()
    {
        SceneManager.LoadScene("StartScene");
    }

    public void Card_check(GameObject card_)
    {
        if (firstCard == null)
        {
            firstCard = card_;
            cooldowncheck = true;
        }
        else if (gameManager.I.secondCard == null)
        {
            secondCard = card_;
            IsMatched();
            cooldowncheck = false;
            cooldown = 0;
        }
    }

    public void NamepanelOpen()
    {
        string numbering = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        if (numbering == "1" || numbering == "4" || numbering == "7")
        {
            nameTxet.text = "조병우";
        }
        if (numbering == "2" || numbering == "5" || numbering == "8")
        {
            nameTxet.text = "지민규";
        }
        if (numbering == "3" || numbering == "6" || numbering == "9")
        {
            nameTxet.text = "이세진";
        }
        nameCard.SetActive(true);
        Invoke("NameCardclclose",1f);
    }
    public void FailpanelOpen()
    {
        string numbering = firstCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite.name;
        nameTxet.text = "실패";
        nameCard.SetActive(true);
        Invoke("NameCardclclose", 1f);
    }
    public void NameCardclclose()
    {
        nameCard.SetActive(false);
    }

    void Dealer()
    {
        bestTimeTxt.text = PlayerPrefs.GetFloat("bestScore1").ToString("N2");

        Difficulty();
        member = member.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();


        for (int j = 0; Type * 2 > j; j++)
        {
            for (int i = 0; 3 > i; i++)
            {
                Vector3 vector = new Vector3((1.4f * i) - 1.4f, (1.4f * -j) + Type, 0);

                GameObject newCard = Instantiate(card);

                newCard.transform.parent = GameObject.Find("cards").transform;
                newCard.transform.position = new Vector3(0,-5.5f, 0);

                newCard.GetComponent<card>().vec = vector;
                newCard.GetComponent<card>().order = k; k++;

                while (member[t] == 0) { t++; }
                string image = member[t].ToString();
                newCard.transform.Find("front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(image);
                t++;
            }






        }
    }
}
