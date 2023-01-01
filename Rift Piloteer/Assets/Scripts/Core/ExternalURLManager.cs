using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExternalURLManager : MonoBehaviour
{
    //VK:https://vk.com/public209887529
    //YouTube:https://www.youtube.com/channel/UCNe8oEDBJhv_MPS4SFt8i3A
    //Parteon:https://www.patreon.com/IDEASET
    public string YouTubeTargetURL;
    public string VKTargetURL;
    public string PatreonTargetURL;


    public void OpenYouTubeURL()
    {
        Application.OpenURL(YouTubeTargetURL);
    }

    public void OpenVKURL()
    {
        Application.OpenURL(VKTargetURL);
    }

    public void OpenPatreonURL()
    {
        Application.OpenURL(PatreonTargetURL);
    }
}
