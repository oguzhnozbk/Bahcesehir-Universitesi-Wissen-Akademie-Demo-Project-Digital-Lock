using BahcesehirUniversitesiWissenAkademie.Loggers;
using System;
using System.Collections.Generic;

namespace BahcesehirUniversitesiWissenAkademie.Classes
{
    //dairesel liste sınıfı. verilen listeyi dailerel listeye çevirerek kullanımını sağlar.
    public class CircularList
    {
        private char[] _chars;
        private int _index = 0;
        private int _listSize;
        public CircularList(List<char> chars)
        {
            try
            {
                _listSize = chars.Count;
                _chars = new char[_listSize];
                for (int i = 0; i < _listSize; i++)
                {
                    _chars[i] = chars[i];
                }
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
            }
        }
        /// <summary>
        /// dairesel listeyi başlangıç değerine döndürür.
        /// </summary>
        /// <returns></returns>
        public bool ResetIndex()
        {
            try
            {
                _index = 0;
                return true;
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
                return false;
            }
        }
        /// <summary>
        /// girilen direction yön bilgisine ve adım sayısına göre liste içerisinde ileri veya geri gider.Değeri geri döndürür.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        public char Step(TurnDirection direction, int step = 1)
        {
            try
            {
                switch (direction)
                {
                    case TurnDirection.Forward:
                        _index = (_index + step) % _listSize;
                        break;
                    case TurnDirection.Backward:
                        _index = (_index - step);
                        while (_index < 0)
                        {
                            _index += _listSize;
                        }
                        break;
                }
                return _chars[_index];
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
                throw;
            }
        }
        /// <summary>
        /// verilen adım sayısı kadar listede ileri gider.Değeri geri döndürür.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public char Next(int step = 1)
        {
            try
            {
                _index = (_index + step) % _listSize;
                return _chars[_index];
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
                throw;
            }
        }
        /// <summary>
        /// verilen adım sayısı kadar listede geri gider. Değeri geri döndürür.
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public char Back(int step = 1)
        {
            try
            {
                _index = (_index - step);
                while (_index < 0)
                {
                    _index += _listSize;
                }
                return _chars[_index];
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
                throw;
            }
        }
        /// <summary>
        /// Bulunduğu adımdaki değeri döndürür.
        /// </summary>
        /// <returns></returns>
        public char GetChar()
        {
            try
            {
                return _chars[_index];
            }
            catch (Exception e)
            {
                FileLogger.Log(e.Message);
                throw;
            }
        }
    }
}
