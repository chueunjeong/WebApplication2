using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class EngineResponse
    {
        public long Id { get; set; }

        public string ipAddress { get; set; }

        public int portNum { get; set; }

        public bool status{ get; set; }
    }
}
