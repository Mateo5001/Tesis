using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using StudentAppHelper.Services.Contract;
using System.IO;
using StudentApp.Movile.Droid.Services;

[assembly: Xamarin.Forms.Dependency(typeof(StorageFilesService))]
namespace StudentApp.Movile.Droid.Services
{
  public class StorageFilesService : IStorageFilesService
  {
    public void guardar(string filename, string content)
    {
      var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
      var filePath = Path.Combine(documentsPath, filename);
      System.IO.File.WriteAllText(filePath, content);
    }
  }
}