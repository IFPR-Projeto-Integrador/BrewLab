using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewLab.Models.Base;
public interface IBrewLabModel<T>
{
    public T Id { get; set; }
}
