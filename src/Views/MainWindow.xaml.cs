﻿using Savaged.BlackNotepad.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Drawing;
using System;

namespace Savaged.BlackNotepad.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel = DataContext as MainViewModel;
        }

        private void OnContentTextSelectionChanged(object sender, RoutedEventArgs e)
        {
            if (_viewModel != null)
            {
                _viewModel.SelectedText = ContentText.SelectedText;
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (_viewModel != null)
            {
                var result = _viewModel.OnClosing();
                if (!result)
                {
                    e.Cancel = true;
                }
            }
        }

        private void OnContentTextPreviewDragOver(object sender, DragEventArgs e)
        {
            if (_viewModel != null && _viewModel.CanExecuteDragDrop
                && (e.Data.GetDataPresent(DataFormats.FileDrop)
                    || e.Data.GetDataPresent(DataFormats.Text)
                    || e.Data.GetDataPresent(DataFormats.Rtf)
                    || e.Data.GetDataPresent(DataFormats.Html)
                    || e.Data.GetDataPresent(DataFormats.UnicodeText)
                    || e.Data.GetDataPresent(DataFormats.Xaml)))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void OnContentTextPreviewDrop(object sender, DragEventArgs e)
        {
            if (_viewModel != null && _viewModel.CanExecuteDragDrop)
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    var fileLocation = (string[])e.Data.GetData(DataFormats.FileDrop);
                    _viewModel.New(fileLocation[0]);
                    e.Handled = true;
                    return;
                }
            }
            e.Handled = false;
        }
    }
}
