using UnityEngine;

public class WeaponUnlocker : MonoBehaviour
{
    public GameObject weaponCanvas;
    public int weaponCost;
    public bool isShotgunUnlocked = false;
    public bool isM4Unlocked = false;
    public bool isBeamUnlocked = false;
    public bool isRailgunUnlocked = false;

    private bool playerInRange = false;
    private GameObject player;
    private WeaponSystem weaponSystem;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            weaponSystem = player.GetComponentInChildren<WeaponSystem>();
        }
        else
        {
        
        }

        SetupCanvas();
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            AttemptPurchase();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            weaponCanvas.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            weaponCanvas.SetActive(true);
        }
    }

    void AttemptPurchase()
    {
        if (MoneyScript.Instance.canBuy(weaponCost))
        {
            MoneyScript.Instance.updateMoney(-weaponCost);
            UnlockWeapon();
            Destroy(gameObject);
        }
        else
        {
        
        }
    }

    void UnlockWeapon()
    {
        if (weaponSystem == null)
        {
            return;
        }

        if (isShotgunUnlocked)
        {
            weaponSystem.shotGunActive2 = true;
        }
        else if (isM4Unlocked)
        {
            weaponSystem.m4Active3 = true;
        }
        else if (isBeamUnlocked)
        {
            weaponSystem.beamActive1 = true;
        }
        else if (isRailgunUnlocked)
        {
            weaponSystem.railGunActive4 = true;
        }
        else
        {
            Debug.LogError("No weapon is unlocked.");
        }
    }

    void SetupCanvas()
    {
        if (weaponCanvas != null)
        {
            Canvas canvas = weaponCanvas.GetComponent<Canvas>();
            if (canvas != null)
            {
                canvas.renderMode = RenderMode.WorldSpace;
            }

            weaponCanvas.transform.position = transform.position + Vector3.up * 1.5f;
            weaponCanvas.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }
}
