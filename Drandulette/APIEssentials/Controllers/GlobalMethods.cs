﻿using Drandulette.Controllers.Data.Models;
using FileManager = System.IO.File;

namespace Drandulette.APIEssentials.Controllers
{
    public static class GlobalMethods
    {
        public static Announcement InsertPictures(Announcement x, int isSpecific)
        {
            var pics = Directory.EnumerateFiles(x.picsPath);

            if (isSpecific == 0)
            {
                x.pics = pics.Select(x => new StreamReader(x).ReadToEnd()).ToList();
                return x;
            }

            x.pics.Add(pics.First());
            return x;
        }

        public static Topic InsertPictures(Topic x)
        {
            x.user.profilePic = Convert.ToBase64String(FileManager.ReadAllBytes($".\\Users\\{x.mailLogin}\\imgnotfound.jpg"));
            return x;
        }

        public static bool Matches(string brand, string model, int year, Announcement x)
        {
            return x.brand.Contains(brand) || x.model.Contains(model) || x.year == year;
        }
    }
}