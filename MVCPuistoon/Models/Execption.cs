using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCPuistoon.Models
{
    public class Execption
    {
      
            void show()
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception e)
                {
                    // code to handle the exception
                }
            }
        internal void Delete(Tapahtumat tapahtumat)
        {
            throw new Exception();
        }

        internal void Save()
        {
            throw new NotImplementedException();
        }

    }
}
