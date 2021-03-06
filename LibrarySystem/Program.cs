﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using System.IO;
using Terminal;
namespace LibrarySystem
{
    class Program
    {
        private static Toplevel top;
        static void Main(string[] args)
        {
            Application.Init();
            top = Application.Top;
            Console.Title = "Kniznicny System";
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.White, Color.Black);
            Console.Clear();
            var win = new Window()
            {
                Title = "Library System",
                Width = Console.WindowWidth,
                Height = Console.WindowHeight,

            };


            OpenDialog opendialog = new OpenDialog("Open Bookfile", "Open Bookfile");

            void OnClickEvent()
            {
                Console.Clear();
                Button btnCLick = new Button(50, 19, "Read");
                opendialog.Add(btnCLick);
                btnCLick.Clicked += OpenDialog;
                Window temp = new Window();
                temp.Add(opendialog);
                top.Add(temp);

            }

            void OpenDialog()
            {
                Console.Clear();
                string[] vs = File.ReadAllLines(opendialog.FilePath.ToString());
                string[] readText = vs;
                string názov = readText[0];
                string autor = readText[1];
                string knihydokopy = readText[2];
                string pozkniny = readText[3];
                string knihykdisp = readText[4];
                string pocetstran = readText[5];
                string obsahh = readText[6];
                Application.Refresh();
                var preview = new Window()
                {
                    Title = názov,
                    Width = Console.WindowWidth,
                    Height = Console.WindowHeight,
                    Visible = true,

                };
               
                Button btnEdit = new Button(0, 27, "EDIT");
                btnEdit.Clicked += onEditCLick;
                Button btnDel = new Button(108, 27, "DELETE");
                btnDel.Clicked += onDelete;

              
                void onDelete()
                {

                    File.Delete(opendialog.FilePath.ToString());
                }

                string vystup = new String(obsahh.Select(ch => ch == 'ô' ? '\n' : ch).ToArray());
                preview.Add(
                    new Label(0, 0, "Nazov: " + názov),
                    new Label(0, 1, "Author: " + autor),
                    new Label(0, 2, "Knihy do kopy: " + knihydokopy),
                    new Label(0, 3, "Požičané kniny: " + pozkniny),
                    new Label(0, 4, "Knihy k dispozícii: " + knihykdisp),
                    new Label(0, 5, "Počet strán: " + pocetstran), btnEdit, btnDel,
                    new Label(0, 6, "Obsah: " + vystup)
                ); ;

                Console.Clear();
                top.Add(preview);
                preview.SetFocus();
            };
            Application.Refresh();

            TextField nazovSave = new TextField(8, 0, 43, "");
            TextField autorSave = new TextField(9, 1, 42, "");
            TextField dokopySave = new TextField(16, 2, 35, "");
            TextField pozicSave = new TextField(17, 3, 34, "");
            TextField kdispSave = new TextField(21, 4, 30, "");
            TextField pocetstranSave = new TextField(14, 5, 37, "");
            TextField obsah = new TextField(22, 6, 30, "");


            void onEditCLick()
            {
                Application.Refresh();

                var editWindow = new Window()
                {
                    Title = "EDIT BOOK",
                    Width = Console.WindowWidth,
                    Height = Console.WindowHeight,
                    Visible = true
                };
                string fileName = opendialog.FilePath.ToString();


                Application.Refresh();
                Console.Clear();
                Button btnOK = new Button(0, 7, "ULOZIT");
                btnOK.Clicked += onSave;
                editWindow.Add(


                    new Label(0, 0, "Nazov:"), nazovSave, autorSave, dokopySave, pozicSave, kdispSave, pocetstranSave,
                    new Label(0, 1, "Author:"),
                    new Label(0, 2, "Knihy do kopy:"),
                    new Label(0, 3, "Požičané kniny:"),
                    new Label(0, 4, "Knihy k dispozícii:"),
                    new Label(0, 5, "Počet strán:"), btnOK



                );

                Application.Refresh();
                top.Add(editWindow);
                editWindow.SetFocus();
                Application.Refresh();

            }


