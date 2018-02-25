using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBEntityModel.DBModel;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.ModelBindings.Models.General;
using System.Data.Entity.SqlServer;

namespace StudentAppHelper.Library.AppLogic
{
  public class ContentLogic
  {
    QueryLogic materias = new QueryLogic();
    public bool crearContenido(ContentModel content, int userId)
    {
      bool resp = false;
      int materid = getMaterIDbyIndex(content.MatterIndex, userId).Value;
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        var contenido = new Content();
        contenido.ContentTypeId = (int)content.ContentTypeId;
        contenido.ContentText = content.ContentText;
        contenido.CreateDate = DateTime.Now;
        contenido.FileUrl = string.Empty;
        contenido.ContentUserId = userId;
        conect.Content.Add(contenido);
        conect.SaveChanges();

        var tag = new Tag();
        tag.ContentId = contenido.ContentId;
        tag.MatterId = materid;
        tag.TopicId = getTipocIDbyIndex(content.TopicIndex, userId, materid).Value;
        tag.TagType = (int)content.ContentTypeId;
        conect.Tag.Add(tag);
        conect.SaveChanges();
        resp = tag.TagId != 0;
      }
      return resp;
    }

    private int? getMaterIDbyIndex(int matterIndex, int userId)
    {
      string matterName = materias.Consultar_Materia(userId)[matterIndex];
      int matterId = 0;
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        matterId = (from mat in conect.Matter
                    where mat.MatterName == matterName && mat.UserId == userId
                    select mat.MatterId).FirstOrDefault();
      }
      return matterId;
    }

    public List<SearchResult> buscar(string filtro)
    {
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        return (from content in conect.Content
                join tag in conect.Tag on content.ContentId equals tag.ContentId
                where content.ContentText.Contains(filtro) || tag.Matter.MatterName.Contains(filtro) || tag.Topic.TopicName.Contains(filtro)
                || tag.Matter.MaterCode.Contains(filtro) || tag.Topic.TopicCode.Contains(filtro)
                select new SearchResult()
                {
                  Icon = tag.TagType == 1 ? "Texto.png" : tag.TagType == 2 ? "Imagen.png" : "Audio.png",
                  SearchName = content.ContentText,
                  IdContent = content.ContentId.ToString()
                }
          ).Distinct().ToList();
      }
    }

    private int? getTipocIDbyIndex(int TopicIndex, int userId, int matterid)
    {
      int TopicId = 0;
      using (StudenAppHelperDBEntities conect = new StudenAppHelperDBEntities())
      {
        TopicId = (from top in conect.Topic
                   where top.MatterId == matterid && top.UserId == userId
                   select top.TopicId).ToList()[TopicIndex];
      }
      return TopicId;
    }
  }
}
