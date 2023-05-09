using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleButtons : MonoBehaviour
{
    public GameObject[] dots;
    public TMP_Text text;
    public GameObject choiceisyours;
    public GameObject screen;
    public GameObject readyButton;
    public GameObject backButton;
    public GameObject forwardButton;
    public string[] story;

    public int screenNum = 0;

    void Start()
    {
        text.text = story[screenNum];
    }

    void Update()
    {
      if(screenNum != 0)
      {
        backButton.SetActive(true);
      }
      else
      {
        backButton.SetActive(false);
      }
      if(screenNum < 2)
      {
        forwardButton.SetActive(true);
        choiceisyours.SetActive(false);
        readyButton.SetActive(false);
      }
      else
      {
        forwardButton.SetActive(false);
        choiceisyours.SetActive(true);
        readyButton.SetActive(true);
      }
    }

    public void Next()
    {
      dots[screenNum].SetActive(false);
      screenNum++;
      text.text = story[screenNum];
      dots[screenNum].SetActive(true);
    }

    public void Prev()
    {
      dots[screenNum].SetActive(false);
      screenNum--;
      text.text = story[screenNum];
      dots[screenNum].SetActive(true);
    }
}
