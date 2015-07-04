﻿using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Microsoft.ApplicationInsights;
using MyToolkit.Paging;
using SampleUwpApp.Views;

namespace SampleUwpApp
{
    sealed partial class App : MtApplication
    {
        public static TelemetryClient TelemetryClient;
        private HamburgerFrame _hamburgerFrame;

        public App()
        {
            TelemetryClient = new TelemetryClient();
            InitializeComponent();
        }

        public override Type StartPageType => typeof(MainPage);

        public override UIElement CreateWindowContentElement()
        {
            _hamburgerFrame = new HamburgerFrame();
            _hamburgerFrame.Hamburger.Header = new HamburgerHeader();
            _hamburgerFrame.Hamburger.TopItems = new ObservableCollection<HamburgerItem>
            {
                new HamburgerItem
                {
                    Label = "Home",
                    Icon = '\uE825'.ToString(),
                    PageType = typeof(MainPage)
                },
                new HamburgerItem
                {
                    Label = "Test",
                    Icon = '\uE825'.ToString(),
                    PageType = typeof(TestPage)
                }
            };
            _hamburgerFrame.Hamburger.BottomItems = new ObservableCollection<HamburgerItem>
            {
                new HamburgerItem
                {
                    Label = "Settings",
                    Icon = '\uE825'.ToString(),
                    PageType = typeof(SettingsPage)
                }
            };
            return _hamburgerFrame.Hamburger;
        }

        public override MtFrame GetFrame(UIElement windowContentElement)
        {
            return _hamburgerFrame.Frame;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);
        }
    }
}
