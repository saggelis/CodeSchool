using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Flipween.Controllers
{
    [EnableCors(origins: "*" , headers: "*", methods: "*")]
    public class UploadController : BaseController
    {

        //[Route("user/PostUserImage")]

        //public async Task<HttpResponseMessage> PostUserImage()
        //{
        //    Dictionary<string, object> dict = new Dictionary<string, object>();
        //    string filePath =string.Empty;
        //    try
        //    {

        //        var httpRequest = HttpContext.Current.Request;

        //        foreach (string file in httpRequest.Files)
        //        {
        //            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

        //            var postedFile = httpRequest.Files[file];
        //            if (postedFile != null && postedFile.ContentLength > 0)
        //            {

        //                int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB

        //                IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
        //                var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
        //                var extension = ext.ToLower();
        //                if (!AllowedFileExtensions.Contains(extension))
        //                {

        //                    var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else if (postedFile.ContentLength > MaxContentLength)
        //                {

        //                    var message = string.Format("Please Upload a file upto 1 mb.");

        //                    dict.Add("error", message);
        //                    return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
        //                }
        //                else
        //                {

        //                    //YourModelProperty.imageurl = userInfo.email_id + extension;
        //                    //  where you want to attach your imageurl

        //                    //if needed write the code to update the table

        //                    filePath = HttpContext.Current.Server.MapPath("~/Upload/" + Guid.NewGuid() + extension);
        //                    //Userimage myfolder name where i want to save my image
        //                    postedFile.SaveAs(filePath);

        //                }
        //            }

        //            //var message1 = string.Format("Image Updated Successfully.");
        //            return Request.CreateResponse(HttpStatusCode.Created, filePath); ;
        //        }
        //        var res = string.Format("Please Upload a image.");
        //        dict.Add("error", res);
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, dict);
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = string.Format("some Message");
        //        dict.Add("error", res);
        //        return Request.CreateResponse(HttpStatusCode.NotFound, dict);
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [Route("api/upload/uploadfile")]
        public async Task<HttpResponseMessage> UploadFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            string filePath = string.Empty;


            string root = HttpContext.Current.Server.MapPath("~/Uploads");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {
                    File.Move(file.LocalFileName, file.LocalFileName + ".jpg");
                    FileInfo fileInfo = new FileInfo(file.LocalFileName + ".jpg");
                    filePath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath + "Uploads/" + Path.GetFileName(fileInfo.FullName);
                   
                  
                }
                return Request.CreateResponse(HttpStatusCode.OK, filePath);
            }
            catch (System.Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }

}
