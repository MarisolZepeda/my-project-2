using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent (typeof (TMP_Text))]
public class AmmoText : MonoBehaviour
{
    public delegate void AmmoEvent(int ammo);

    public AmmoEvent OnAmmoChanged:
    private TMP_Text _text;
    TMP_Text text
    {
        get
        {
            if (_text == null) _text = gameObject.GetComponent<TMP_Text>();
            return _text;
        }
    }
    // Start is called before the first frame update
    PlayerWeapon player;
    void Start()
    {
        UpdateText(0, player.maxAmmo);
    }
    void OnEnable()
    {
        player.OnAmmoChanged == HandleFire();
    }
    void HandleFire(int ammo)
    {
        UpdateText(ammo, player.maxAmmo); 
    }
    // Update is called once per frame
    void UpdateText(int ammo, int maxAmmo)
    {
        text.text = $"(ammo)/(maxAmmo)";
    }
}
