using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string levelScene = "Assets/Scenes/hoyo01.unity";
    [SerializeField] private Button playButton;
    [SerializeField] private Text buttonLabel;

    private bool loading;

    void Awake()
    {
        foreach (var ball in FindObjectsByType<GolfBallController>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            ball.enabled = false;
            var body = ball.GetComponent<Rigidbody>();
            if (body != null) body.isKinematic = true;
        }
        foreach (var hole in FindObjectsByType<HoleTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None)) hole.enabled = false;
        foreach (var follow in FindObjectsByType<GolfCameraFollow>(FindObjectsInactive.Include, FindObjectsSortMode.None)) follow.enabled = false;
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Jugar()
    {
        if (loading) return;

        if (!Application.CanStreamedLevelBeLoaded(levelScene))
        {
            Debug.LogError("No se puede cargar el nivel: " + levelScene);
            return;
        }

        loading = true;

        if (playButton != null) playButton.interactable = false;
        if (buttonLabel != null) buttonLabel.text = "CARGANDO...";

        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return null;
        SceneManager.LoadSceneAsync(levelScene, LoadSceneMode.Single);
    }
}