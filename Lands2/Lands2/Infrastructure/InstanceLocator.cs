using System;
using System.Collections.Generic;
using System.Text;
//Esta clase nos va a serrvir para encontrar a la MainViewModel
//Esto nos sirve para ligar la pagina Login a la MainViewModel
namespace Lands2.Infrastructure
{
    using ViewModels;
    class InstanceLocator
    {
        #region Properties
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion
        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
