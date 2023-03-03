using Notepad.ViewModels.Pages;
using Notepad.Views.Pages;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object content;
        private ViewModelBase notepadView;
        private ViewModelBase fileListView;

        public MainWindowViewModel()
        {
            notepadView = new NotepadViewModel();
            fileListView = new FileListViewModel();
            Content = fileListView;
        }
        public object Content
        {
            get => content;
            set
            {
                this.RaiseAndSetIfChanged(ref content, value);
            }
        }
    }
}
