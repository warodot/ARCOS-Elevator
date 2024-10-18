using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class MapEventManager : MonoBehaviour
{
    public List<GameObject> cinematicOBs = new();
    public GameObject CinmematicPlayer;
    public GameObject firstSpawn;
    public List<Item> weaponItems = new();

    public Item buttonItem;
    public GameObject explosionTrigger;

    public AudioSource playMusic;

    public void StartGameplay()
    {
        StartMusic();
        for (int i = 0; i < cinematicOBs.Count; i++)
        {
            cinematicOBs[i].SetActive(false);

        }
        MapPlayerPosManager.instance.GetPlayerRef().transform.SetPositionAndRotation(CinmematicPlayer.transform.position, CinmematicPlayer.transform.rotation);
        MapPlayerPosManager.instance.GetPlayerRef().SetActive(true);
        firstSpawn.SetActive(true);

        for (int i = 0; i < weaponItems.Count; i++)
        {
            RingMenuManager.Instance.AddItem(weaponItems[i]);
        }
    }

    async void StartMusic()
    {
        await Task.Delay(3000);
        playMusic.Play();
    }

    private void Update()
    {
        if (RingMenuManager.Instance.Items.Contains(buttonItem) && !explosionTrigger.activeInHierarchy)
        {
            explosionTrigger.SetActive(true);
        }
    }
}
