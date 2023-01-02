using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// FlyBehaviour inherits from GenericBehaviour. This class corresponds to the flying behaviour.
public class PickBehaviour : GenericBehaviour
{
    public string pickButton = "Pick";              // button fly mặc định.

    private int pickBool;                          // Animator variable related to flying.
                                                   //private bool pick = false;                     // Boolean to determine whether or not the player activated fly mode.
    public bool pick = false;                     // Boolean to determine whether or not the player activated fly mode.
    private CapsuleCollider col;                  // tham chiếu đến capsulle collider của player.
    private int groundedBool;

    //1
    [SerializeField] protected GameObject DoorObject;
    public GameObject textPickKey;
    private bool isKey;
    private bool isDoor;
    [SerializeField] protected GameObject textInfo;
    [SerializeField] protected Button buttonYes;
    [SerializeField] protected Button buttonNo;
    [SerializeField] protected GameObject key;

    //2
    private int countDiamond;
    public TextMeshProUGUI countDiamondText;

    private float timeDuration = 3000f;

    [SerializeField]
    private bool countDown = true;

    private float timer;

    [SerializeField]
    private TextMeshProUGUI firstMinute;
    [SerializeField]
    private TextMeshProUGUI secondMinute;
    [SerializeField]
    private TextMeshProUGUI separator;
    [SerializeField]
    private TextMeshProUGUI firstSecond;
    [SerializeField]
    private TextMeshProUGUI secondSecond;

    private float flashTimer;
    private float flashDuration = 1f;

    // Start is always called after any Awake functions.
    void Start()
    {
        //Khởi tạo tham chiếu
        pickBool = Animator.StringToHash("Pick");
        col = this.GetComponent<CapsuleCollider>();
        // theo dõi behaviour này trên manager.
        behaviourManager.SubscribeBehaviour(this);
        groundedBool = Animator.StringToHash("Grounded");
        behaviourManager.GetAnim.SetBool(groundedBool, true);

        //1
        isKey = false;
        isDoor = false;
        textPickKey.SetActive(false);
        textInfo.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);

        //2
        countDiamond = 0;
        SetCountDiamondText();

