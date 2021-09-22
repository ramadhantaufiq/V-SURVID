using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fungsi_tombol : MonoBehaviour
{
    public int ID_Tombol;
    public void btn_tombol()
    {
        GetComponent<Animation>().Play("button");
        if (GameSystem.instance.Data_Soal[GameSystem.instance.ID_Soal].Soal_JawabanA)
        {
            if(ID_Tombol == 0)
            {
                // jawaban benar
                GameSystem.instance.CekJawban(true);
            }
            else
            {
                // jawbaan salah
                GameSystem.instance.CekJawban(false);
            }
        }
        else
        {
            if (ID_Tombol == 0)
            {
                // jawaban salah
                GameSystem.instance.CekJawban(false);
            }
            else
            {
                // jawbaan benar
                GameSystem.instance.CekJawban(true);
            }
        }
    }
}
