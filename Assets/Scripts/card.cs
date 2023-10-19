using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class card : MonoBehaviour
{
    public AudioClip flip;
    public AudioSource audioSource;
    public Animator anim;
    public Vector3 vec = new Vector3(0,0,0);
    public int order = 0;
    bool turn = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Invoke("Order",order * 0.2f);
        if (turn)
        {
            transform.position = Vector3.Lerp(transform.position,vec, (5 * Time.deltaTime));
        }
    }

    public void openCard()
    {
        anim.SetBool("isOpen", true);
        transform.Find("front").gameObject.SetActive(true);
        transform.Find("back").gameObject.SetActive(false);
        transform.Find("back").gameObject.GetComponent<SpriteRenderer>().color = new Color(125 / 255f, 125 / 255f, 125 / 255f, 255 / 255f);
        gameManager.I.Card_check(gameObject);
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }

    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("back").gameObject.SetActive(true);
        transform.Find("front").gameObject.SetActive(false);
    }
    
    public void Five_count()
    {
        if (gameManager.I.firstCard != null && gameManager.I.secondCard == null)
        {
            gameManager.I.firstCard.GetComponent<card>().CloseCardInvoke();
            gameManager.I.firstCard = null;
        }
    }

    void Order()
    {
        turn = true;
    }
}
