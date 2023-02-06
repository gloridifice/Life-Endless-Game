using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelItem : MonoBehaviour
{
    //判断关卡是否已解锁
    static public bool[] levelIsUnlock = { true, false, false, false, false };

    public int levelID; //对应关卡的序号
    private Button levelButton;

    void Awake()
    {
        levelButton = GetComponent<Button>();
        levelButton.onClick.AddListener(OnClick);
        if(levelIsUnlock[levelID])
        {
            levelButton.interactable = true;
        }
        else
        {
            levelButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClick()
    {
        //场景加载，进入关卡
        SceneManager.LoadScene("Level"+levelID);
    }
}
