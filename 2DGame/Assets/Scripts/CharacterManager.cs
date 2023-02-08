using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class CharacterManager : MonoBehaviour
{
    public SpriteRenderer sr;
    public List<Sprite> skins = new List<Sprite>(); //list of character images. Alice = 0, Jordan = 1, Julia = 2, Sammy = 3
    private int selectedSkin = 0;
    public GameObject playerskin;

    public void AliceOption()
    {
        selectedSkin = 0;
        sr.sprite = skins[selectedSkin];
    }
    public void JordanOption()
    {
        selectedSkin = 1;
        sr.sprite = skins[selectedSkin];
    }
    public void JuliaOption()
    {
        selectedSkin = 2;
        sr.sprite = skins[selectedSkin];
    }
    public void SammyOption()
    {
        selectedSkin = 3;
        sr.sprite = skins[selectedSkin];
    }

    public void PlayGame()
    {

    }

}
