using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;
using Demo_MVVMBasic.DataAccessLayer;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using Demo_MVVMBasic.BusinessLayer;

namespace Demo_MVVMBasic
{
    class MainWindowViewModel : ObservableObject
    {
        public ICommand ButtonSellCommand { get; set; }
        public ICommand ButtonBuyCommand { get; set; }
        public ICommand ButtonAddCommand { get; set; }
        public ICommand ButtonEditCommand { get; set; }
        public ICommand ButtonDeleteCommand { get; set; }
        public ICommand ButtonQuitCommand { get; set; }

        private Widget _selectedWidget;
        private Widget _widgetToAdd;
        private Widget _widgetToEdit;
        private string _widgetOperationFeedback;
        private WidgetBusiness _widgetBusiness;

        public ObservableCollection<Widget> Widgets { get; set; }

        public Widget SelectedWidget
        {
            get { return _selectedWidget; }
            set
            {
                _selectedWidget = value;
                if (_selectedWidget != null)
                {
                    WidgetToEdit = SelectedWidget.Copy();
                    OnPropertyChanged(nameof(SelectedWidget));
                }
            }
        }

        public Widget WidgetToAdd
        {
            get { return _widgetToAdd; }
            set
            {
                _widgetToAdd = value;
                OnPropertyChanged(nameof(WidgetToAdd));
            }
        }

        public Widget WidgetToEdit
        {
            get { return _widgetToEdit; }
            set
            {
                _widgetToEdit = value;
                OnPropertyChanged(nameof(WidgetToEdit));
            }
        }

        public string WidgetOperationFeedback
        {
            get { return _widgetOperationFeedback; }
            set
            {
                _widgetOperationFeedback = value;
                OnPropertyChanged(nameof(WidgetOperationFeedback));
            }
        }

        public MainWindowViewModel()
        {
            _widgetBusiness = new WidgetBusiness();
            Widgets = new ObservableCollection<Widget>(_widgetBusiness.AllWidgets());

            if (Widgets.Any()) SelectedWidget = Widgets[0];

            ButtonSellCommand = new RelayCommand(new Action<object>(SellWidgets));
            ButtonBuyCommand = new RelayCommand(new Action<object>(BuyWidgets));
            ButtonAddCommand = new RelayCommand(new Action<object>(AddWidget));
            ButtonEditCommand = new RelayCommand(new Action<object>(EditWidget));
            ButtonDeleteCommand = new RelayCommand(new Action<object>(DeleteWidget));
            ButtonQuitCommand = new RelayCommand(new Action<object>(QuitWidget));

            WidgetToAdd = new Widget();
            WidgetToEdit = SelectedWidget.Copy();
        }

        public void SellWidgets(object parameter)
        {
            int.TryParse((string)parameter, out int quantity);
            SelectedWidget.CurrentInventory -= quantity;
        }

        public void BuyWidgets(object parameter)
        {
            int.TryParse((string)parameter, out int quantity);
            SelectedWidget.CurrentInventory += quantity;
        }

        public void AddWidget(object parameter)
        {
            //
            // TODO - add code to validate user input
            //

            string commandParameter = parameter.ToString();

            if (commandParameter == "SAVE")
            {
                if (WidgetToAdd != null)
                {
                    // TODO - handle I/O Errors
                    _widgetBusiness.AddWidget(WidgetToAdd);

                    Widgets.Add(WidgetToAdd);
                    WidgetOperationFeedback = "New Widget Added";
                    SelectedWidget = WidgetToAdd;
                }
            }
            else if (commandParameter == "CANCEL")
            {
                WidgetOperationFeedback = "New Widget Canceled";
            }
            else
            {
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding widgets.");
            }
            WidgetToAdd = new Widget();
        }

        public void EditWidget(object parameter)
        {
            //
            // TODO - add code to validate user input
            //

            string commandParameter = parameter.ToString();

            if (commandParameter == "SAVE")
            {
                if (WidgetToEdit != null)
                {
                    // TODO - handle I/O Errors
                    _widgetBusiness.UpdateWidget(WidgetToEdit);

                    Widget widgetToDelete = SelectedWidget;
                    Widgets.Add(WidgetToEdit);
                    SelectedWidget = WidgetToEdit;
                    Widgets.Remove(widgetToDelete);

                    WidgetOperationFeedback = "Widget Updated";
                }
            }
            else if (commandParameter == "CANCEL")
            {
                WidgetToEdit = SelectedWidget.Copy();
                WidgetOperationFeedback = "Widget Update Canceled";
            }
            else
            {
                throw new ArgumentException($"{commandParameter} is not a valid command parameter for the adding widgets.");
            }
        }

        public void DeleteWidget(object parameter)
        {
            if (SelectedWidget != null)
            {
                string widgetName = SelectedWidget.Name;

                MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete the {widgetName} widgets from inventory?", "Delete Widgets", MessageBoxButton.YesNo);

                switch (result)
                {
                    case MessageBoxResult.Yes:
                        // TODO - handle I/O Errors
                        _widgetBusiness.DeleteWidget(SelectedWidget.Id);

                        Widgets.Remove(SelectedWidget);
                        WidgetOperationFeedback = "Widget Deleted";

                        if (Widgets.Any()) SelectedWidget = Widgets[0];
                        break;

                    case MessageBoxResult.No:
                        WidgetOperationFeedback = "Widget Deletion Canceled";
                        break;
                }
            }
        }

        public void QuitWidget(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
