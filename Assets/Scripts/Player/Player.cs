using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public ParticleSystem fire, water, earth, air;
    public StaffController staff;
    public Transform spawnPoint;
    public InputField inputField;
    public GameObject gameOverCanvas;
    private int coins;
    public Text coinsText;
    public SkinnedMeshRenderer mr;
    public Material normalMat;
    public Material transparentMat;
    private Rigidbody rb;
    private CapsuleCollider playerCollider;
    public GameObject inputParticles;
    private RigBuilder rig;
    public AudioSource leftFoot, rightFoot;
    public AudioClip wallHit;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        rig = GetComponent<RigBuilder>();
        
        GetComponent<Animator>().Play("Run");
        ToggleFootSteps();

        inputField.ActivateInputField();
        coins = 0;
    }

    private void Update()
    {
        if (Input.touchCount >= 1)
        {
            foreach (Touch touch in Input.touches)
            {
                int id = touch.fingerId;
                if (EventSystem.current.IsPointerOverGameObject(id) &&
                    EventSystem.current.currentSelectedGameObject.name.Contains("Input") && 
                    !inputField.isFocused)
                {
                    inputField.ActivateInputField();
                }

                else
                {
                    inputField.DeactivateInputField();
                }
            }

        }
    }

    public void InputChanged(string text)
    {
        if (text.ToLower() == "fire")
        {
            PlayParticle(fire);
        }

        else if (text.ToLower() == "water")
        {
            PlayParticle(water);
        }

        else if (text.ToLower() == "earth")
        {
            PlayParticle(earth);
        }

        else if (text.ToLower() == "air")
        {
            PlayParticle(air);
        }

        else if (text.Length >= 5)
        {
            inputField.text = "";
            inputField.ActivateInputField();
        }
    }


    protected void PlayParticle(ParticleSystem particle)
    {
        inputField.text = "";
        inputField.ActivateInputField();
        staff.ChangeWeight();
        particle.gameObject.SetActive(true);

        if (GameController.sfx && particle.gameObject.GetComponent<AudioSource>())
        {
            particle.gameObject.GetComponent<AudioSource>().Play();
        }

        inputParticles.SetActive(true);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Earth" ||
            collision.gameObject.tag == "Fire" ||
            collision.gameObject.tag == "Water")
        {
            GetComponent<Animator>().Play("Idle", 1);
            inputField.DeactivateInputField();
            rig.enabled = false;

            GetComponent<Animator>().Play(collision.gameObject.tag);
            PlatformController.platformSpeed = 0;
            InvokeRepeating("LowerPosition", 0, 0.04f);

            if (collision.gameObject.tag == "Fire")
            {
                transform.rotation = Quaternion.Euler(11.314f, 0f, 0f);
            }

            if (collision.gameObject.tag == "Earth")
            {
                if (GameController.sfx)
                {
                    GetComponent<AudioSource>().clip = wallHit;
                    GetComponent<AudioSource>().Play();
                }

                InvokeRepeating("GetBack", 0, 0.006f);
            }

            Invoke("GameOver", 2.5f);
        }
    }

    public void IncreaseCoins()
    {
        coins += 10;
        coinsText.text = coins.ToString();
    }

    public int GetCoins()
    {
        return coins;
    }

    public void ActivateCheat()
    {
        if (GameController.cheat == "Pass Through")
        {
            GameController.cheat = null;

            mr.material = transparentMat;
            //Color32 color = new Color32(255, 255, 255, 0);
            //mat.color = color;

            rb.constraints = RigidbodyConstraints.FreezePositionX | 
                             RigidbodyConstraints.FreezePositionY | 
                             RigidbodyConstraints.FreezeRotationZ;
            playerCollider.enabled = false;

            Invoke("ResetTransparency", 7f);
        }
        SaveManager.Instance.Save();
    }

    protected void ResetTransparency()
    {
        mr.material = normalMat;
        //Color32 color = new Color32(255, 255, 255, 255);
        //mat.color = color;

        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ;
        playerCollider.enabled = true;
    }

    protected void LowerPosition()
    {
        if (transform.position.y > -0.4f)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.02f, transform.localPosition.z);
        }
    }

    protected void GetBack()
    {
        if (transform.position.z > -14.888)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - .03f);
        }
    }

    protected void GameOver()
    {
        gameOverCanvas.SetActive(true);
    }

    public void PlayLeftFootStep()
    {
        leftFoot.Play();
    }
    
    public void PlayRightFootStep()
    {
        rightFoot.Play();
    }

    public void ToggleFootSteps()
    {
        if (GameController.sfx)
        {
            GetComponent<Animator>().Play("Footsteps", 1);
        }

        else
        {
            GetComponent<Animator>().Play("Idle", 1);
        }
    }
}
