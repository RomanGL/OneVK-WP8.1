using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using OneVK.Model.Common;
using System.Collections.ObjectModel;
using OneVK.Model.Photo;
using OneVK.Enums.Common;
using OneVK.Model.Audio;
using OneVK.Model.Video;
using System.Diagnostics;
using OneVK.Core;
using GalaSoft.MvvmLight;
using OneVK.Model.Message;
using Windows.Graphics.Display;

namespace OneVK.Controls
{
    /// <summary>
    /// Является элементом управления для представления различных вложений.
    /// </summary>
    [TemplatePart(Name = RootPanelName, Type = typeof(StackPanel))]
    public sealed class AttachmentsPresenter : Control
    {
        private const string RootPanelName = "RootPanel";

        private StackPanel RootPanel;
        private AudiosPresenter _audios;
        private MediaItemsPresenter _mediaPresenter;
        private ListItemsPresenter _listPresenter;

        /// <summary>
        /// Инициализирует новый экземрляр класса.
        /// </summary>
        public AttachmentsPresenter()
        {
            this.DefaultStyleKey = typeof(AttachmentsPresenter);
        }

        #region Свойства зависимостей

        #region Attachments DependencyProperty
        /// <summary>
        /// Список прикреплений, которые требуется отобразить.
        /// </summary>
        public object Attachments
        {
            get { return (object)GetValue(AttachmentsProperty); }
            set { SetValue(AttachmentsProperty, value); }
        }

        public static readonly DependencyProperty AttachmentsProperty =
            DependencyProperty.Register("Attachments", typeof(object),
            typeof(AttachmentsPresenter), new PropertyMetadata(default(object), OnAttachmentsChanged));

        /// <summary>
        /// Вызывается при изменении коллекции вложений.
        /// </summary>
        private static void OnAttachmentsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var presenter = (AttachmentsPresenter)obj;
            presenter.Update();
        }

        /// <summary>
        /// Имеются ли элементы в презентере.
        /// </summary>
        public bool HasItems
        {
            get { return (bool)GetValue(HasItemsProperty); }
            set { SetValue(HasItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HasItemsProperty =
            DependencyProperty.Register("HasItems", typeof(bool), typeof(AttachmentsPresenter), new PropertyMetadata(default(bool)));


        #endregion

        #endregion

        #region Свойства
        #endregion

        #region Presenters
        /// <summary>
        /// Представляет аудиозаписи.
        /// </summary>
        public AudiosPresenter Audios
        {
            get
            {
                if (_audios == null)
                    _audios = new AudiosPresenter();
                return _audios;
            }
            private set { _audios = value; }
        }

        /// <summary>
        /// Представляет медиавложения.
        /// </summary>
        public MediaItemsPresenter MediaPresenter
        {
            get
            {
                if (_mediaPresenter == null)
                    _mediaPresenter = new MediaItemsPresenter();
                return _mediaPresenter;
            }
            private set { _mediaPresenter = value; }
        }

        /// <summary>
        /// Представляет элементы в виде списка.
        /// </summary>
        public ListItemsPresenter ListPresenter
        {
            get
            {
                if (_listPresenter == null)
                    _listPresenter = new ListItemsPresenter();
                return _listPresenter;
            }
            private set { _listPresenter = value; }
        }
        #endregion

        /// <summary>
        /// Вызывается при построении шаблона.
        /// </summary>
        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.RootPanel = GetTemplateChild("RootPanel") as StackPanel;
            Update();
        }

        /// <summary>
        /// Обновляет содержимое.
        /// </summary>
        private void Update()
        {
            if (RootPanel == null)
                return;           

            RootPanel.Children.Clear();
            Audios = null;
            MediaPresenter = null;
            ListPresenter = null;

            if (Attachments == null)
            {
                HasItems = false;
                return;
            }
            else
                HasItems = true;

            List<VKAttachment> wallAttachments = null;
            List<VKMessageAttachment> messageAttachments = null;

            if (Attachments is List<VKAttachment>)
            {
                wallAttachments = (List<VKAttachment>)Attachments;
                for (int i = 0; i < wallAttachments.Count; i++)
                {
                    switch (wallAttachments[i].Type)
                    {
                        case VKAttachmentType.Photo:
                            MediaPresenter.Items.Add((VKPhoto)wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Posted_photo:
                            break;
                        case VKAttachmentType.Video:
                            MediaPresenter.Items.Add((VKVideoBase)wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Audio:
                            Audios.Audios.Add((VKAudio)wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Doc:
                            ListPresenter.Items.Add(wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Graffiti:
                            break;
                        case VKAttachmentType.Link:
                            ListPresenter.Items.Add(wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Note:
                            break;
                        case VKAttachmentType.App:
                            break;
                        case VKAttachmentType.Poll:
                            ListPresenter.Items.Add(wallAttachments[i].Attachment);
                            break;
                        case VKAttachmentType.Page:
                            break;
                        case VKAttachmentType.Album:
                            break;
                        case VKAttachmentType.Photos_list:
                            break;
                        default:
                            break;
                    }
                }
            }
            else if (Attachments is List<VKMessageAttachment>)
            {
                messageAttachments = (List<VKMessageAttachment>)Attachments;
                for (int i = 0; i < messageAttachments.Count; i++)
                {
                    switch (messageAttachments[i].Type)
                    {
                        case VKMessageAttachmentType.Photo:
                            MediaPresenter.Items.Add((VKPhoto)messageAttachments[i].Attachment);
                            break;
                        case VKMessageAttachmentType.Video:
                            MediaPresenter.Items.Add((VKVideoBase)messageAttachments[i].Attachment);
                            break;
                        case VKMessageAttachmentType.Audio:
                            Audios.Audios.Add((VKAudio)messageAttachments[i].Attachment);
                            break;
                        case VKMessageAttachmentType.Doc:
                            ListPresenter.Items.Add(messageAttachments[i].Attachment);
                            break;
                        case VKMessageAttachmentType.Wall:
                            break;
                        case VKMessageAttachmentType.Wall_reply:
                            break;
                        case VKMessageAttachmentType.Sticker:
                            MediaPresenter.Sticker = (VKSticker)messageAttachments[i].Attachment;
                            break;
                        case VKMessageAttachmentType.Gift:
                            break;
                    }
                }
            }

            if (_mediaPresenter != null)
            {
#if DEBUG
                if (ViewModelBase.IsInDesignModeStatic)
                    _mediaPresenter.MaxRectSize = new Rectangle(400, 300);
#endif
                _mediaPresenter.MaxRectSize = new Rectangle(!double.IsInfinity(MaxWidth) ? MaxWidth : 
                    IsLandscape() ? Window.Current.Bounds.Height : Window.Current.Bounds.Width, 
                    !double.IsInfinity(MaxHeight) ? MaxHeight : 300);
                RootPanel.Children.Add(MediaPresenter);
            }
            if(_audios != null)
                RootPanel.Children.Add(Audios);
            if (_listPresenter != null)
                RootPanel.Children.Add(ListPresenter);
            InvalidateMeasure();
        }

        private static DisplayInformation _info;

        private static bool IsLandscape()
        {
            if (_info == null) _info = DisplayInformation.GetForCurrentView();
            return _info.CurrentOrientation == DisplayOrientations.Landscape ||
                _info.CurrentOrientation == DisplayOrientations.LandscapeFlipped;
        }
    }
}
