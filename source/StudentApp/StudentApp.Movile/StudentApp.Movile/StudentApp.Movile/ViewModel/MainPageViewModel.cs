﻿using StudenAppHelper.ResousrcesStrings;
using StudentApp.Movile.Util.CustomViewModel;
using StudentAppHelper.ModelBindings.Models;
using StudentAppHelper.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace StudentApp.Movile.ViewModel
{
  public class MainPageViewModel : CustomAppViewModel
  {

    #region Atributos privados
    private string _anotationText;
    private int _IndexMatter;
    private int _IndexTopic;
    private List<string> _MatterList;
    private List<string> _TopicList;
    #endregion

    #region Consturctores
    public MainPageViewModel() : base()
    {
      btnGuardar = btnGdef;
    }
    #endregion

    #region Propiedades 
    #endregion

    #region Encapsulamiento de atributos

    public string AnotationText { get => _anotationText; set { _anotationText = value; OnPropertyChanged(); } }
    public int IndexMatter { get => _IndexMatter; set { _IndexMatter = value; OnPropertyChanged(); } }
    public int IndexTopic { get => _IndexTopic; set { _IndexTopic = value; OnPropertyChanged(); } }
    public List<string> MatterList { get => _MatterList; set { _MatterList = value; OnPropertyChanged(); } }
    public List<string> TopicList { get => _TopicList; set { _TopicList = value; OnPropertyChanged(); } }

    #endregion

    #region Defeinicion de comandos
    public ICommand btnGuardar { get; protected set; }
    #endregion

    #region Definicion de IComand
    public Command btnGdef
    {
      get
      {
        return new Command(async () =>
        {
          await guardar();
        });
      }
    }

    #endregion

    #region eventos
    private async Task  guardar()
    {
      //_client.IsAuthenticated = true;
      //var result =  await _client.CallAsync<ObjectNull,string>("api/Account/holamundo",new ObjectNull());
      //if (result == null)
        _navigate.NavigateTo("Login");
      //Message = result;//result;

    }
    #endregion
  }
}
