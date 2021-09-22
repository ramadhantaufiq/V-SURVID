using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJalan : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;
    public float moveSpeed = 6;
    public AudioClip WeaponBox, QuestionBox, Finish, kurangDarah, habisDarah;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //input
        float x = Input.GetAxisRaw("Horizontal") * moveSpeed;
        float y = Input.GetAxisRaw("Vertical") * moveSpeed;

        //moving
        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, rb.velocity.y, movePos.z);
        //Debug.Log(newMovePos);
        rb.velocity = newMovePos;

        if(newMovePos != Vector3.zero)
        {
            anim.SetFloat("moveSpeed", 1, 0.1f, Time.deltaTime);
        }
        else{
            anim.SetFloat("moveSpeed", 0, 0.1f, Time.deltaTime);
        }
        //anim.SetFloat("moveSpeed", Mathf.Abs(y + x), 0.1f, Time.deltaTime);
        

        

       // Bergerak();

    }

    /*
    void Bergerak()
    {
        float gerak = Input.GetAxis("Vertical");
        rb.velocity = Vector3.forward * gerak * moveSpeed;
        anim.SetFloat("moveSpeed", Mathf.Abs(gerak), 0.1f, Time.deltaTime);
    }
    */

    //player mati saat menyetuh virus 
    //masih error



    private void OnTriggerEnter(Collider trig)
    {
        Debug.Log(trig.gameObject.name);
        if (trig.gameObject.CompareTag("Pertanyaan"))
        {
            Destroy(trig.gameObject);
            GameSystem.instance.PanggilSoal(trig.gameObject.GetComponentInChildren<ID_Soal>().ID);
            AudioSource.PlayClipAtPoint(QuestionBox, transform.position);
        }

        if (trig.gameObject.CompareTag("virus"))
        {
           
            if (GameSystem.instance.Kekuatan)
            {
                if(GameSystem.instance.Nama_kekuatan == "masker")
                {

                }
                else
                {
                    Destroy(trig.gameObject);
                    GameSystem.instance.ResetPower();
                    AudioSource.PlayClipAtPoint(habisDarah, transform.position);
                   
                }
            }
            else
            {
                //Destroy(trig.gameObject);
                GameSystem.instance.Sys_Darah--;
                AudioSource.PlayClipAtPoint(kurangDarah, transform.position);
                
            }
            
        }


        if (trig.gameObject.CompareTag("box"))
        {
            Destroy(trig.gameObject);
            GameSystem.instance.Aktif_Power((Random.Range(0, 2)));
            AudioSource.PlayClipAtPoint(WeaponBox, transform.position);
            
        }

        if (trig.gameObject.CompareTag("Finish") )
        {
            

            if(GameSystem.instance.ID_Jumlah == GameSystem.instance.ID_Jumlah_Soal)
            {
                Destroy(trig.gameObject);
                GameSystem.instance.GameSelesai(false);
                AudioSource.PlayClipAtPoint(Finish, transform.position);
            }
            else
            {
                if (GameSystem.instance.Ui_Notifikasi_Belum_Selesai.active == false)
                {
                    GameSystem.instance.Ui_Notifikasi_Belum_Selesai.SetActive(true);
                }
            }
        }


    }


    private void OnTriggerExit(Collider trig)
    {
        if (trig.gameObject.CompareTag("Finish"))
        {
            if (GameSystem.instance.Ui_Notifikasi_Belum_Selesai.active == true)
            {
                GameSystem.instance.Ui_Notifikasi_Belum_Selesai.SetActive(false);
            }
        }
    }



    /*

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
    */


}
