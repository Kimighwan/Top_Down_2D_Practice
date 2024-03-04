using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public TypeEffect talk;
    public GameObject scanObject;
    public Animator talkPanel;
    public bool isAction;
    public TalkManager talkManager;
    public int talkIndex;
    public Image portraitImg;
    public Animator portraitAnimator;
    public QuestManager questManager;
    public Sprite prevPortrait;
    public GameObject menuSet;
    public Text questTalk;
    public GameObject player;
    public Text npcName;

    private void Start()
    {
        GameLoad();
        questTalk.text = questManager.CheckQuest();
    }

    private void Update()
    {
        //Sub Menu
        if (Input.GetButtonDown("Cancel"))
        {
            SubMenuActive();
        }    
    }

    public void SubMenuActive() {
        if (menuSet.activeSelf)
            menuSet.SetActive(false);
        else
            menuSet.SetActive(true);
    }

    public void Action(GameObject scanObj)
    {

        scanObject = scanObj; // 원래 바로 밑 코드는 talkText.text = "이것의 이름은 " + scanObject.name + "이라고 한다.";
        ObjData objData = scanObject.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        talkPanel.SetBool("isShow", isAction);

    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = 0;
        string talkData = "";
        //Set Talk Data
        if (talk.isAnim) 
        {
            talk.SetMeg("");
            return;
        }
        else
        {
            questTalkIndex = questManager.GetQuestTalkIndex();
            talkData = talkManager.GetTalk(id + questTalkIndex, talkIndex);

        }

        //End Talk
        if(talkData == null)
        {
            isAction = false;
            talkIndex = 0;
            questTalk.text = questManager.CheckQuest(id, isNpc);
            return;
        }

        if (!isNpc)
            npcName.text = null;

        if (isNpc) {
            talk.SetMeg(talkData.Split(':')[0]);

            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse(talkData.Split(':')[1]));
            portraitImg.color = new Color(1,1,1,1);
            //Animation Portrait
            if(prevPortrait != portraitImg.sprite)
            {
                portraitAnimator.SetTrigger("doEffect");
                prevPortrait = portraitImg.sprite;
            }
            npcName.text = scanObject.GetComponent<ObjData>().name;


        }
        else
        {
            talk.SetMeg(talkData);

            portraitImg.color = new Color(1, 1, 1, 0);
        }
        isAction = true;
        talkIndex++;      
    }

    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", questManager.questId);
        PlayerPrefs.SetInt("QuestActionIndex", questManager.questActionIndex);
        PlayerPrefs.Save();

        menuSet.SetActive(false);
    }

    public void GameLoad()
    {
        if (!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x = PlayerPrefs.GetFloat("PlayerX");
        float y = PlayerPrefs.GetFloat("PlayerY");
        int questId = PlayerPrefs.GetInt("questId");
        int questActionIdex = PlayerPrefs.GetInt("questActionIdex");

        player.transform.position = new Vector3(x, y, 0);
        questManager.questId = questId;
        questManager.questActionIndex = questActionIdex;
        questManager.ControlObject();
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
