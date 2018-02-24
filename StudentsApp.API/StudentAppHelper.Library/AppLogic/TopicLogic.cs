using DBEntityModel.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentAppHelper.ModelBindings.Models;

namespace StudentAppHelper.Library.AppLogic
{
  public class TopicLogic
  {
    public List<string> lisTopic(string MatterName, int userId)
    {
      List<string> topicList = new List<string>();
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        int? matterId = getMaterIDbyName(MatterName, userId);
        topicList = (from topic in conect.Topic
                     where topic.UserId == userId && topic.MatterId == matterId
                     select topic.TopicName
                     ).ToList();
      }
      return topicList;
    }

    public bool createTopic(string matterName, int userId, TopicModel topic)   
    {
      bool resp = false;      
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())  
      {
        var newTopic = new Topic();   
        newTopic.UserId = userId;
        newTopic.MatterId = getMaterIDbyName(matterName, userId);
        newTopic.TopicName = topic.TopicName;
        newTopic.TopicCode = topic.TopicCode;
        newTopic.isActive = topic.isActive;
        newTopic.isEliminated = false;
        conect.Topic.Add(newTopic);
        conect.SaveChanges();
        if (newTopic.TopicId != 0)
        {
          resp = true;
        }
      }
      return resp;
    }

    private int? getMaterIDbyName(string matterName, int userId)
    {
      if(string.IsNullOrEmpty( matterName))
      {
        return 0;
      }
      int matterId = 0;
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        matterId = (from mat in conect.Matter
                        where mat.MatterName == matterName && mat.UserId == userId
                        select mat.MatterId).FirstOrDefault(); 
      }
      return matterId;
    }
  }
}
