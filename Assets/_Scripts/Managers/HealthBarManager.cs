using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarManager : MonoBehaviour
{
    [SerializeField] Image playerHealthImage;
    [SerializeField] GameObject healthBarPrefab;
    [SerializeField] Transform CanvasParent;

    private Transform cameraT;
    private Player player;
    private Dictionary<ZombieAI, Image> healths = new Dictionary<ZombieAI, Image>();
 
    private void OnEnable()
    {
        ZombieMode.ZombieCreated += ZombieCreated;
    }
    private void OnDisable()
    {
        ZombieMode.ZombieCreated -= ZombieCreated;
    }
    private void Start()
    {
        cameraT = Camera.main.transform;
        player = GameManager.Instance.player;
    }
    void ZombieCreated(ZombieAI zombie)
    {
        var bar = Instantiate(healthBarPrefab, CanvasParent);
        bar.SetActive(false);
        Image barImage = bar.transform.GetChild(1).GetComponent<Image>();
        healths.Add(zombie, barImage);
    }
    void Update()
    {
        if(GameManager.Instance.started)
        {
            HandleHealths();
            HandlePlayerHealth();
        }
    }
    void HandleHealths()
    {
        if (healths.Count == 0)
            return;

        foreach (KeyValuePair<ZombieAI, Image> kvp in healths)
        {
            var parentBar = kvp.Value.transform.parent;
            if (kvp.Key.gameObject.activeSelf)
            {
                //health image fill with health value
                float fillAmount = kvp.Key.GetHealth() / kvp.Key.GetBaseHealth();

                kvp.Value.fillAmount = fillAmount >= 0 ? fillAmount : 0;
                //if bar is not active then activate if health >0

                bool active = fillAmount > 0;
                parentBar.gameObject.SetActive(active);

                //position amd rotation adjustment
                parentBar.position = kvp.Key.transform.position;
                parentBar.GetChild(0).LookAt(cameraT);
                parentBar.GetChild(1).LookAt(cameraT);
            }
            else
            {
                if(parentBar.gameObject.activeSelf)
                    parentBar.gameObject.SetActive(false);
            }
        }
    }

    void HandlePlayerHealth()
    {
        playerHealthImage.fillAmount = player.GetHealth() / player.GetBaseHealth();
        if (playerHealthImage.fillAmount < .3f)
            playerHealthImage.color = Color.red;
        else
            playerHealthImage.color = Color.green;
    }
}