        //3
        ResetTimer();

    }

    void SetCountDiamondText()
    {
        countDiamondText.text = "   " + countDiamond.ToString();
    }

    private void ResetTimer()
    {
        if (countDown)
        {
            timer = timeDuration;
        }
        else
        {
            timer = 0f;
        }
        SetTextDisplay(true);
    }

    private void UpdateTimerDisplay(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);

        string currentTime = string.Format("{00:00}{1:00}", minutes, seconds);
        firstMinute.text = currentTime[0].ToString();
        secondMinute.text = currentTime[1].ToString();
        firstSecond.text = currentTime[2].ToString();
        secondSecond.text = currentTime[3].ToString();
    }

    private void Flash()
    {
        if (countDown && timer != 0)
        {
            timer = 0;
            UpdateTimerDisplay(timer);
        }

        if (!countDown && timer != timeDuration)
        {
            timer = timeDuration;
            UpdateTimerDisplay(timer);
        }
        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >= flashDuration)
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(false);
        }
        else
        {
            flashTimer -= Time.deltaTime;
            SetTextDisplay(true);
        }
    }

    private void SetTextDisplay(bool enable)
    {
        firstMinute.enabled = enable;
        secondMinute.enabled = enable;
        separator.enabled = enable;
        firstSecond.enabled = enable;
        secondSecond.enabled = enable;
    }
    //1
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("isKey") && pick)
        {
            Debug.Log("Starting see key:    " + Time.time);
            isKey = true;
            textPickKey.SetActive(true);
            textInfo.SetActive(false);
            buttonYes.gameObject.SetActive(false);
            buttonNo.gameObject.SetActive(false);
            StartCoroutine(AfterSeeKey(3.0f));
        }
    }
    //    if (other.gameObject.CompareTag("isNPC"))
    //    {
    //        textInfo.SetActive(true);
    //        buttonYes.gameObject.SetActive(true);
    //        buttonNo.gameObject.SetActive(true);
    //        Debug.Log("Starting meet NPC:    " + Time.time);
    //        StartCoroutine(AfterMeetNPC(5.0f));
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("isNPC"))
        {
            textInfo.SetActive(true);
            buttonYes.gameObject.SetActive(true);
            buttonNo.gameObject.SetActive(true);
            Debug.Log("Starting meet NPC:    " + Time.time);
            StartCoroutine(AfterMeetNPC(5.0f));
        }

        if (other.gameObject.CompareTag("isDiamond"))
        {

            Debug.Log("Starting see diamond:    " + Time.time);
            StartCoroutine(AfterSeeDiamond(3.0f));
        }    
        
        IEnumerator AfterSeeDiamond(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            other.gameObject.SetActive(false);
            countDiamond = countDiamond + 216;
            SetCountDiamondText();
            Debug.Log("Done " + Time.time);
        }
    }

    IEnumerator AfterMeetNPC(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        textInfo.SetActive(false);
        buttonYes.gameObject.SetActive(false);
        buttonNo.gameObject.SetActive(false);
        Debug.Log("Done " + Time.time);

    }
    IEnumerator AfterSeeKey(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        textPickKey.SetActive(false);
        key.gameObject.SetActive(false);
        Debug.Log("Done " + Time.time);
    }



    //1
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            isDoor = true;
        }
    }
    // Cập nhật được sử dụng để đặt các tính năng bất kể behavour đang hoạt động..
    void Update()
    {
        //2
        if (countDown && timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else if (!countDown && timer < timeDuration)
        {
            timer += Time.deltaTime;
            UpdateTimerDisplay(timer);
        }
        else
        {
            Flash();
        }

        //1
        if (isKey && isDoor)
        {
            DoorObject.transform.Translate(new Vector3(-0.7f, 0.0f, -1.2f));

            DoorObject.transform.Rotate(0, -124.879f, 0);

            isKey = false;
            isDoor = false;
        }

        // chuyển đổi bay theo input, chỉ khi có trạng thái ghi đè hoặc chuyển đổi tạm thời.
        if (Input.GetButtonDown(pickButton) && !behaviourManager.IsOverriding()
            && !behaviourManager.GetTempLockStatus(behaviourManager.GetDefaultBehaviour))
        {
            pick = !pick;

            // Buộc kết thúc quá trình chuyển đổi bước nhảy.
            behaviourManager.UnlockTempBehaviour(behaviourManager.GetDefaultBehaviour);

            //Tuân theo trọng lực. Đó là luật
            //behaviourManager.GetRigidBody.useGravity = !pick;

            //Player đang bay
            if (pick)
            {
                // Đăng kí behaviour này
                behaviourManager.RegisterBehaviour(this.behaviourCode);
            }
            else
            {
                // Set hướng va chạm/collider thanh dọc.
                col.direction = 1;
                //Set độ lệnh mặc định của camera
                behaviourManager.GetCamScript.ResetTargetOffsets();

                //Hủy đăng kí behavior này và đặt hành vi hiện tại thành behaviour mặc định
                behaviourManager.UnregisterBehaviour(this.behaviourCode);
            }
        }

        // Khẳng định đây là behaviour hoạt động 
        // Assert this is the active behaviour
        pick = pick && behaviourManager.IsCurrentBehaviour(this.behaviourCode);

        // Set biến fly liên quan trên Animator Controller.
        behaviourManager.GetAnim.SetBool(pickBool, pick);
    }

    // Hàm này được gọi khi một behavior khác ghi đè lên behavior hiện tại
    public override void OnOverride()
    {
        // Đảm bảo collider sẽ trở lại vị trí thẳng đứng khi behaviour bị ghi đè
        col.direction = 1;
    }

}
