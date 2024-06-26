﻿using AIpaca_App.Data;
using AIpaca_App.Models;
using AIpaca_App.Resources.Splash;
using AIpaca_App.Views;
using CommunityToolkit.Mvvm.Messaging;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Maui.Controls;
using System.IO;
using AIpaca_App.ViewModels;

namespace AIpaca_App
{
    public partial class App : Application, INotifyPropertyChanged
    {
        
        public static DatabaseService? DatabaseService { get; private set; }

        // 업적 관리를 위한 db,
        //static DatabaseHelper? database;

        // 해당 SQLite-net에서는 파일이 존재하지 않으면 자동생성됨으로 필요없음
        //public static DatabaseHelper Database
        //{
        //    get
        //    {
        //        if (database == null)
        //        {
        //            database = new DatabaseHelper(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "AchieveList.db3"));
        //        }
        //        return database;
        //    }
        //}

        public static new App? Current => Application.Current as App;

        public App()
        {
            InitializeComponent();
            LoadPreferences();

            MainPage = new SplashPage();

            DatabaseService = new DatabaseService();
        }

        private void LoadPreferences()
        {
            // 다크 모드 설정 불러오기 및 적용
            var isDarkModeEnabled = Preferences.Get("IsDarkModeEnabled", false);
            ApplyTheme(isDarkModeEnabled);

            // 앱의 언어 설정 불러오기 및 적용
            var languageCode = Preferences.Get("LanguageCode", "ko");
            CultureInfo.CurrentCulture = new CultureInfo(languageCode);
            CultureInfo.CurrentUICulture = new CultureInfo(languageCode);
        }

        public void ApplyTheme(bool isDarkMode)
        {
            var currentTheme = isDarkMode ? AppTheme.Dark : AppTheme.Light;
            UserAppTheme = currentTheme;

            // 페이지 배경색 변경
            var backgroundColor = isDarkMode ? Color.FromArgb("#121212") : Color.FromArgb("#fafafa");
            if (Resources.TryGetValue("DefaultPageBackgroundColor", out var resource))
            {
                Resources["DefaultPageBackgroundColor"] = backgroundColor;
            }
        }
    }
}
