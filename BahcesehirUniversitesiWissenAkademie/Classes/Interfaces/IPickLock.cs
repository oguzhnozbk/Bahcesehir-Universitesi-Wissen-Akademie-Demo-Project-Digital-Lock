using System;
using System.Collections.Generic;
using System.Text;

namespace BahcesehirUniversitesiWissenAkademie.Interfaces
{
    public interface IPickLock
    {
        string Unlock(IDigitalLock digitalLock);
    }
}
