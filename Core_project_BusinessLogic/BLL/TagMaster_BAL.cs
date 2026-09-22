using System.Data;
using System.Xml;
using Core_project_BusinessLogic.DAL;
using Core_project_BusinessLogic.Entity;
using Microsoft.Extensions.Configuration;

namespace Core_project_BusinessLogic;

public class TagMaster_BAL : TagMaster_DAL
{
    private readonly TagMaster_DAL _dal;
    public TagMaster_BAL(IConfiguration configuration)
  : base(configuration)
    {
        _dal = new TagMaster_DAL(configuration);

    }

    public List<TagMaster> GetAllTags_BAL(int status, int language_id, out List<Options_List> languages)
    {
        languages = [];
        List<TagMaster> list = new List<TagMaster>();
        DataSet ds = _dal.GetAll(status, language_id);
        if (ds.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                languages.Add(new Options_List
                {
                    id = Convert.ToInt32(row["ID"]),
                    title = row["name"] as string
                });
            }
        }
        if (ds.Tables[1].Rows.Count > 0)
        {
            foreach (DataRow row in ds.Tables[1].Rows)
            {
                list.Add(Map(row));
            }
        }

        return list;
    }

    public TagMaster GetTagById_BAL(int id)
    {
        TagMaster obj = new();
        DataTable dt = _dal.GetById(id);

        if (dt.Rows.Count > 0)
        {
            obj = Map(dt.Rows[0]);
        }

        return obj;
    }

    public int AddTag(TagMaster obj, int userid)
    {
        DataTable dt = _dal.Add(obj, userid);

        if (dt.Rows.Count > 0)//dt.Columns.Contains("NewID")
        {
            return Convert.ToInt32(dt.Rows[0]["NewID"]);
        }
        else
        {
            return 0;
        }

    }

    public void UpdateTag(TagMaster obj, int userid)
    {
        _dal.Update(obj, userid);
    }

    private TagMaster Map(DataRow row)
    {

        return new TagMaster
        {
            ID = Convert.ToInt32(row["ID"]),
            Tag_name = row["Tag_name"] as string,
            Language_Master_ID = row["Language_Master_ID"] as int?,
            Languauge_Name = row.Table.Columns.Contains("Language_Name") ? row["Language_Name"]?.ToString() : null,
            Status = row["Status"] as int?,
            Created_Date = row["Created_Date"] != DBNull.Value ? Convert.ToDateTime(row["Created_Date"]) : (DateTime?)null,
            Updated_Date = row["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(row["Updated_Date"]) : (DateTime?)null,
        };
    }

    public void DeactivateTag(int id, int userid)
    {
        _dal.DeactivateTag_DAL(id, userid);
    }
}