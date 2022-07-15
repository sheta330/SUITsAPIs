using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Helper
{
    public class imageprocess
    {
        public string DbPath(IFormFile file, string type, string newname)
        {
            try
            {
                var foldername = Path.Combine("Image", type);
                var PathToSave = Path.Combine(Directory.GetCurrentDirectory(), foldername);
                if (file.Length > 0)
                {
                    var fullpath = Path.Combine(PathToSave, newname);
                    var dbpath = Path.Combine(foldername, newname);
                    using (var stream = new FileStream(fullpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return (dbpath);
                }
                else
                {
                    return ("Select Image");
                }
            }
            catch (Exception ex)
            {
                return ("some thing wroung" + ex.Message);
            }
        }

        public bool delete(string filePath)
        {
            try
            {
                File.Delete(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
