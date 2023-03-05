using Notepad.ViewModels.Pages;
using Notepad.Views.Pages;
using ReactiveUI;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reflection.Metadata;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Notepad.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object content;
        private NotepadViewModel notepadView;
        private FileListOpenViewModel fileListOpenView;
        private FileListSaveViewModel fileListSaveView;

        public MainWindowViewModel()
        {
            notepadView = new NotepadViewModel();
            notepadView.Text = "";
            fileListOpenView = new FileListOpenViewModel();
            fileListSaveView = new FileListSaveViewModel();
            Content = notepadView;
            Observable.Merge(
                    notepadView.OpenCommand,
                    notepadView.SaveCommand)
                .Subscribe(flag =>
                {
                    if (flag == true)
                    {
                        { 
                            Content = fileListSaveView;
                            fileListSaveView.FileText = notepadView.Text;
                        }

                    }
                    else Content = fileListOpenView;

                });
            fileListOpenView.OpenCommand
                .Subscribe(str =>
                {
                    if (fileListOpenView.Status == true)
                    {
                        notepadView.Text = str;
                        fileListOpenView.Status = false;
                        Content = notepadView;
                    }
                });
            fileListOpenView.CancelCommand
                .Subscribe(unit =>
                {
                    Content = notepadView;
                });
            fileListSaveView.OpenCommand
                .Subscribe(str =>
                {
                    if (fileListSaveView.Status == true)
                    {
                        fileListOpenView.DirectoriesAndFiles();
                        fileListSaveView.DirectoriesAndFiles();
                        fileListSaveView.Status = false;
                        Content = notepadView;
                    }
                });
            fileListSaveView.CancelCommand
                .Subscribe(unit =>
                {
                    Content = notepadView;
                });
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
