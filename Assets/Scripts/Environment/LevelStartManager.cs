using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class LevelStartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetObjects();
    }

    public void SetObjects()
    {
        GameObject[] g = { CameraManager.GetCamera(), CameraManager.GetLookAt() };
        int index = 0;

        var timeline = gameObject.GetComponent<PlayableDirector>().playableAsset as UnityEngine.Timeline.TimelineAsset;
        foreach (var track in timeline.GetOutputTracks())
        {
            if (track.name.Contains("Animation Track") && !track.name.Contains("2"))
            {
                gameObject.GetComponent<PlayableDirector>().SetGenericBinding(track, g[index]);
                index++;
            }
        }
    }
}
