using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Business> businesses;

    private void Start()
    {
        PlayerData.ownedBusinesses = new Dictionary<string, Business>();

        businesses[0].ActivateBusiness();
        FindObjectOfType<AudioManager>().Play("Background");
    }
}
