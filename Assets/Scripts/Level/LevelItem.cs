using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelItem : MonoBehaviour
{
    //�жϹؿ��Ƿ��ѽ���
    static public bool[] levelIsUnlock = { true, false, false, false, false };

    public int levelID; //��Ӧ�ؿ������
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
        //�������أ�����ؿ�
        SceneManager.LoadScene("Level"+levelID);
    }
}
