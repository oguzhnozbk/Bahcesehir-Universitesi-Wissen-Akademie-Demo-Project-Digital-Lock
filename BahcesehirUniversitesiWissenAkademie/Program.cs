using BahcesehirUniversitesiWissenAkademie.Classes;
using System;

namespace BahcesehirUniversitesiWissenAkademie
{
    class Program
    {
        static void Main(string[] args)
        {
            DigitalLock digitalLock = new DigitalLock();
            Maymuncuk maymuncuk = new Maymuncuk();
            int CipherLength = digitalLock.GetCipherLength(), numberOfDigits = maymuncuk.GetNumberOfDigits(digitalLock);
            Console.WriteLine(CipherLength + " basamaklı " + numberOfDigits + " farklı karakterli bir kilit ve random şifresi oluşturuldu.");
            Console.WriteLine(digitalLock.ReadAll() + " değeri kilit üzerinde görünmektedir.");
            Console.WriteLine("Şifre Çözme işlemi başlamıştır.");
            string sifre = maymuncuk.Unlock(digitalLock);
            if (sifre.Length == CipherLength)
            {
                Console.WriteLine("Şifre çözülmüştür. Şifreniz : " + sifre);
                Console.WriteLine("Lock methodu ile şifre değiştiriliyor. Yeni şifre Random olarak belirleniyor.");
                Random random = new Random();
                for (int i = 0; i < CipherLength; i++)
                {
                    digitalLock.Turn(TurnDirection.Backward, i, random.Next(0, numberOfDigits));
                    Console.WriteLine(i + ". basamak için belirlenen karakter : " + digitalLock.Read(i));
                }

                Console.WriteLine("Belirlenen şifre : " + digitalLock.ReadAll());
                if (digitalLock.Lock(false))
                {
                    Console.WriteLine("Şifre değiştirme işlemi başarılı bir şekilde gerçekleşmiştir.");
                    Console.WriteLine("Yeni oluşturulan şifreyi çözme işlemi başlamıştır.");
                    sifre = maymuncuk.Unlock(digitalLock);
                    if(sifre.Length == CipherLength)
                        Console.WriteLine("Şifre çözülmüştür. Şifreniz : " + sifre);
                    else
                        Console.WriteLine(sifre);
                }
                else
                    Console.WriteLine("Şifre değiştirme işlemi başarısız olmuştur.");
            }
            else
                Console.WriteLine(sifre);

        }
    }

    public enum TurnDirection
    {
        Forward,
        Backward
    }
}
