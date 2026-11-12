using System;
using System.Collections.Generic;
using System.Text;

namespace MuholdApp
{
    public class Muhold
    {
        public string Azonosito { get; set; }
        protected int sulyKg;
        public int SulyKg{
            get { return sulyKg; }
            set
            {
                if (value >= 0)
                {
                    sulyKg = 100;
                }
                else
                {
                    sulyKg = value;
                }
            }
        }
        private static int osszesMuholdSzama = 0;
        public static int OsszesMuholdSzama
        {
            get { return osszesMuholdSzama; }
        }
        public Muhold(string azon, int suly)
        {
            Azonosito = azon;
            SulyKg = suly;
            osszesMuholdSzama++;
        }
        public virtual int NapiKoltseg()
        {
            return sulyKg*5;
        }
        public override string ToString()
        {
            return $"Műhold {Azonosito} | Tömeg: {sulyKg} | Napi költség: {NapiKoltseg()} EUR";
        }
    }
}
