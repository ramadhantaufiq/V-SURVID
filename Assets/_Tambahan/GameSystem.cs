using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class GameSystem : MonoBehaviour
{
    // Penjelasan Setiap fungsi ada di paling atas berurut barisnya
    // Fungsi static GameSystem instance (ada di Awake() ) fungsinya agar script bisa di akses dari semua script
    // ID_Soal sebagai id untuk memanggil soal yang keberapa
    // ID_Jumlah = jumlah Kotak yang di ambil saat ini , dan ID_Jumlah_Soal data jumlah target yang harus di capai
    // ID Game aktif = indikator game aktif apa tidak
    // id soal aktif = indikator soal aktif
    // id game selsai = indikator game selesai

    public static GameSystem instance;
    public int ID_Soal;
    public int ID_Jumlah = 99 , ID_Jumlah_Soal;
    public bool ID_GameAktif;
    public bool ID_SoalAktif;
    public bool ID_GameSelesai;





    // Player kamera adalah refrensi Script kamera yang ada di player
    // Player jalan ref dari PlayerJalan di player
    // Sys darah adalah banyaknya darah saat ini
    // kekuatan , indikator kekuatan aktif apa tidak
    // nama kekuatan saat ini
    // warna yang di gunakan untuk indikator UI darah
    [Header("System Permainan")]
    public Kamera Player_Kamera;
    public PlayerJalan Player_Jalan;
    public int Sys_Darah;
    public bool Kekuatan;
    public string Nama_kekuatan;
    public Color[] Sys_Warna;

    // ini adalah kumpulan kumpulan UI gameobject , refrensinya udah ada semua di inspector
    [Header("Canvas Permainan")]
    public GameObject Ui_Soal;
    public GameObject Ui_GameOver;
    public GameObject Ui_GameMenang;
    public GameObject Ui_Informasi;
    public GameObject Ui_Notifikasi_Benar,Ui_Notifikasi_Salah,Ui_Notifikasi_Belum_Selesai;
    public GameObject[] Ui_Darah;
    public GameObject[] Ui_PowerUp,Obj_PowerUp;

   
    // text tulisan ini Text untuk soal
    // Pilihan A dan Pilihan B
    // text saaat notifikasi salah
    // text box info box pertanyaan yang telah di dapat
    [Header("Setting Soal & Notifikasi")]
    public Text Text_Tulisan;
    public Text Text_PilihanA, Text_PilihanB;
    public Text Text_Notifikasi;
    public Text Text_Box;
    

    // ini adalah refrensi class soal
    // pertanyaan
    // Jawban A dan B di dalam aarray
    // Jawabannya apakah jawaban yang benar ada di A
    // Tulisan untuk notifikasi salah
    [System.Serializable]
    public class Soal
    {
        [TextArea()]
        public string Soal_Pertanyaan;
        public string[] Soal_Jawaban;
        public bool Soal_JawabanA;
        [TextArea()]
        public string Soal_NotifikasiSalah;

    }
    // ini manggil class soal 
    public Soal[] Data_Soal;
    

    // ini agar fungsi script bisa di akses dari script lain 
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update

    // reset data awal setiap permainan
    // id_jumlah_soal langsung otomatis mengambil data dari banyaknya object yang mempunyai tag Pertanyaan
    void Start()
    {
        Sys_Darah = 3;
        ID_Jumlah = 0;
        ID_Jumlah_Soal = GameObject.FindGameObjectsWithTag("Pertanyaan").Length;
        ID_GameAktif = true;
        ID_GameSelesai = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        Set_Darah(); // Seting darah
        Set_Text(); // set text

        // Jika game belum selsai dan darah habis maka panggil fungsi game seelsai
        if(!ID_GameSelesai && Sys_Darah <= 0)
        {
            ID_GameSelesai = true;
            GameSelesai(true);
        }
        //


        // START Pengaturan kursor // 
        // jika game aktif maka kursor terlihat , fungsi kamera aktif , fungsi jalan aktif
        if (ID_GameAktif)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Player_Kamera.enabled = true;
            Player_Jalan.enabled = true;

        }
            
        // Kebalikan dari fungsi atas
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            Player_Kamera.enabled = false;Player_Jalan.enabled = false;

        }

        // END Pengaturan kursor // 

        // Start Fungsi Tekan Keyboard saat soal aktif //

        // jika UI Soal aktif dan Game Tidak aktif dan ID Soal aktif maka fungsi sentuhnya
        // artinya jika ui soal aktif dan fungsi gerak mati dan soal sudah bisa di akses maka fungsi tekan keyboard baru aktif
        // jika Bool Soal_JawabanA = true , Tekan A Benar , Tekan B Salah
        // sebaliknya
        if (Ui_Soal.gameObject.active && !ID_GameAktif && ID_SoalAktif)
        {
            if (Data_Soal[ID_Soal].Soal_JawabanA)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    // benar
                    CekJawban(true);
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                    // salah
                    CekJawban(false);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    // salah
                    CekJawban(false);
                }

                if (Input.GetKeyDown(KeyCode.B))
                {
                        // benar
                    CekJawban(true);
                }
            }
        }


        // END Fungsi Tekan Keyboard saat soal aktif //
    }

    // Setting darah ada disini
    // disini menggunakan fungsi for
    // For sebanyak Ui darah 
    // jika nama darah kurang dari darah saat ini maka warna 0
    // sebaliknya maka warna 1 
    // cek warna di inspector
    public void Set_Darah()
    {
        for (int i = 0; i < Ui_Darah.Length; i++)
        {
            if(int.Parse(Ui_Darah[i].gameObject.name) <= Sys_Darah)
            {
                Ui_Darah[i].GetComponent<Image>().color = Sys_Warna[0];
            }
            else
            {
                Ui_Darah[i].GetComponent<Image>().color = Sys_Warna[1];
            }
        }
    }


    // set text untuk box
    public void Set_Text()
    {
        Text_Box.text = ": " + ID_Jumlah + "/" + ID_Jumlah_Soal;
    }


    // fungsi set soal berdasarkan idsoal dan memanggil ienumerator dengan Coroutine
    public void PanggilSoal(int idsoal)
    {
        ID_Soal = idsoal;
        StartCoroutine(Set_Soal());
    }

    // coroutine ini adalah fitur script yang bisa menggunakan WAIT 
    // artinya script bisa di jeda jeda pemanggilannya 
    // nah disini di set soal di gunakan untuk men Set tulisan tulisan text sesuai dengan 
    // data yang ada di Class Soal berdasarkan ID_Soal yand telah kita set di PanggilSoal(idsoal)
    // disini dapat di lihat text di sesuaikan lalu wait selama 1s dan IDSoalAktif = true
    public IEnumerator Set_Soal()
    {
        ID_GameAktif = false;
        ID_SoalAktif = false;
        Ui_Soal.SetActive(true);

        Text_Tulisan.text = Data_Soal[ID_Soal].Soal_Pertanyaan;
        Text_PilihanA.text = Data_Soal[ID_Soal].Soal_Jawaban[0];
        Text_PilihanB.text = Data_Soal[ID_Soal].Soal_Jawaban[1];

        Text_Notifikasi.text = Data_Soal[ID_Soal].Soal_NotifikasiSalah;

        yield return new WaitForSeconds(1f);
        ID_SoalAktif = true;

    }
 

    // Cek jawaban yang di gunakan di fungsi tombol keyboard di atas dan 
    // di fungsi tombol
    // apakah jawaban benar atau tidak
    public void CekJawban(bool jawaban)
    {
        StartCoroutine(UI_Notifikasi_Jawaban(jawaban));
        //UI_Notifikasi_Jawaban(true);
    }

    // sama seperti tadi disini juga fungsi yang ada wait waitnya
    //jika jawbaan benar maka muncul ui benar jika salah maka keluar ui salah
    //wait 2 detik
    // setelah itu jika benar maka UI langsung tertutup dan nambah data
    // jijka salah maka keluar UI notifikasi salah
    public IEnumerator UI_Notifikasi_Jawaban(bool benar)
    {
        ID_SoalAktif = false;
        yield return new WaitForSeconds(0);
        if (benar)
        {
            Ui_Notifikasi_Benar.SetActive(true);
        }
        else
        {
            Ui_Notifikasi_Salah.SetActive(true);
        }

        yield return new WaitForSeconds(2);

        Ui_Notifikasi_Salah.SetActive(false);
        Ui_Notifikasi_Benar.SetActive(false);
        Ui_Soal.SetActive(false);
        if (benar)
        {
            
            ID_GameAktif = true;
            ID_SoalAktif = false;
            ID_Jumlah++;
        }
        else
        {
            Ui_Informasi.SetActive(true);
        }

    }


    // fungsi yang di gunakan untuk lanjut di notifikasi salah

    public void btn_lanjut()
    {
        //Sys_Darah--;
        ID_Jumlah++;
        ID_GameAktif = true;
        ID_SoalAktif = false;
        Ui_Informasi.SetActive(false);
    }


    // fungsi mendapatkan power
    public void Aktif_Power(int id)
    {
        Kekuatan = true;
        if(id == 0)
        {
            Nama_kekuatan = "hand";
        }
        else
        {
            Nama_kekuatan = "masker";
        }
        Obj_PowerUp[id].SetActive(true);
        Ui_PowerUp[id].SetActive(true);

        // ini adalah fungsi untuk mengaktifkan fitur power menghilang
        // disini aku set 10detik , kalau mau ganti bisa dengan 20f untuk 20 detik ,
        StartCoroutine(DurasiPower(10f));
    }


    // fugnsi memanggil game selesai menang atau kalah
    public void GameSelesai(bool kalah)
    {

        ID_GameAktif = false;
        ID_GameSelesai = true;

        if (kalah)
        {
            Ui_GameOver.SetActive(true);
        }
        else
        {
            Ui_GameMenang.SetActive(true);
        }
    }

    // reset power , menghilangkan power yang ada
    public void ResetPower()
    {
        for (int i = 0; i < Ui_PowerUp.Length; i++)
        {
            Ui_PowerUp[i].SetActive(false);
            Obj_PowerUp[i].SetActive(false);
        }
        Kekuatan = false;
    }


    // fungsi wait untuk menghilangkan power
    public IEnumerator DurasiPower(float lama)
    {
        yield return new WaitForSeconds(lama);
        ResetPower();
    }


  

}
