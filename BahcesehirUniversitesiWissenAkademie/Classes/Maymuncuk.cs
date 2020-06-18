using BahcesehirUniversitesiWissenAkademie.Interfaces;
using BahcesehirUniversitesiWissenAkademie.Loggers;
using System;

namespace BahcesehirUniversitesiWissenAkademie.Classes
{
    public class Maymuncuk : IPickLock
    {
        public string Unlock(IDigitalLock digitalLock)
        {
            try
            {
                int cipherLength = digitalLock.GetCipherLength();
                int numberOfDigits = GetNumberOfDigits(digitalLock);

                int [] a = new int[cipherLength];
                for (int i = 0; i < cipherLength; i++)
                {
                    a[i] = 0;
                }
                do
                {
                    for (int j = 0; j < numberOfDigits; j++)
                    {
                        digitalLock.Lock(true);
                        if (!digitalLock.IsLocked())
                            return digitalLock.ReadAll();
                        digitalLock.Turn(TurnDirection.Forward, 0, 1);
                        a[0]++;
                    }
                    for (int i = 0; i < (cipherLength-1); i++)
                    {
                        if (a[i] == numberOfDigits)
                        {
                            a[i] = 0;
                            a[i + 1]++;
                            digitalLock.Turn(TurnDirection.Forward, i+1, 1);
                        }
                    }
                    if (a[cipherLength-1] == numberOfDigits)
                    {
                        break;
                    }
                } while (true);

                return "Şifre çözülemedi.";

            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- şifre çözme sırasında hata oluştu.");
                throw;
            }
        }

        public int GetNumberOfDigits(IDigitalLock digitalLock)
        {
            try
            {
                char firstValue = digitalLock.Read(0);
                int numberOfDigits = 0;
                bool isDifferent = true;
                do
                {
                    numberOfDigits++;
                    digitalLock.Turn(TurnDirection.Forward, 0, 1);
                    if (firstValue == digitalLock.Read(0))
                    {
                        isDifferent = false;
                    }
                } while (isDifferent);
                digitalLock.Reset();
                return numberOfDigits;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Basamak sayısı bulunma sırasında hata oluştu.");
                throw;
            }
        }
    }
}
