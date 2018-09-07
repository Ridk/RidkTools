using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManagement : MonoBehaviour
{
    private  Text showName;

    [SerializeField]
    public VideoPlayer _videoPlayer;
    private GameObject videoObject;
    private string videoPath="/views/3d_test/video";
    public  Text ShowName
    {
        get { return showName; }
    }

    [SerializeField]
    public GameObject FireBtn;
    public GameObject Menu;
    public static UIManagement UIM;
    [SerializeField]
    private GameObject videoBG;
    private string[] contentText =
    {
        "欢迎来到<自动喷水灭火系统>的课程学习",
        "首先,我们把左边的设备拖放到相应的位置.",
        "第二步,把对应的设备连线",
        "第三部步,进入火灾模拟学习",
        "即将发生火灾,请保持镇定认真观察相应设备的反应.",
        "恭喜你完成了此课程的学习!",
        "操作错误!"
    };
    [SerializeField] public  GameObject WizardP;

    private Image Content;

    private void Awake()
    {
        videoObject = _videoPlayer.gameObject;
        UIM = this;
        showName = GetComponentInChildren<Text>();
        showName.raycastTarget = false;
        Content = WizardP.transform.Find("Content").GetComponent<Image>();
    }

/*   public void VideoPlay(string name)
   {
       if (_videoPlayer!=null)
       {
           Destroy(_videoPlayer);
           _videoPlayer=videoObject.AddComponent<VideoPlayer>();
           _videoPlayer.playOnAwake = false;
           _videoPlayer.renderMode = VideoRenderMode.MaterialOverride;
       }
         videoBG.SetActive(false);
       _videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
       _videoPlayer.gameObject.SetActive(false);
       _videoPlayer.gameObject.SetActive(true);
       _videoPlayer.url = videoPath + "/" + name + ".mp4";
       _videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
       _videoPlayer.controlledAudioTrackCount = 1;
       var aus = _videoPlayer.gameObject.GetComponent<AudioSource>();
       _videoPlayer.SetTargetAudioSource(0,aus);
       _videoPlayer.Play();
       _videoPlayer.aspectRatio = VideoAspectRatio.Stretch;
   }*/

/*    public void ShowContent()
    {
        Content.GetComponentInChildren<Text>().text = contentText[(int) UserControl.userControl._projectStatus];
        WizardP.SetActive(true);
    }
    public void ShowContent(int index)
    {
        
        Content.GetComponentInChildren<Text>().text = contentText[index];
        WizardP.SetActive(true);
    }*/

    /*public void Wizard()
    {
        switch (UserControl.userControl._projectStatus)
        {
            case ProjectStatus.Wizard:
            case ProjectStatus.Mount:
            case ProjectStatus.Connect:
                WizardP.SetActive(false);
                break;
            case ProjectStatus.Complete:
                WizardP.SetActive(false);
                UserControl.userControl.isComplete = true;
                UIM.FireBtn.SetActive(UserControl.userControl.isComplete );
                break;
            case ProjectStatus.Warning:
                WizardP.SetActive(false);
                UserControl.userControl.FireMode();
                UserControl.userControl._projectStatus = ProjectStatus.Complete;
                break;
            case ProjectStatus.FierMode:
                UserControl.userControl._projectStatus = ProjectStatus.Warning;
                Content.GetComponentInChildren<Text>().text = contentText[(int) UserControl.userControl._projectStatus];
                break;
        }
    }*/
}