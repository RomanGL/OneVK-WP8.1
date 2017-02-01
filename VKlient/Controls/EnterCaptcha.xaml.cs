﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Pickers.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу http://go.microsoft.com/fwlink/?LinkID=390556

namespace OneVK.Controls
{
    public partial class EnterCaptcha : ContentDialog, INotifyPropertyChanged
    {
        public EnterCaptcha(string captchaURL)
        {
            this.InitializeComponent();
            DataContext = this;
            CaptchaURL = captchaURL; 
        }

        private string _url;
        private string _captcha = "";

        public string CaptchaURL
        {
            get { return _url; }
            set
            {
                if (value != _url)
                {
                    _url = value;
                    OnPropertyChanged("CaptchaURL");
                }
            }
        }

        public string Captcha
        {
            get { return _captcha; }
            set
            {
                if (value != _captcha)
                {
                    _captcha = value;
                    OnPropertyChanged("Captcha");
                }
            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Возникает, когда значение свойства изменено.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Уведомляет клиентов об изменении свойства.
        /// </summary>
        /// <param name="propertyName">Имя свойства.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            { handler(this, new PropertyChangedEventArgs(propertyName)); }
        }
    }
}
