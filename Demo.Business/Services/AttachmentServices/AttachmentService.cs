﻿using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Services.AttachmentServices
{
    public class AttachmentService : IAttachmentService
    {
        private List<string> allowExtension = [".png", ".jpeg", ".jpg"];

   private   const  int MaxSize = 2_0970_152;
        public string? Upload(IFormFile file, string folderName)
        {
           //1-Check Extension
           if(!allowExtension.Contains( Path.GetExtension(file.FileName)))
                return null;
           //2-Check Size
           if(file.Length==0||file.Length>MaxSize)
                return null;
           //3-Get Located Folder Path
           var FolderPath=Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files",folderName);
           //4-Make Attachment Name Unique
           var fileName=$"{Guid.NewGuid()}_{file.FileName}";
           //5-Get FilePath
           var FilePath=Path.Combine(FolderPath,fileName);

            //6-Create  File Stream
            using FileStream Fs = new FileStream(FilePath, FileMode.Create);
           //7-Use Stream to copy file

            file.CopyTo(Fs);

            //8-Return File name

            return fileName;

        }



   
        public bool Delete(string folderPath)
        {
            if(!File.Exists(folderPath))
                return false;
            else        
            {
                File.Delete(folderPath);
                return true;
            }
        }

   
    }
}
