using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTrance : MonoBehaviour
{
    private RawImage cover;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        cover = GetComponent<RawImage>();
        yield return cover.DOFade(0, 0.5f)
            .WaitForCompletion();
        cover.raycastTarget = false;
    }

    public void Trance(string targetScene)
    {
        
        cover.raycastTarget = true;
        StartCoroutine(BackScene(targetScene));
    }
    // Update is called once per frame
    private IEnumerator BackScene(string loadScene)
    {
        yield return cover.DOFade(1, 0.5f)
            .WaitForCompletion();
        SceneManager.LoadScene(loadScene);
    }
}
