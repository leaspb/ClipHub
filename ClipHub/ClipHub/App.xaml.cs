﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

using System;
using System.Resources;
using System.Text;
using Hardcodet.Wpf.TaskbarNotification;
using WpfKlip.Core;
using WpfKlip.Core.Win;
using WpfKlip.Core.Win.Enums;
using Db4objects.Db4o;
using Db4objects.Db4o.Query;
using Db4objects.Db4o.Linq;
using ClipboardPro.Code.Models;
using ClipboardPro.Code.Helpers;
using ClipboardPro.Code.DAO;

namespace ClipboardPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private TaskbarIcon tb;
        public static Boolean skipNextPaste = false;
        public static IClipboardRepository clipRepository;

        public App()
        {
            clipRepository = new ClipboardRepository();

            InitializeComponent();

            InitApplication();
        }

        private void InitApplication()
        {
            //initialize NotifyIcon

            tb = (TaskbarIcon)this.FindResource("TrayNotificationIcon");
            //tb = (TaskbarIcon)FindResource("TrayNotificationIcon");
            tb.Visibility = System.Windows.Visibility.Visible;

            registerHotKeys();
        }

        private void registerHotKeys()
        {
            //bind to clipboard 
            ShellHook sh;
            ClipboardHelper ch;
            GlobalHotkeyHelper gh;
            System.Windows.Forms.Form f;

            f = new System.Windows.Forms.Form();
            sh = new ShellHook(f.Handle);
            //sh.WindowActivated += sh_WindowActivated;
            //sh.RudeAppActivated += sh_WindowActivated;
            ch = new ClipboardHelper(f.Handle);
            ch.ClipboardGrabbed += ch_ClipboardGrabbed;
            ch.RegisterClipboardListener();

            gh = new GlobalHotkeyHelper(f.Handle);
            gh.GlobalHotkeyFired += new GlobalHotkeyHandler(gh_GlobalHotkeyFired);

            //gh.RegisterHotKey(72783, KeyModifiers.Control, VirtualKeys.VK_COMMA);
            //gh.RegisterHotKey(72784, KeyModifiers.Alt, VirtualKeys.VK_PERIOD);
            gh.RegisterHotKey(72784, KeyModifiers.Control, VirtualKeys.VK_2);
        }

        void ch_ClipboardGrabbed(System.Windows.Forms.IDataObject dataObject)
        {
            if (skipNextPaste)
            {
                skipNextPaste = false;
                return;
            }

            //using (IObjectContainer db = Db4oEmbedded.OpenFile("test"))
            //{
            //    List<ClipboardEntry> result = (from ClipboardEntry p in db
            //                                   select p).ToList();

            //    Console.WriteLine(result.Count);
            //}

            var formats = dataObject.GetFormats();

            if (formats.Contains(DataFormats.Text))
            {
                var text = Clipboard.GetText();

                //if (Settings.Default.OmitEmptyStrings && String.IsNullOrEmpty(text))
                //{
                //    return;
                //}

                //if (Settings.Default.OmitWhitespacesOnlyString && String.IsNullOrEmpty(text.Trim()))
                //{
                //    return;
                //}

                //if (CheckDuplicates(ItemsBox, (obj) => (text as string) == (obj as string)))
                //    return;
                var data = Clipboard.GetDataObject();
                //n = new TextDataLBI(Clipboard.GetDataObject());

                ClipboardEntry newClip = new ClipboardEntry();
                newClip.clipboardContents = text;
                newClip.dateClipped = DateTime.Now;

                // accessDb4o
                clipRepository.Save(newClip);
            }
            else if (formats.Contains(DataFormats.FileDrop))
            {
                //var files = dataObject.GetData(DataFormats.FileDrop) as string[];

                //if (CheckDuplicates(ItemsBox, (obj) =>
                //{
                //    string[] objasfiles = obj as string[];

                //    if (objasfiles != null && objasfiles.Length == files.Length)
                //    {

                //        // we need this since order may nto be preserved
                //        // e.g. when user selects file from l-to-r and 
                //        // and then r-to-l
                //        for (int i = 0; i < objasfiles.Length; i++)
                //        {
                //            if (!files.Contains(objasfiles[i]))
                //                return false;
                //        }

                //        return true;
                //    }
                //    return false;
                //}))
                //    return;

                //n = new FileDropsLBI(files);
            }
            else if (System.Windows.Clipboard.ContainsImage())
            {

                //System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                //System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)clipboardData.GetData(
                //   System.Windows.Forms.DataFormats.Bitmap);

                //if (CheckDuplicates(ItemsBox, (obj) =>
                //{
                //    var taghash = obj as ImageHash;
                //    if (taghash == null)
                //    {
                //        return false;
                //    }

                //    else return ImageHash.Compare(taghash, new ImageHash(bitmap)) == ImageHash.CompareResult.ciCompareOk;
                //}))
                //    return;

                //n = new ImageLBI(bitmap);
            }

            //if (n != null)
            //{
            //    ItemsBox.Items.Insert(0, n);
            //    //n.ItemClicked += new ItemCopiedEventHandler(n_ItemClicked);
            //}

            /* Console.WriteLine("\r\n\r\n");

             for (int i = 0; i < ItemsBox.Items.Count; i++)
             {
                 Console.WriteLine("{0}:{1}", i, (ItemsBox.Items[i] as ListBoxItem).Tag);
             }*/
        }

        void gh_GlobalHotkeyFired(int id)
        {
            if (id == 72783)
            {
                //IntPtr main = GetActiveWindowTitle();

                //Console.WriteLine("fired key" + DateTime.Now.ToString());
                ////mainWindow.ToogleVisibility();
                //skipNextPaste = false;
                //if (lastPasteLenght > 0)
                //{
                //    //send backspace till it's done
                //    for (int i = 0; i < lastPasteLenght; i++)
                //    {
                //        //System.Windows.Forms.SendKeys.SendWait("{BACKSPACE}");
                //        InputSimulator.SimulateKeyPress(VirtualKeyCode.BACK);

                //        Console.WriteLine("fired backspace key");
                //    }
                //}
                ////Clipboard.SetText(DateTime.Now.ToString(), TextDataFormat.Text);
                //Console.WriteLine("prepare sending");

                ////User32.SetActiveWindow(main);

                //InputSimulator.SimulateTextEntry(DateTime.Now.ToString());
                //Console.WriteLine("sent");
                //lastPasteLenght = DateTime.Now.ToString().Length;
                //return;
            }

            if (id == 72784)
            {
                //IntPtr main = GetActiveWindowTitle();

                Console.WriteLine("fired key" + DateTime.Now.ToString());

                SelectMenu select = new SelectMenu();

                select.Show();

                return;
            }

            //int itemindex = id - 667;

            //if (itemindex < mainWindow.ItemsBox.Items.Count)
            //{
            //    (mainWindow.ItemsBox.Items[itemindex] as DataEnabledListBoxItem).DoMouseCommand(MouseCommand.GetCommandForClick((ClickType)Settings.Default.ItemHotkeyActAs));
            // }

        }

        private IntPtr GetActiveWindowTitle()
        {
            //const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            //StringBuilder Buff = new StringBuilder(nChars);
            handle = User32.GetForegroundWindow();

            Console.WriteLine(this.GetProcessThreadFromWindow(handle));
            //if (User32.GetWindowText(handle, Buff, nChars) > 0)
            //{/
            //    return Buff.ToString();
            //}
            return handle;
        }



        private int GetProcessThreadFromWindow(IntPtr hwnd)
        {
            int procid = 0;
            int threadid = User32.GetWindowThreadProcessId(hwnd, ref procid);
            return procid;
        }
    }
}