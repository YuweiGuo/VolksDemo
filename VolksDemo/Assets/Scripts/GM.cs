using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GM : MonoBehaviour
{
    public List<int> SubMapIds;
    public Player Niko;
    public Map TestMap;

    public GameObject RollButton;

    public GameObject CupButton;

    public GameObject RankButton;

    public GameObject LocateButton;
    
    public int DiceStatus = 0;
    public GameObject Dice1Button;
    public GameObject Dice2Button;
    public GameObject Dice3Button;

    public int RewardRecorder = 0;
    public int BackFromWord = 0;
    // Start is called before the first frame update
    void Start()
    {
        SubMapIds = new List<int> {0,1,2,3,4,5,0,1,3,4,3,2,1,0,5,1,2,4,1,2,0,3,3,5,1,1,2,4,4,3,1,1,6};
        TestMap = new Map();
        TestMap.inGM = this;
        TestMap.BuildMapWithSubMapIds(SubMapIds);
        TestMap.ShowMap();

        Niko = new Player();
        Niko.InitPlayer(TestMap.StartTile);
        Niko.ShowPlayer();

        // roll 点功能
        RollButton = Instantiate(Tools.FindPfbPerName("rollbutton_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;
        RollButton.GetComponent<RollADice>().inGM = this;

        // cup相关
        CupButton = Instantiate(Tools.FindPfbPerName("cup_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;

        // rank相关
        RankButton = Instantiate(Tools.FindPfbPerName("rank_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;

        // locate相关
        LocateButton = Instantiate(Tools.FindPfbPerName("locate_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;
        LocateButton.GetComponent<LocateClick>().inGM = this;

        // item dice 相关
        
        Dice1Button = Instantiate(Tools.FindPfbPerName("dice1_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;
        Dice1Button.GetComponent<Dice1Click>().inGM = this;
        Dice2Button = Instantiate(Tools.FindPfbPerName("dice2_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;
        Dice2Button.GetComponent<Dice1Click>().inGM = this;
        Dice3Button = Instantiate(Tools.FindPfbPerName("dice3_pfb"), GameObject.FindGameObjectWithTag("UICanvas").transform) as GameObject;
        Dice3Button.GetComponent<Dice1Click>().inGM = this;

    }

    // Update is called once per frame
    void Update()
    {
        if(BackFromWord == 1)
        {
            if (RewardRecorder == 3)
            {
                RewardRecorder = 0;
                StartCoroutine(RewardAnimation());
            }
        }

        BackFromWord = 0;
    }

    public void PlayerGoForward(int StepCount)
    {
        StartCoroutine(GoForwardAnimation(StepCount));
    }

    public void PlayerGoBack()
    {
        StartCoroutine(GoBackAnimation());
    }

    private IEnumerator GoForwardAnimation(int StepCount)
    {
        for (int StepNow = 0; StepNow < StepCount; StepNow++)
        {
            if (Niko.Position.Left == null) yield break;
            Niko.GoForward();
            Niko.PlayerPfb.transform.DOLocalMoveY(Niko.Position.TilePfb.transform.localPosition.y + Niko.PlayerPfb.GetComponent<RectTransform>().sizeDelta.y / 2f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            //判断类型停止  
            if ((int)Niko.Position.Type >= 1 && (int)Niko.Position.Type < 6) break;    
        }
        Niko.Position.TilePfb.GetComponent<TileManager>().DoEvent(Niko.Position.Type, this);
    }
    private IEnumerator GoBackAnimation()
    {
       
        if (Niko.Position.Pre == null) yield break;
        Niko.GoBack();
        Niko.PlayerPfb.transform.DOLocalMoveY(Niko.Position.TilePfb.transform.localPosition.y + Niko.PlayerPfb.GetComponent<RectTransform>().sizeDelta.y / 2f, 0.5f);
        yield return new WaitForSeconds(1.5f);
        Niko.Position.TilePfb.GetComponent<TileManager>().DoEvent(Niko.Position.Type, this);
    }

    public void LocateCamera()
    {
        MapController mc = MapController.GetInstance();
        mc.Camera.transform.DOMoveY(Niko.PlayerPfb.transform.position.y + 20, 0.5f);  ;
    }

    public void DiceClick(int Type)
    {
        GameObject Dice = GameObject.FindGameObjectWithTag("dice"+Type.ToString());
        if (DiceStatus == 0)
        {
            Dice.transform.Find("selected").gameObject.SetActive(true);
            DiceStatus = Type;
        }
        else
        {
            if (DiceStatus == Type)
            {
                Dice.transform.Find("selected").gameObject.SetActive(false);
                DiceStatus = 0;
            }
            else
            {
                for (int typeall = 1; typeall < 4; typeall++)
                {
                    GameObject DiceN = GameObject.FindGameObjectWithTag("dice"+typeall.ToString());
                    DiceN.transform.Find("selected").gameObject.SetActive(false);
                }
                Dice.transform.Find("selected").gameObject.SetActive(true);
                DiceStatus = Type;
            }
        }

    }

    public void ResetDice()
    {
        for (int typeall = 1; typeall < 4; typeall++)
        {
            GameObject DiceN = GameObject.FindGameObjectWithTag("dice"+typeall.ToString());
            DiceN.transform.Find("selected").gameObject.SetActive(false);
        }
        DiceStatus = 0;
    }
    
    public IEnumerator RewardAnimation()
    {
        GameObject ca = GameObject.FindGameObjectWithTag("PopCanvas");
        GameObject reward = ca.transform.Find("Reward").gameObject;
        reward.SetActive(true);
        float imageCount = 20;
        float playTime = 2f;
        for (int imageNum = 0; imageNum < imageCount; imageNum++)
        {
            reward.GetComponent<Image>().sprite = Tools.FindSpritePerName("reward" + UnityEngine.Random.Range(1, 9).ToString());
            yield return new WaitForSeconds(playTime*2f/(imageCount*(imageCount+1)) * (imageNum+1f));
        }
        yield return new WaitForSeconds(1f);
        float yBefore = reward.transform.position.y;
        reward.transform.DOMoveY(yBefore + 20, 1f);
        yield return new WaitForSeconds(1f);
        reward.SetActive(false);
        reward.transform.position = new Vector3(reward.transform.position.x, yBefore, 0);
    }

}
