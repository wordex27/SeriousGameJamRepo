using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
public class MonsterController : MonoBehaviour
{
    public TextMeshProUGUI flashText;

    public AudioSource audioSource;

    public AudioClip[] audioClips;
    private bool isDead = false;
    public Image deathScreen;

    public CanvasGroup canvasGroup;
    private Color originalColor;
    private Coroutine flash;
    public Transform[] idlePositions;

    public Transform spawn;

    public float time = 0f;

    public float timeToSpawn = 3f;
    private GameObject player;
    public Transform targ;

    public GameObject playePrefab;
    private LockerController lockerController;
    private float attackDistance;
    private NavMeshAgent agent;

    private float soundTimer = 0f;

    private float timeBetweenSound;
    private float distance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
        agent = GetComponent<NavMeshAgent>();
        deathScreen.enabled = false;

        timeBetweenSound = UnityEngine.Random.Range(10, 20);
        
    }

    // Update is called once per frame
    void Update()
    {
        soundTimer += Time.deltaTime;
        if (soundTimer >= timeBetweenSound)
        {
            PlayRandomMonsterSound();
        }
        if (player == null)
        {
            time += Time.deltaTime;
            if (time > timeToSpawn){
                deathScreen.enabled = false;
                isDead = false;
            }
            player = GameObject.FindGameObjectWithTag("Player");
            lockerController = player.GetComponent<LockerController>();
        }
            
            if (!lockerController.getLocker() && player != null && lockerController != null){
                attackDistance = 1.5f;
                targ = player.transform;
                distance = Vector3.Distance(agent.transform.position, targ.position);
                if (distance < attackDistance || player.transform.position.y < -2)
                {
                    CharacterController cc = player.GetComponent<CharacterController>();
                    time = 0f;
                    cc.enabled = false;
                    player.transform.position = spawn.position;
                    cc.enabled = true;
                    agent.isStopped = true;
                    deathScreen.enabled = true;
                    if (!isDead)
                    {
                        FlashImage();
                        isDead = true;
                    }
                }
                else
                {
                    agent.isStopped = false;
                    agent.destination = targ.position;
                }
            }
            else
            {
                attackDistance = 100f;
                distance = Vector3.Distance(agent.transform.position, targ.position);
                if (distance < attackDistance)
                {
                    targ = idlePositions[UnityEngine.Random.Range(0, idlePositions.Length)];
                }
                agent.isStopped = false;
                agent.destination = targ.position;
            }
    }

    public void FlashImage()
    {
        if (flash != null)
        {
            StopCoroutine(flash);
        }

        flash = StartCoroutine(colorFlashSequence());
    }

    private IEnumerator colorFlashSequence()
    {
        canvasGroup.alpha = 1f;
        if (flashText != null) flashText.enabled = false;

        yield return new WaitForSeconds(0.1f);

        if (flashText != null) flashText.enabled = true;

        float elapsed = 0f;
        while (elapsed < 3)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / 1);
            yield return null;
        }

        canvasGroup.alpha = 0f;
    }

    void PlayRandomMonsterSound()
    {
        if (audioClips.Length > 0 && audioSource != null)
        {
            int randomIndex = UnityEngine.Random.Range(0, audioClips.Length);
            AudioClip chosenClip = audioClips[randomIndex];

            audioSource.PlayOneShot(chosenClip, 0.5f);

            soundTimer = 0f;
            timeBetweenSound = UnityEngine.Random.Range(10, 20);
        }
    }
}
