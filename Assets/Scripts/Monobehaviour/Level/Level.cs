using NavMeshPlus.Components;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public GameParameters gameParameters;
    public NavMeshSurface surface;

    private MusicManager music;

    public Camera camera;
    //public NavigationCollect

    public bool onAlarm;

    private Player player;

    void Awake()
    {
        music = GetComponent<MusicManager>();

        camera = FindFirstObjectByType<Camera>();

        surface = GetComponentInChildren<NavMeshSurface>();
        //Debug.Log("Level.Awake() called in " + gameObject.scene.name);

        player = GetComponentInChildren<Player>();

        onAlarm = false;

        //Pitch
        float startPitch = music.soundParameters.onStartSoundStartPitch;
        music.soundParameters.onStartSoundPitch = startPitch;

        SceneManager.sceneLoaded += OnSceneLoaded;


    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene name: {scene.name}");

/*        if (camera)
        {
            camera.transform.localPosition = Vector2.zero;
            camera.transform.position = Vector2.zero;
            camera.GetComponent<CinemachineBrain>().enabled = true;
        }*/
    }

    void Start()
    {
        camera.transform.localPosition = new Vector3(0, 0, -10);
        camera.transform.position = new Vector3(0, 0, -10);

        StartCoroutine(BuildLevel());

        foreach (Chamber chamber in GetComponentsInChildren<Chamber>())
        {
            chamber.transform.GetComponentInChildren<DoorsController>().OpenDoors();
        }
    }

    IEnumerator BuildLevel()
    {
        yield return new WaitForEndOfFrame(); // ∆дем конца кадра
        yield return new WaitForFixedUpdate();
        foreach (DoorsController doorsController in GetComponentsInChildren<DoorsController>())
        {
            doorsController.WallsToDoors();
        }

        RegenerateCompositeCollider2Ds();

        surface?.BuildNavMesh();

        music.StartToPlayMusic();

        yield return new WaitForSeconds(0.2f);

        if (camera)
        {
            camera.GetComponent<CinemachineBrain>().enabled = true;
        }

        player.SetStartPosition();
    }

    void RegenerateCompositeCollider2Ds()
    {
        CompositeCollider2D[] compositeCollider2Ds = GetComponentsInChildren<CompositeCollider2D>();
        foreach (CompositeCollider2D compositeCollider2D in compositeCollider2Ds)
        {
            compositeCollider2D.GenerateGeometry();
        }
    }

}
