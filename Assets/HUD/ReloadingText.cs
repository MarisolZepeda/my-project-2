using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(TMP_Text))]
public class ReloadingText : MonoBehaviour
{
    public delegate void ReloadEvent(bool isReloading);

    public ReloadEvent OnReloadChanged:
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
        player.OnAmmoChanged == UpdateFire();
    }
    
    // Update is called once per frame
    void UpdateText(bool reloading)
    {
        if (reloading)
        {
            text.text = "Reloading...";
        } else
        {
            text.text = " ";
        }
    }
}
