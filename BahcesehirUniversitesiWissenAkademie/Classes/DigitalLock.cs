using BahcesehirUniversitesiWissenAkademie.Interfaces;
using BahcesehirUniversitesiWissenAkademie.Loggers;
using System;
using System.Collections.Generic;

namespace BahcesehirUniversitesiWissenAkademie.Classes
{
    public class DigitalLock : IDigitalLock
    {
        private List<CircularList> TryPassword = new List<CircularList>();
        private char[] _cipher;
        private bool _locked = true;
        public DigitalLock()
        {
            SetRandomCipher();
        }
        private void SetRandomCipher()
        {
            try
            {
                Random random = new Random();
                List<List<char>> dataset = new List<List<char>>()
                {
                    new List<char>()
                    {
                        '0' ,'1' ,'2' ,'3' ,'4' ,'5' ,'6' ,'7' ,'8' ,'9'
                    },
                    new List<char>()
                    {
                        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
                    }
                };
                int selectDataset = random.Next(0, 2);
                int selectCipherLenth = random.Next(4, 7);
                _cipher = new char[selectCipherLenth];
                for (int i = 0; i < selectCipherLenth; i++)
                {
                    _cipher[i] = dataset[selectDataset][random.Next(0, dataset[selectDataset].Count)];
                    TryPassword.Add(new CircularList(dataset[selectDataset]));
                }
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Şifre oluşturulamadı.");
            }

        }
        public int GetCipherLength()
        {
            try
            {
                return _cipher.Length;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Şifre uzunluğu döndürülemedi.");
                throw;
            }
        }
        public bool Turn(TurnDirection direction, int circleIndex, int step)
        {
            #region Step 1
            //try
            //{
            //    if (direction == TurnDirection.Forward)
            //    {
            //        TryPassword[circleIndex].Next(step);
            //    }
            //    else
            //    {
            //        TryPassword[circleIndex].Back(step);
            //    }

            //    return true;
            //}
            //catch (Exception e)
            //{
            //    FileLogger.Log(e.Message + " -- " + (circleIndex + 1) + ". şifre hanesi değiştirilemedi.");
            //    return false;
            //}
            #endregion

            #region Step 2

            try
            {
                TryPassword[circleIndex].Step(direction, step);
                return true;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- " + (circleIndex + 1) + ". şifre hanesi değiştirilemedi.");
                return false;
            }
            #endregion
        }
        public string ReadAll()
        {
            try
            {
                string display = "";
                foreach (var p in TryPassword)
                {
                    display += p.GetChar();
                }
                return display;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Ekranda görünen şifre görüntülenemedi.");
                return "Display edilemedi.";
            }
        }

        public char Read(int circleIndex)
        {
            try
            {
                return TryPassword[circleIndex].GetChar();
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- " + (circleIndex + 1) + ". şifre alanı ekrana getirelemedi.");
                return '*';
            }
        }

        public bool Lock(bool garbleAfterLock)
        {
            try
            {
                if (!_locked)
                {
                    if (!garbleAfterLock)
                    {
                        for (int i = 0; i < _cipher.Length; i++)
                        {
                            _cipher[i] = TryPassword[i].GetChar();
                        }
                        Reset();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    bool isDifferent = false;
                    for (int i = 0; i < _cipher.Length; i++)
                    {
                        if (_cipher[i] != TryPassword[i].GetChar())
                        {
                            isDifferent = true;
                        }
                    }

                    if (isDifferent)
                    {
                        _locked = true;
                        return false;
                    }
                    else
                    {
                        _locked = false;
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Şifre yenileme veya şifre açma sırasında hata oluştu.");
                throw;
            }
        }

        public bool IsLocked()
        {
            try
            {
                return _locked;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Kilit durumu döndürülürken hata oluştu.");
                return true;
            }
        }

        public bool Reset()
        {
            try
            {
                foreach (var p in TryPassword)
                {
                    p.ResetIndex();
                }

                _locked = true;
                return true;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message + " -- Değerler sıfırlanırken hata oluştu.");
                throw;
            }
        }
    }
}
