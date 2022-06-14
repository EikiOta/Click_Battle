using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public Player player;
    private int levelMinHp = 20;
    private int levelMaxHp = 10000;
    private int maxLevel = 100;
    private float degree = 1.2f;
    private float coinMultiplier = 0.5f;



    public Text hpLabel;

    private int hp;
    private int maxHp;

    public Image monsterImage;

    public Sprite[] monsterImages;
    public GameObject hitEffect;

    public GameObject coinEffect;
    public AudioClip coinSE;

    public AudioSource audioSource;
    private int CalcHp(){
        float tmp = Mathf.Pow((float)player.kill/maxLevel, degree);
        int hp = (int)((levelMaxHp - levelMinHp) * tmp + levelMinHp + 0.5f);

        return hp;
    }

    private void SetUp(){
        maxHp = CalcHp();
        hp = maxHp;
        hpLabel.text = hp + "/" + maxHp;

        int imageIndex = Random.Range(0, monsterImages.Length);

        monsterImage.sprite = monsterImages[imageIndex];
    }

    // Start is called before the first frame update
    void Start()
    {
        SetUp();
    }
    public void OnClickMonster(){
        hp -= player.level;
        hpLabel.text = hp + "/" + maxHp;

        GameObject hit = Instantiate(hitEffect, transform.position,
            Quaternion.identity);
        Destroy(hit, 0.5f);

        if(hp <= 0){
            audioSource.PlayOneShot(coinSE);
            GameObject coin = Instantiate(coinEffect,transform.position, Quaternion.identity);
            Destroy(coin, 3f);
            player.kill++;

            int amount = (int)(CalcHp() * coinMultiplier);
            player.AddCoin(amount);
            SetUp();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
