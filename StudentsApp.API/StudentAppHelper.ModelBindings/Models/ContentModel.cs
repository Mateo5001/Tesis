using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAppHelper.ModelBindings.Models.General;

namespace StudentAppHelper.ModelBindings.Models
{
   public class ContentModel
  {
    public TipoContenido ContentTypeId;
    public string ContentText;
    public string FileUrl;
    public int MatterIndex;
    public int TopicIndex;
  }
}
