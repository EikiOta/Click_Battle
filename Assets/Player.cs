using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{

    

    private int levelMinHp = 20;
    private int levelMaxHp = 10000;
    private int maxLevel = 100;


    public Text coinLabel;
    public Text levelLabel;
    public int coin;
    public int level;
    public int kill;

    public Button powerUpBtn;

    public Text powerUpCoinText;

    public int PowerUpCoin(){
        return (level - 1) * 20 +100;
    }



    public void UpdateUI(){
        coinLabel.text = "Coin" + coin;
        levelLabel.text = "Level" + level;

        int require = PowerUpCoin();
        if(coin <require){
            powerUpBtn.interactable = false;
        }else{
            powerUpBtn.interactable = true;
        }

        powerUpCoinText.text = "Coin" + require;
    }
    public void AddCoin(int amount){
        coin += amount;
        UpdateUI();
        SaveData();
    }
    public void SaveData(){
        PlayerPrefs.SetInt("coin", coin);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("kill", kill);
        
    }
    private void Awake(){
        coin = PlayerPrefs.GetInt("coin", coin);
        level = PlayerPrefs.GetInt("level", level);
        kill = PlayerPrefs.GetInt("kill", kill);

    }

    public void OnPowerUp(){
        int require = PowerUpCoin();
        if(coin >= require){
            level++;
            AddCoin(-require);
        }

    }

    public void OnReset(){
        PlayerPrefs.DeleteAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
