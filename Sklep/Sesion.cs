using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sklep.entity;
using Sklep.Entity;

namespace Sklep
{
    class Sesion
    {
        public static User sesion = null;       
        public Sesion()
        {

        }
        public int StartSession(User u)
        {
            sesion = u;
            return 1;
        }
        public int EndSession() 
        {
            sesion = null;
            return 1;
        }
        
    }
}
