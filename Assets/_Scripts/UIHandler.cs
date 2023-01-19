using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class UIHandler : Singleton<UIHandler>
{
    [SerializeField] Canvas zombieHealths;
    [SerializeField] Transform zombieParent;
    [SerializeField] Transform zombieDeathMenu;
    [Space(5)]
    [Header("Buttons")]
    [SerializeField] Button playButton;
    [SerializeField] Button playAgainButton;
    [Space(5)]
    [Header("Texts")]
    [SerializeField] TMP_Text ammoText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text newWaveText;

    private string waveString = "Wave Starts in ";
    private string waveCountString = "Wave:";

    private void Start()
    {
        UpdateAmmoText(GunManager.Instance.activeGun.magazineSize);
        playButton.onClick.AddListener(ZombieGameStart);
        playAgainButton.onClick.AddListener(RePlay);
    }

    public void UpdateAmmoText(uint ammoCount) =>
        ammoText.text = ammoCount.ToString();
    
    public void UpdateWaveText(float waveCount) =>
        waveText.text = waveCountString + waveCount;

    public void ZombieGameStart() =>
        GameManager.Instance.StartGame();
    
    public void PlayerDeath() =>
        zombieDeathMenu.gameObject.SetActive(true);
    
    public void StartNewWaveText(float waitTime)
    {
        newWaveText.gameObject.SetActive(true);
        StartCoroutine(CountDown(waitTime));
    }
    IEnumerator CountDown(float seconds)
    {
        float count = seconds;
        while(count >= 0)
        {
            newWaveText.text = waveString + count;
            yield return new WaitForSeconds(1.0f);
            count--;
        }
        if (count <= 0)
            newWaveText.gameObject.SetActive(false);
    }

    public void RePlay()
    {
        GameManager.Instance.LockCursor();
        ZombieMode.Instance.RePlay();
    }
    public void MainMenu() => 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
