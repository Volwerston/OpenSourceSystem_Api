using OpenSourceSystem_Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OpenSourceSystem_Api.Controllers
{
    [Route("api/Note")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class NoteController : ApiController
    {
        public async Task<IHttpActionResult> Post([FromBody] NoteSearchOptions options)
        {
            using (SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Select * from Notes", con))
                {
                    await con.OpenAsync();

                    using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                    {
                        List<Note> toReturn = new List<Note>();

                        while (rdr.Read())
                        {
                            toReturn.Add(new Note()
                            {
                                Id = Convert.ToInt32(rdr["Id"].ToString()),
                                AddingDate = Convert.ToDateTime(rdr["AddingDate"].ToString()),
                                Text = rdr["Text"].ToString(),
                                Title = rdr["Title"].ToString(),
                                Username = rdr["Username"].ToString()       
                            });
                        }

                        return Ok(toReturn);
                    }
                }
            }
        }

        public async Task<IHttpActionResult> Put([FromBody] Note toAdd)
        {
            using(SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string cmdString = "Insert into Notes VALUES(@ti,@te,@at,@un)";

                using (SqlCommand cmd = new SqlCommand(cmdString, con))
                {

                    cmd.Parameters.AddWithValue("@ti", toAdd.Title);
                    cmd.Parameters.AddWithValue("@te", toAdd.Text);
                    cmd.Parameters.AddWithValue("@at", toAdd.AddingDate);
                    cmd.Parameters.AddWithValue("@un", toAdd.Username);

                    await con.OpenAsync();

                    await cmd.ExecuteNonQueryAsync();

                    return Ok();
                }
            }
        }
        
        public IHttpActionResult Get()
        {
            return Ok("Hello world");
        }
    }
}
