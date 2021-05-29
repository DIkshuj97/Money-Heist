using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour
{
    public Text gameTimerText;
    public Text ammoText;
    public Text totalAmmoText;
    public Slider healthSlider;
    public Slider slowmoSlider;
    public Slider rewindSlider;
    public GameObject timeUpPanel=null;

    public float Ammo = 5;
    public float TotalAmmo;
    public float maxHealth = 100;
    public float health;
    public int RestartTimes = 3;

    float gameTimer = 300f;
    public float timer;
    TimeBody timeBody;

    bool isReset = false;

    private void Awake()
    {
        int numscene = FindObjectsOfType<GameCanvas>().Length;
        if (numscene > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        timer = gameTimer;
        timeBody = FindObjectOfType<TimeBody>();
        TotalAmmo = Ammo;
        health = maxHealth;
        SetMaxHealth(maxHealth);
        timeUpPanel.SetActive(false);
    }

    private void Update()
    {
        ShowTimer();
        ShowAmmo();
        ShowRewindTimer();
        SetHealth(health);
        ResetGame();
        ResetTimer();
        CheckTimer();
    }

    private void ResetGame()
    {
        int currentindex = SceneManager.GetActiveScene().buildIndex;
        if (currentindex == 0)
        {
            Destroy(gameObject);
        }
    }

    private void ShowAmmo()
    {
        ammoText.text = Ammo.ToString();
        totalAmmoText.text = "/ " + TotalAmmo.ToString();
    }

    private void ShowTimer()
    {
        timer -= Time.deltaTime;

        int seconds = (int)(timer % 60);
        int minutes = (int)(timer / 60) % 60;

        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);
        gameTimerText.text = timerString;
    }

    private void CheckTimer()
    {
        if(timer<=0)
        {
            timeUpPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ShowRewindTimer()
    {
        timeBody = FindObjectOfType<TimeBody>();
        if (timeBody != null)
        {
            SetRewind(timeBody.pointsInTimes.Count);
        }
    }

    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(float health)
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        healthSlider.value = health;
    }

    public void SetMaxSlowmo(float time)
    {
        slowmoSlider.maxValue = time;
        slowmoSlider.value = time;
    }

    public void SetSlowmo(float time)
    {
        slowmoSlider.value = time;
    }

    public void SetRewind(float time)
    {
        rewindSlider.value = time;
    }

    public void IncreaseCurrentAmmo(float ammoAmount)
    {
        Ammo += ammoAmount;
        TotalAmmo += ammoAmount;
    }

    public void ResetTimer()
    {
        if (!isReset)
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                timer = gameTimer;
                isReset = true;
            } 
        }
    }

    public void RestoreInitialStats()
    {
        health = maxHealth;
        Ammo = TotalAmmo;
    }

    public void InitialHealth()
    {
        maxHealth = 100;
        health = maxHealth;
        SetMaxHealth(maxHealth);
    }
}