            void onCreateNewBook()
            {
                Console.Clear();
                var editWindow = new Window()
                {
                    Title = "NEW BOOK",
                    Width = Console.WindowWidth,
                    Height = Console.WindowHeight,
                    Visible = true
                };
                string fileName = nazovSave.Text.ToString() + ".txt";




                Button btnOK2 = new Button(0, 7, "ULOZIT");
                btnOK2.Clicked += onSave;
                editWindow.Add(

                    new Label(0, 0, "Nazov:"), nazovSave, autorSave, dokopySave, pozicSave, kdispSave, pocetstranSave,
                    new Label(0, 1, "Author:"),
                    new Label(0, 2, "Knihy do kopy:"),
                    new Label(0, 3, "Požičané kniny:"),
                    new Label(0, 4, "Knihy k dispozícii:"),
                    new Label(0, 5, "Počet strán:"), btnOK2


                );

                Application.Refresh();
                top.Add(editWindow);
                editWindow.SetFocus();
                Application.Refresh();

            }

            void onSave()
            {
                Console.Clear();
                Application.Refresh();
                string[] writeText = new string[6];
                writeText[0] = nazovSave.Text.ToString();
                writeText[1] = autorSave.Text.ToString();
                writeText[2] = dokopySave.Text.ToString();
                writeText[3] = pozicSave.Text.ToString();
                writeText[4] = kdispSave.Text.ToString();
                writeText[5] = pocetstranSave.Text.ToString();
                File.WriteAllLines(nazovSave.Text.ToString() + ".txt", writeText);
                Application.Refresh();

            }
            void OnReadBooks()
            {
                Console.Clear();
                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + "/Knihy";
                string[] files = Directory.GetFileSystemEntries(path, "*", SearchOption.AllDirectories);
                string text = "";
                for (int i = 0; i < files.Length; i++)
                {
                    if (Path.GetExtension(files[i]) == ".txt")
                    {
                        text += Path.GetFileName(files[i]) + "\n";
                    }
                }
                MessageBox.Query("KNIHY", text, "OK");
            }

            int consoleCenter = Console.WindowWidth / 2;
            Application.Refresh();
            var btnFindBook = new Button(Console.WindowWidth / 2 - 7, 13, "FIND BOOK");
            btnFindBook.Clicked += OnClickEvent;

            var btnNewBook = new Button(Console.WindowWidth / 2 - 7, 17, "NEW BOOK");
            btnNewBook.Clicked += onCreateNewBook;

            var btnviewbook = new Button(Console.WindowWidth / 2 - 7, 15, "VIEW BOOK");
            btnviewbook.Clicked += OnReadBooks;


            win.Add(btnNewBook);
            win.Add(btnFindBook);
            win.Add(btnviewbook);
            win.Add(
                new Label(consoleCenter - 37, 1, @"  _      _ _                             _____           _                 "),
                new Label(consoleCenter - 37, 2, @" | |    (_) |                           / ____|         | |                "),
                new Label(consoleCenter - 37, 3, @" | |     _| |__  _ __ __ _ _ __ _   _  | (___  _   _ ___| |_ ___ _ __ ___  "),
                new Label(consoleCenter - 37, 4, @" | |    | | '_ \| '__/ _` | '__| | | |  \___ \| | | / __| __/ _ \ '_ ` _ \ "),
                new Label(consoleCenter - 37, 5, @" | |____| | |_) | | | (_| | |  | |_| |  ____) | |_| \__ \ ||  __/ | | | | |"),
                new Label(consoleCenter - 37, 6, @" |______|_|_.__/|_|  \__,_|_|   \__, | |_____/ \__, |___/\__\___|_| |_| |_|"),
                new Label(consoleCenter - 37, 7, @"                                 __/ |          __/ |                      "),
                new Label(consoleCenter - 37, 8, @"                                |___/          |___/                       ")

                );
            Application.Refresh();
            top.Add(win);
            Application.Refresh();
            /*
               *var menuBar = new MenuBar(new MenuBarItem[] {
                new MenuBarItem("File", new MenuItem[] {
                    new MenuItem ("SKOLA", "", () => Application.Run (opendialog)),
                    new MenuItem ("EPIKA", "", () => Application.Run(savedialog)),
                    new MenuItem ("POEZIA", "", () => Application.Run (opendialog)),
                    new MenuItem ("ROMAN", "", () => Application.Run(savedialog)),
                    new MenuItem ("CREDITS", "", () => {

                    var a = MessageBox.Query("CREDITS", "Make Lukáš and David");


                    }),
                    new MenuBarItem("Settings",new MenuItem[] {


                    }),
                    new MenuBarItem("About", new MenuItem[] {



                    }),
                }),

            }
            );
            top.Add(menuBar);
            */
            Application.Run();



        }
    }
}
