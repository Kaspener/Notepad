using Avalonia.Input;
using Avalonia.Interactivity;
using Notepad.Models;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;

namespace Notepad.ViewModels.Pages
{
    public class FileListViewModel : ViewModelBase
    {
        private string path = Directory.GetCurrentDirectory();
        private ObservableCollection<FileVisual> _directories;
        private DirectoryInfo dr;
        private FileVisual file;
        private string textBoxText;


        public FileListViewModel()
        {
            DirectoriesAndFiles();

            OpenCommand = ReactiveCommand.Create(() =>
            { 
                if (!file.IsFile)
                {
                    if (file.Image == "Assets/up.png")
                    {
                        if (Directory.GetParent(path) is not null)
                        {
                            path = Directory.GetParent(path).FullName;
                            DirectoriesAndFiles();
                        }

                    }
                    else
                    {
                        path += @"\"+file.Name;
                        DirectoriesAndFiles();
                    }
                } 
            });
        }

        private void DirectoriesAndFiles()
        {
            dr = new DirectoryInfo(path);
            Directories = new ObservableCollection<FileVisual>();
            if (Directory.GetParent(path) is not null) Directories.Add(new FileVisual { Name = "..", IsFile = false, Image = "Assets/up.png" });
            foreach (var d in dr.GetDirectories())
            {
                Directories.Add(new FileVisual { Name = d.Name, IsFile = false, Image = "Assets/directory.png" });
            }
            foreach (var d in dr.GetFiles())
            {
                Directories.Add(new FileVisual { Name = d.Name, IsFile = true, Image = "Assets/file.png" }); ;
            }
        }

        public ObservableCollection<FileVisual> Directories
        {
            get => _directories;
            set
            {
                this.RaiseAndSetIfChanged(ref _directories, value);
            }
        }


        public FileVisual File
        {
            get => file;
            set 
            { 
                this.RaiseAndSetIfChanged(ref file, value);
                if (file is not null) TextBoxText = file.IsFile == true ? file.Name : "";
            }
        }

        public string TextBoxText
        {
            get => textBoxText;
            set => this.RaiseAndSetIfChanged(ref textBoxText, value);
        }

        ReactiveCommand<Unit, Unit> OpenCommand { get; }
    }
}
