using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewLab.Common.DTOs;
public static class ResultDTO
{
    public class Auth
    {
        public bool Success { get; set; }
        public IEnumerable<(string Description, string Code)> Errors { get; set; } = [];
    }
}
