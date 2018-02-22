using DBEntityModel.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAppHelper.Library.AppLogic
{
  public class TopicLogic
  {
    public List<string> lisTopic(string MatterName, int userId)
    {
      List<string> topicList = new List<string>();
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        topicList = (from topic in conect.Topic
                     join mat in conect.Matter on topic.MatterId equals mat.MatterId
                     where mat.MatterName == MatterName && topic.UserId == userId
                     select topic.TopicName
                     ).ToList();
      }
      return topicList;
    }
  }
}
