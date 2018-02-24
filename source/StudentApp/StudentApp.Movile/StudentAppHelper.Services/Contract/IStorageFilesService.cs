using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Services.Contract
{
  public interface IStorageFilesService
  {
    void guardar(string filename, string content);
    
  }
}
