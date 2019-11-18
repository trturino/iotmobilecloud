using System;
using System.ComponentModel;
using trtrurino.iot.mobile.Services;
using trtrurino.iot.mobile.ViewModels;
using Xamarin.Forms;

namespace trtrurino.iot.mobile.Views
{
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        private ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel(new DeviceService());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        private void Switch_OnToggled(object sender, ToggledEventArgs e)
        {
            var sw = sender as Switch;
            if (sw == null) return;

            if (!viewModel.IsBusy)
            {
                var device = sw.BindingContext as Models.Device;
                if (device == null) return;
                sw.Toggled -= Switch_OnToggled;
                viewModel.SetStatus.Execute(device);
            }
        }

        private void BindableObject_OnBindingContextChanged(object sender, EventArgs e)
        {
            if (!(sender is Switch s)) return;

            s.Toggled += Switch_OnToggled;
        }
    }
}