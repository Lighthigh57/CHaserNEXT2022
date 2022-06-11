using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private string scene;

    private RawImage cover;
    // Start is called before the first frame update
    private IEnumerator Start()
    {
        cover = GetComponent<RawImage>();
        yield return cover.DOFade(1, 0.5f)
            .WaitForCompletion();
        
    }

    // Update is called once per frame
    public IEnumerator BackScene()
    {
        
        yield return cover.DOFade(1, 0.5f)
            .WaitForCompletion();
        SceneManager.LoadScene(scene);
    }
}
