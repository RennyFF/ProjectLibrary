using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectLibrary.MVVM.ViewModel.CoreVMs
{
    class DialogWindowViewModel : BaseViewModel
    {
        #region Values
        private readonly Window WindowContext;
        private string headerMessage;
        public string HeaderMessage
        {
            get => headerMessage;
            set
            {
                headerMessage = value;
                onPropertyChanged();
            }
        }
        private string mainMessage;
        public string MainMessage
        {
            get => mainMessage;
            set
            {
                mainMessage = value;
                onPropertyChanged(nameof(MainMessage));
            }
        }
        #endregion
        #region Commands
        private RelayCommand closeCommand { get; set; }
        public RelayCommand CloseCommand
        {
            get
            {
                return closeCommand ??= new RelayCommand(obj =>
                {
                    WindowContext.Close();
                }, obj => true);
            }
        }
        #endregion

        public DialogWindowViewModel(Window Window, string HeaderMessage, string MainMessage)
        {
            WindowContext = Window;
            this.HeaderMessage = HeaderMessage;
            this.MainMessage = MainMessage;
        }
    }
}
